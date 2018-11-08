using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;

public class ByteBuffer
{
    MemoryStream stream = null;
    BinaryWriter writer = null;
    BinaryReader reader = null;


    public ByteBuffer()
    {
        stream = new MemoryStream();
        writer = new BinaryWriter(stream);
    }

    public ByteBuffer(byte[] data)
    {
        if (data != null)
        {
            stream = new MemoryStream(data);
            reader = new BinaryReader(stream);
        }
        else
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }
    }

    // 重写方法功能:
    public void ByteBufferWrite()
    {
        stream.Seek(0, SeekOrigin.Begin);
        stream.SetLength(0);
        stream.Position = 0;

        if (writer == null)
        {
            writer = new BinaryWriter(stream);
        }
        else
        {
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.BaseStream.Position = 0;
        }
    }

    public void ByteBufferReader(byte[] data)
    {
        stream.Seek(0, SeekOrigin.Begin);
        stream.SetLength(0);
        stream.Position = 0;
        stream.Write(data, 0, data.Length);
        stream.Position = 0;
        if (reader == null)
        {
            reader = new BinaryReader(stream);
        }
        else
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.BaseStream.Position = 0;
        }
    }


    public void Close()
    {
        if (writer != null) writer.Close();
        if (reader != null) reader.Close();

        stream.Close();
        writer = null;
        reader = null;
        stream = null;
    }

    public void WriteByte(byte v)
    {
        writer.Write((byte)v);
    }

    public void WriteChar(char v)
    {
        writer.Write((char)v);
    }

    public void WriteBool(bool v)
    {
        writer.Write((bool)v);
    }

    public void WriteInt(int v)
    {
        writer.Write((int)v);
    }

    //public void WriteBytes(byte[] msg)
    //{
    //    writer.Write(msg);
    //}

    public void WriteUInt(uint v)
    {
        writer.Write((uint)v);
    }

    public void WriteShort(ushort v)
    {
        writer.Write((ushort)v);
    }

    public void WriteUShort(ushort v)
    {
        writer.Write((ushort)v);
    }

    public void WriteLong(long v)
    {
        writer.Write((long)v);
    }

    public void WriteULong(ulong v)
    {
        writer.Write((ulong)v);
    }

    public void WriteFloat(float v)
    {
        byte[] temp = BitConverter.GetBytes(v);
        Array.Reverse(temp);
        writer.Write(BitConverter.ToSingle(temp, 0));
    }

    public void WriteDouble(double v)
    {
        byte[] temp = BitConverter.GetBytes(v);
        Array.Reverse(temp);
        writer.Write(BitConverter.ToDouble(temp, 0));
    }

    public void WriteString(string v)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(v);
        if (bytes.Length < 255)
        {
            writer.Write((byte)bytes.Length);
        }
        else if (bytes.Length < 0xfffe)
        {
            writer.Write((byte)0xff);
            writer.Write((ushort)bytes.Length);
        }
        else
        {
            writer.Write((byte)0xff);
            writer.Write((ushort)0xffff);
            writer.Write((ulong)bytes.Length);
        }
        writer.Write(bytes);
    }

    public void WriteShortString(string v)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(v);
        writer.Write((ushort)bytes.Length);
        if (bytes.Length > 0)
        {
            writer.Write(bytes);
        }
    }

    public void WriteStringLength(string v, int length)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(v);
        writer.Write(bytes, 0, length);
    }

    public void WriteBytes(byte[] v)
    {
        writer.Write((int)v.Length);
        writer.Write(v);
    }

    public void WriteBytesOnly(byte[] v)
    {
        writer.Write(v);
    }

    public void WriteBytesPtr(byte[] v)
    {
        writer.Write((byte)v.Length);
        writer.Write(v);
    }

    //public void WriteBuffer(LuaByteBuffer strBuffer)
    //{
    //    WriteBytes(strBuffer.buffer);
    //}

    public void WriteDataLength(byte[] data, int length)
    {
        writer.Write(data, 0, length);
    }

    public byte ReadByte()
    {
        return reader.ReadByte();
    }

    public char ReadChar()
    {
        return reader.ReadChar();
    }

    public bool ReadBool()
    {
        return reader.ReadBoolean();
    }

    public sbyte ReadSByte()
    {
        return reader.ReadSByte();
    }

    public int ReadInt()
    {
        return (int)reader.ReadInt32();
    }

    public UInt32 ReadUInt()
    {
        return reader.ReadUInt32();
    }

    public ushort ReadShort()
    {
        return (ushort)reader.ReadInt16();
    }

    public ushort ReadUShort()
    {
        return (ushort)reader.ReadUInt16();
    }

    public Int64 ReadLong()
    {
        return reader.ReadInt64();
    }

    public float ReadFloat()
    {
        byte[] temp = BitConverter.GetBytes(reader.ReadSingle());
        Array.Reverse(temp);
        return BitConverter.ToSingle(temp, 0);
    }

    public double ReadDouble()
    {
        byte[] temp = BitConverter.GetBytes(reader.ReadDouble());
        Array.Reverse(temp);
        return BitConverter.ToDouble(temp, 0);
    }

    public string ReadShortString()
    {
        ushort len = ReadShort();
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        return Encoding.Default.GetString(buffer); ;
    }

    public string ReadString()
    {
        int len = ReadInt();
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        return Encoding.Default.GetString(buffer);
    }


    public string ReadCharString()
    {
        char len = ReadChar();
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        return Encoding.Default.GetString(buffer);
    }

    public string ReadByteString()
    {
        ushort len = ReadByte();
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        //string str1 = Encoding.ASCII.GetString(buffer);
        //string str2 = Encoding.BigEndianUnicode.GetString(buffer);
        //string str3 = Encoding.Default.GetString(buffer);
        //string str4 = Encoding.Unicode.GetString(buffer);
        //string str5 = Encoding.UTF32.GetString(buffer);
        //string str6 = Encoding.UTF7.GetString(buffer);
        //string str7 = Encoding.UTF8.GetString(buffer);

        return Encoding.Default.GetString(buffer);
    }

    public string ReadByteString_UTF8()
    {
        ushort len = ReadByte();
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        return Encoding.UTF8.GetString(buffer);
    }

    public string ReadStringNumber(int len)
    {
        byte[] buffer = new byte[len];
        buffer = reader.ReadBytes(len);
        return Encoding.Default.GetString(buffer);
    }


    public byte[] ReadBytes()
    {
        int len = ReadInt();
        return reader.ReadBytes(len);
    }

    public byte[] ReadBytesOnly(int length)
    {
        return reader.ReadBytes(length);
    }

    public byte[] ReadBytesNumber(int len)
    {
        return reader.ReadBytes(len);
    }

    //public LuaByteBuffer ReadBuffer()
    //{
    //    byte[] bytes = ReadBytes();
    //    return new LuaByteBuffer(bytes);
    //}

    public byte[] ToBytes()
    {
        writer.Flush();
        return stream.ToArray();
    }

    public void Flush()
    {
        writer.Flush();
    }

    // 判断还有多少数据可读, 只供可读流去操作
    public int Avail()
    {
        return (int)stream.Length;
    }

    public void SeekCurrent(long offset)
    {
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
    }
}
