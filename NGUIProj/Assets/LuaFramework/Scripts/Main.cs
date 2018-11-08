using UnityEngine;
using System.Collections;
using ProtoBuf;

namespace LuaFramework {
    /// <summary>
    /// 框架主入口
    /// </summary>
    public class Main : MonoBehaviour {
        private static Main _instance = null;
        public static Main Instance
        {
            get
            {
                return _instance;
            }
        }
        void Start() {
            byte[] data = TableManager.Instance.ReadDataConfig("goods_info.data");
            UFramework.Goods_Info_Array gia = UFramework.Goods_Info_Array.Parser.ParseFrom(data);
            AppFacade.Instance.StartUp();   //启动游戏
        }

        private void Update()
        {
            Timer.Instance.Update();
        }
    }
}