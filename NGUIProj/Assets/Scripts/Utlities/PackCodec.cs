using UnityEngine;
using System.Collections;
using System.IO;
using Google.Protobuf;

public class PackCodec {

	static public byte[] Serialize (Google.Protobuf.IMessage msg)
    {
        byte[] result = null;

        if (msg != null)
        {
            using(var stream = new MemoryStream())
            {
                msg.WriteTo(stream);
                //Serializer.Serialize<T>(stream, msg);
                result = stream.ToArray();
            }
        }

        return result;
    }

    //static public T Deserialize<T>(byte[] message)
    //{
    //    T result = default(T);
    //    if (message != null)
    //    {
    //        using(var stream = new MemoryStream(message))
    //        {
    //            result = Serializer.Deserialize<T>(stream);
    //        }
    //    }

    //    return result;
    //}

    static public byte[] WriteMessage(byte[] message)
    {
        MemoryStream ms = null;

        using(ms = new MemoryStream())
        {
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);
            ushort msglen = (ushort)message.Length;
            writer.Write(msglen);
            writer.Write(message);
            writer.Flush();
            return ms.ToArray();
        }
    }
}
