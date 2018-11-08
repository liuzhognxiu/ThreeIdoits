using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class UILogic {

	private HashSet<NotifyPropChanged> m_itemSources = new HashSet<NotifyPropChanged>();
	private EventController m_eventController;

	public NotifyPropChanged ItemSource
	{
		set
		{
			if (value != null && !m_itemSources.Contains(value))
			{
				m_itemSources.Add(value);
				value.SetEventController(m_eventController);
				value.AddUnloadCallback(() =>
			    {
					if (m_itemSources != null && m_itemSources.Contains(value))
					{
						m_itemSources.Remove(value);
					}
				});
			}
		}
	}

    public UILogic()
	{
		m_eventController = new EventController();
	}

	public void SetBinding<T>(String key, Action<T> action)
	{
        if (m_eventController.ContainsEvent(key))
            return;
		m_eventController.AddEventListener(key, action);
	}
    public void SetBinding(String key, Action action)
    {
        if (m_eventController.ContainsEvent(key))
            return;
        m_eventController.AddEventListener(key, action);
    }

    public void SetBinding<T1, T2>(String key, Action<T1, T2>action)
    {
        if (m_eventController.ContainsEvent(key))
            return;
        m_eventController.AddEventListener(key, action);
    }

	/// 根据数据源更新UI。（效率不高，不要频繁调用）
	public void UpdateUI()
	{
		foreach (var itemSource in m_itemSources)
		{
			if (itemSource != null)
			{
				var type = itemSource.GetType();
				//获取带一个参数回调方法的TriggerEvent。

				System.Reflection.MethodInfo mTriggerEvent = null;
				System.Reflection.MethodInfo[] methodInfoArray =  m_eventController.GetType().GetMethods();
				foreach(System.Reflection.MethodInfo mi in methodInfoArray)
				{
					if(mi.Name == "TriggerEvent" && mi.IsGenericMethod && mi.GetGenericArguments().Length == 1)
					{
						mTriggerEvent = mi;
						break;
					}
				}

				if(mTriggerEvent == null)
					return;
				foreach (var item in m_eventController.TheRouter)
				{
					var prop = type.GetProperty(item.Key);
					if (prop == null)
						continue;
					var method = mTriggerEvent.MakeGenericMethod(prop.PropertyType);//对泛型进行类型限定
					var value = prop.GetGetMethod().Invoke(itemSource, null);//获取参数值
					method.Invoke(m_eventController, new object[] { item.Key, value });//调用对应参数类型的触发方法
				}
			}
		}
	}

	public virtual void Release()
	{
		// 不注释：清空属性监听, 需要在UI重新Load的时候重新设置属性监听ItemSource
		// 注释：保留属性监听，不需要重新设置ItemSource
		foreach (var item in m_itemSources)
		{
			if (item != null)
				item.RemoveEventController(m_eventController);
		}
		m_itemSources.Clear(); // 如果RemoveEventController，需要把m_itemSources列表同时清空，否则无法重新设置EventController
		
		// 需要清空，在UI重新Load的时候重新添加监听
		m_eventController.Cleanup();
	}
}

public interface INotifyPropChanged
{
	void SetEventController(EventController controller);
	void RemoveEventController(EventController controller);
	void OnPropertyChanged<T>(string propertyName, T value);
	void AddUnloadCallback(Action onUnload);
}
public abstract class NotifyPropChanged
{
	private HashSet<EventController> m_uiBindingSet = new HashSet<EventController>();
	private Action m_onUnload;

	public void SetEventController(EventController controller)
	{
		m_uiBindingSet.Add(controller);
	}

	public void RemoveEventController(EventController controller)
	{
		m_uiBindingSet.Remove(controller);
	}

	public void OnPropertyChanged<T>(string propertyName, T value)
	{
		foreach (var item in m_uiBindingSet)
		{
			if (item != null)
				item.TriggerEvent(propertyName, value);
		}
	}
    public void OnPropertyUpdate(string propertyName)
    {
        foreach (var item in m_uiBindingSet)
        {
            if (item != null)
                item.TriggerEvent(propertyName);
        }
    }

    public void AddUnloadCallback(Action onUnload)
	{
		m_onUnload = onUnload;
	}

	protected void ClearBinding()
	{
		if (m_onUnload != null)
			m_onUnload();
		m_uiBindingSet.Clear();
	}
	
	
}

public class EventController
{
    private List<string> m_permanentEvents = new List<string>();
    private Dictionary<string, Delegate> m_theRouter = new Dictionary<string, Delegate>();

    public void AddEventListener<T>(string eventType, Action<T> handler)
    {
        this.OnListenerAdding(eventType, handler);
        this.m_theRouter[eventType] = (Action<T>)Delegate.Combine((Action<T>)this.m_theRouter[eventType], handler);
    }

    public void AddEventListener(string eventType, Action handler)
    {
        this.OnListenerAdding(eventType, handler);
        this.m_theRouter[eventType] = (Action)Delegate.Combine((Action)this.m_theRouter[eventType], handler);
    }

    public void AddEventListener<T, U>(string eventType, Action<T, U> handler)
    {
        this.OnListenerAdding(eventType, handler);
        this.m_theRouter[eventType] = (Action<T, U>)Delegate.Combine((Action<T, U>)this.m_theRouter[eventType], handler);
    }

