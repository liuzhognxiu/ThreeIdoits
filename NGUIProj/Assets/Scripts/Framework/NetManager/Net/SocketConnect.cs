using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


enum ErrorCode
{
    Success = 0,
    ConnectError = -1,
    TimeOutError = -2,
}

/// <summary>
/// 
/// </summary>
/// <param name="package"></param>
public delegate void NetPushCallback(SocketPackage package);
/// <summary>
/// 
/// </summary>
public class SocketConnect
{
    /// <summary>
    /// push Action的请求
    /// </summary>
    private static readonly List<SocketPackage> ActionPools = new List<SocketPackage>();
    private Socket _socket;
    private readonly string _host;
    private readonly int _port;
    private readonly IHeadFormater _formater;
    private bool _isDisposed;
    private readonly List<SocketPackage> _sendList;
    private readonly Queue<SocketPackage> _receiveQueue;
    private readonly Queue<SocketPackage> _pushQueue;
    private const int TimeOut = 30;//30秒的超时时间
    private Thread _thread = null;
    private const int HearInterval = 10000;
    private System.Threading.Timer _heartbeatThread = null;
    private byte[] _hearbeatPackage;
    private bool m_isConnect = false;


    public SocketConnect(string host, int port, IHeadFormater formater)
    {
        this._host = host;
        this._port = port;
        _formater = formater;
        _sendList = new List<SocketPackage>();
        _receiveQueue = new Queue<SocketPackage>();
        _pushQueue = new Queue<SocketPackage>();
    }

    static public void PushActionPool(int actionId, GameAction action)
    {
        RemoveActionPool(actionId);
        SocketPackage package = new SocketPackage();
        package.ActionId = actionId;
        package.Action = action;
        ActionPools.Add(package);
    }

    static public void RemoveActionPool(int actionId)
    {
        foreach (SocketPackage pack in ActionPools)
        {
            if (pack.ActionId == actionId)
            {
                ActionPools.Remove(pack);
                break;
            }
        }
    }
    /// <summary>
    /// 取出回返消息包
    /// </summary>
    /// <returns></returns>
    public SocketPackage Dequeue()
    {
        lock (_receiveQueue)
        {
            if (_receiveQueue.Count == 0)
            {
                return null;
            }
            else
            {
                return _receiveQueue.Dequeue();
            }
        }
    }

