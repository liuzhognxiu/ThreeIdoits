﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class MapManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MapManager), typeof(System.Object));
		L.RegFunction("ReadMapData", ReadMapData);
		L.RegFunction("LuaReadDataConfig", LuaReadDataConfig);
		L.RegFunction("New", _CreateMapManager);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMapManager(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				MapManager obj = new MapManager();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: MapManager.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadMapData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MapManager obj = (MapManager)ToLua.CheckObject<MapManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			byte[] o = obj.ReadMapData(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaReadDataConfig(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MapManager obj = (MapManager)ToLua.CheckObject<MapManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			LuaInterface.LuaByteBuffer o = obj.LuaReadDataConfig(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, MapManager.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