    public void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        this.OnListenerAdding(eventType, handler);
        this.m_theRouter[eventType] = (Action<T, U, V>)Delegate.Combine((Action<T, U, V>)this.m_theRouter[eventType], handler);
    }

    public void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
    {
        this.OnListenerAdding(eventType, handler);
        this.m_theRouter[eventType] = (Action<T, U, V, W>)Delegate.Combine((Action<T, U, V, W>)this.m_theRouter[eventType], handler);
    }

    public void Cleanup()
    {
        List<string> list = new List<string>();
        foreach (KeyValuePair<string, Delegate> pair in this.m_theRouter)
        {
            bool flag = false;
            foreach (string str in this.m_permanentEvents)
            {
                if (pair.Key == str)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                list.Add(pair.Key);
            }
        }
        foreach (string str in list)
        {
            this.m_theRouter.Remove(str);
        }
    }

    public bool ContainsEvent(string eventType)
    {
        return this.m_theRouter.ContainsKey(eventType);
    }

    public void MarkAsPermanent(string eventType)
    {
        this.m_permanentEvents.Add(eventType);
    }

    private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
    {
        if (!this.m_theRouter.ContainsKey(eventType))
        {
            this.m_theRouter.Add(eventType, null);
        }
        Delegate delegate2 = this.m_theRouter[eventType];
        if ((delegate2 != null) && (delegate2.GetType() != listenerBeingAdded.GetType()))
        {
            throw new EventException(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, delegate2.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }

    private void OnListenerRemoved(string eventType)
    {
        if (this.m_theRouter.ContainsKey(eventType) && (this.m_theRouter[eventType] == null))
        {
            this.m_theRouter.Remove(eventType);
        }
    }

    private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
    {
        if (!this.m_theRouter.ContainsKey(eventType))
        {
            return false;
        }
        Delegate delegate2 = this.m_theRouter[eventType];
        if ((delegate2 != null) && (delegate2.GetType() != listenerBeingRemoved.GetType()))
        {
            throw new EventException(string.Format("Remove listener {0}\" failed, Current type is {1}, adding type is {2}.", eventType, delegate2.GetType(), listenerBeingRemoved.GetType()));
        }
        return true;
    }

    public void RemoveEventListener<T>(string eventType, Action<T> handler)
    {
        if (this.OnListenerRemoving(eventType, handler))
        {
            this.m_theRouter[eventType] = (Action<T>)Delegate.Remove((Action<T>)this.m_theRouter[eventType], handler);
            this.OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener(string eventType, Action handler)
    {
        if (this.OnListenerRemoving(eventType, handler))
        {
            this.m_theRouter[eventType] = (Action)Delegate.Remove((Action)this.m_theRouter[eventType], handler);
            this.OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
    {
        if (this.OnListenerRemoving(eventType, handler))
        {
            this.m_theRouter[eventType] = (Action<T, U>)Delegate.Remove((Action<T, U>)this.m_theRouter[eventType], handler);
            this.OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        if (this.OnListenerRemoving(eventType, handler))
        {
            this.m_theRouter[eventType] = (Action<T, U, V>)Delegate.Remove((Action<T, U, V>)this.m_theRouter[eventType], handler);
            this.OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
    {
        if (this.OnListenerRemoving(eventType, handler))
        {
            this.m_theRouter[eventType] = (Action<T, U, V, W>)Delegate.Remove((Action<T, U, V, W>)this.m_theRouter[eventType], handler);
            this.OnListenerRemoved(eventType);
        }
    }

    public void TriggerEvent(string eventType)
    {
        Delegate delegate2;
        if (this.m_theRouter.TryGetValue(eventType, out delegate2))
        {
            Delegate[] invocationList = delegate2.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action action = invocationList[i] as Action;
                if (action == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    //LoggerHelper.Except(exception, null);
                }
            }
        }
    }

    public void TriggerEvent<T>(string eventType, T arg1)
    {
        Delegate delegate2;
        if (this.m_theRouter.TryGetValue(eventType, out delegate2))
        {
            Delegate[] invocationList = delegate2.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action<T> action = invocationList[i] as Action<T>;
                if (action == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    action(arg1);
                }
                catch (Exception exception)
                {
                    // LoggerHelper.Except(exception, null);
                }
            }
        }
    }

    public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
    {
        Delegate delegate2;
        if (this.m_theRouter.TryGetValue(eventType, out delegate2))
        {
            Delegate[] invocationList = delegate2.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action<T, U> action = invocationList[i] as Action<T, U>;
                if (action == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    action(arg1, arg2);
                }
                catch (Exception exception)
                {
                    // LoggerHelper.Except(exception, null);
                }
            }
        }
    }

    public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
    {
        Delegate delegate2;
        if (this.m_theRouter.TryGetValue(eventType, out delegate2))
        {
            Delegate[] invocationList = delegate2.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action<T, U, V> action = invocationList[i] as Action<T, U, V>;
                if (action == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    action(arg1, arg2, arg3);
                }
                catch (Exception exception)
                {
                    // LoggerHelper.Except(exception, null);
                }
            }
        }
    }

    public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
    {
        Delegate delegate2;
        if (this.m_theRouter.TryGetValue(eventType, out delegate2))
        {
            Delegate[] invocationList = delegate2.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action<T, U, V, W> action = invocationList[i] as Action<T, U, V, W>;
                if (action == null)
                {
                    throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                }
                try
                {
                    action(arg1, arg2, arg3, arg4);
                }
                catch (Exception exception)
                {
                    // LoggerHelper.Except(exception, null);
                }
            }
        }
    }

    public Dictionary<string, Delegate> TheRouter
    {
        get
        {
            return this.m_theRouter;
        }
    }
}

public class EventException : Exception
{
    public EventException(string message)
        : base(message)
    {
    }

    public EventException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}