    public SocketPackage DequeuePush()
    {
        lock (_pushQueue)
        {
            if (_pushQueue.Count == 0)
            {
                return null;
            }
            else
            {
                return _pushQueue.Dequeue();
            }
        }
    }
    private void CheckReceive()
    {
        while (true)
        {
            if (_socket == null) return;
            try
            {
                if (_socket.Poll(5, SelectMode.SelectRead))
                {
                    if (_socket.Available == 0)
                    {
                        Debug.Log("Close Socket");
                        Close();
                        break;
                    }
                    byte[] prefix = new byte[2048];
                    int recnum = _socket.Receive(prefix);

                    if (recnum > 0)
                    {
                        byte[] data = new byte[recnum];
                        int datalen = recnum;
                        int streamPos = 0;
                        Buffer.BlockCopy(prefix, 0, data, 0, datalen);
                        

                        NetReader reader = new NetReader(_formater);
                        reader.pushNetStream(data, NetworkType.Socket, NetWriter.ResponseContentType);
                        SocketPackage findPackage = null;

                        Debug.Log("Socket receive ok, revLen:" + recnum
                            + ", actionId:" + reader.ActionId
                            + ", error:" + reader.StatusCode
                            + ", packLen:" + reader.Buffer.Length);
                        lock (_sendList)
                        {
                            //find pack in send queue.
                            foreach (SocketPackage package in _sendList)
                            {
                                if (package.ActionId == reader.ActionId || package.ActionRespId == reader.ActionId)
                                {
                                    package.Reader = reader;
                                    package.ErrorCode = reader.StatusCode;
                                    findPackage = package;
                                    break;
                                }

                            }
                        }
                        if (findPackage == null)
                        {
                            lock (_receiveQueue)
                            {
                                //find pack in receive queue.
                                foreach (SocketPackage package in ActionPools)
                                {
                                    if (package.ActionId == reader.ActionId || package.ActionRespId == reader.ActionId)
                                    {
                                        package.Reader = reader;
                                        package.ErrorCode = reader.StatusCode;
                                        findPackage = package;
                                        break;
                                    }
                                }
                            }
                        }
                        if (findPackage != null)
                        {
                            lock (_receiveQueue)
                            {
                                _receiveQueue.Enqueue(findPackage);
                            }
                            lock (_sendList)
                            {
                                _sendList.Remove(findPackage);
                            }
                        }
                        else
                        {
                            //server push pack.
                            SocketPackage package = new SocketPackage();
                            package.ActionId = reader.ActionId;
                            package.ErrorCode = reader.StatusCode;
                            package.Reader = reader;

                            lock (_pushQueue)
                            {
                                _pushQueue.Enqueue(package);
                            }
                        }

                    }

                }
                else if (_socket.Poll(5, SelectMode.SelectError))
                {
                    Close();
                    UnityEngine.Debug.Log("SelectError Close Socket");
                    break;

                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("catch" + ex.ToString());
                if (ex is SocketException)
                {
                    switch(((SocketException)ex).SocketErrorCode)
                    {
                        case SocketError.Disconnecting:
                        case SocketError.HostDown:
                        case SocketError.Interrupted:
                        case SocketError.NetworkUnreachable:
                        case SocketError.Shutdown:
                            Close();
                            break;
                        default:
                            UnityEngine.Debug.Log("error code is: " + ((SocketException)ex).SocketErrorCode);
                            Close();
                            break;
                    }
                }
            }

            Thread.Sleep(5);

        }

    }

    //public void CheckNetState()
    //{
    //    if (socket == null)
    //    {
    //        return;
    //    }
    //    //DateTime start = DateTime.Now;
    //    UnityEngine.NetworkReachability state = UnityEngine.Application.internetReachability;
    //    if (state == UnityEngine.NetworkReachability.NotReachable)
    //    {
    //        IsNetStateChange = true;
    //        UnityEngine.Debug.Log("IsNetStateChange = true" + state.ToString());
    //    }
    //    else if (NetState != state)//处理3G 2G的情况
    //    {
    //        UnityEngine.Debug.Log("IsNetStateChange = true" + state.ToString());
    //        IsNetStateChange = true;
    //    }
    //    //UnityEngine.Debug.Log("CheckTime" + DateTime.Now.Subtract(start).TotalMilliseconds );
    //}

    /// <summary>
    /// 打开连接
    /// </summary>
    public void Open()
    {
        UnityEngine.NetworkReachability state = UnityEngine.Application.internetReachability;
        if (state != UnityEngine.NetworkReachability.NotReachable)
        {
            IPAddress[] address = Dns.GetHostEntry(_host).AddressList;

            IPAddress add_ip = address[0];
            if (IsApplePlatform)
            {
                foreach (var addr in address)
                {
                    if (addr.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        add_ip = addr;
                        break;
                    }
                }
            }

            if (add_ip.AddressFamily == AddressFamily.InterNetworkV6)
            {
                _socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            }
            else
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            _socket.SendBufferSize = 8192;
            _socket.ReceiveBufferSize = 8192;
            //_socket.BeginConnect(add_ip, _port, new AsyncCallback(connect_callback), _socket);//异步connect
            _socket.Connect(add_ip, _port);
            Debug.Log("Socket connected " + add_ip + _port);
            //if (_heartbeatThread == null)
            //{
            //    _heartbeatThread = new Timer(SendHeartbeatPackage, null, HearInterval, HearInterval);
            //    ReBuildHearbeat();
            //}
            _thread = new Thread(new ThreadStart(CheckReceive));
            _thread.Start();
        }

    }

    private void connect_callback(IAsyncResult ar)
    {
        try
        {
            var socket = ar.AsyncState as Socket;
            if (socket != null)
            {
                socket.EndConnect(ar);
            }
            Debug.Log("Socket connected to " + socket.RemoteEndPoint.ToString());
            m_isConnect = true;
            if (_heartbeatThread == null)
            {
                _heartbeatThread = new System.Threading.Timer(SendHeartbeatPackage, null, HearInterval, HearInterval);
                ReBuildHearbeat();
            }
            _thread = new Thread(new ThreadStart(CheckReceive));
            _thread.Start();
        }
        catch (SocketException ex)
        {
            m_isConnect = false;
        }
        
    }

    /// <summary>
    /// rebuild socket send hearbeat package 
    /// </summary>
    public void ReBuildHearbeat()
    {
        _hearbeatPackage = _formater.BuildHearbeatPackage();
    }

    private void SendHeartbeatPackage(object state)
    {
        try
        {
            if (_hearbeatPackage != null && !PostSend(_hearbeatPackage))
            {
                Debug.Log("send heartbeat paketage fail");
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void EnsureConnected()
    {
        if (_socket == null)
        {
            Open();
        }

    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        if (_socket == null) return;
        try
        {
            lock (this)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                _socket = null;
                m_isConnect = false;

                _heartbeatThread.Dispose();
                _heartbeatThread = null;

                _thread.Abort();
                _thread = null;
            }

        }
        catch (Exception)
        {
            _socket = null;
        }
    }


    private bool IsApplePlatform
    {
        get
        {
            return Application.platform == RuntimePlatform.IPhonePlayer ||
                   Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="data"></param>
    private bool PostSend(byte[] data)
    {
        EnsureConnected();
        if (_socket != null)
        {
            //socket.Send(data);
            IAsyncResult asyncSend = _socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(sendCallback), _socket);
            bool success = asyncSend.AsyncWaitHandle.WaitOne(5000, true);
            if (!success)
            {
                Debug.Log("asyncSend error close socket");
                Close();
                return false;
            }
            return true;
        }
        return false;

    }
    private void sendCallback(IAsyncResult asyncSend)
    {
    }
    public void Send(byte[] data, SocketPackage package)
    {
        if (data == null)
        {
            return;
        }
        lock (_sendList)
        {
            _sendList.Add(package);
        }

        try
        {
            PostSend(data);
            //UnityEngine.Debug.Log("Socket send actionId:" + package.ActionId + ", msgId:" + package.MsgId + ", send result:" + bRet);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("Socket send actionId: " + package.ActionId + " error" + ex);
            package.ErrorCode = (int)ErrorCode.ConnectError;
            package.ErrorMsg = ex.ToString();
            lock (_receiveQueue)
            {
                _receiveQueue.Enqueue(package);
            }
            lock (_sendList)
            {
                _sendList.Remove(package);
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        try
        {
            if (!this._isDisposed)
            {
                if (isDisposing)
                {
                    //if (socket != null) socket.Dispose(true);
                }
            }
        }
        finally
        {
            this._isDisposed = true;
        }
    }

    public void ProcessTimeOut()
    {
        SocketPackage findPackage = null;
        lock (_sendList)
        {
            foreach (SocketPackage package in _sendList)
            {
                if (DateTime.Now.Subtract(package.SendTime).TotalSeconds > TimeOut)
                {
                    package.ErrorCode = (int)ErrorCode.TimeOutError;
                    package.ErrorMsg = "TimeOut";
                    findPackage = package;
                    break;
                }
            }
        }
        if (findPackage != null)
        {
            lock (_receiveQueue)
            {
                _receiveQueue.Enqueue(findPackage);
            }
            lock (_sendList)
            {
                _sendList.Remove(findPackage);
            }
        }
    }


}

