using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaFramework;

namespace LuaData
{
    public class SocketPackage
    {
        private int m_reqId = 0;
        private int m_respId = 0;
        private Action<ByteBuffer> m_callback;
        private ByteBuffer m_content;

    }
}
