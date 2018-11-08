using UnityEngine;
using System.Collections;

public class PomeloPackage {
    public string Route { get; set; }
    public LuaInterface.LuaFunction luaFunc { get; set; }
    public LuaInterface.LuaTable SendParams { get; set; }
    public string ReturnData { get; set; }
}
