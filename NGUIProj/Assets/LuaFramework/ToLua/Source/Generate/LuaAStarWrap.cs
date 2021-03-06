﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaAStarWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaAStar), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("Search", Search);
		L.RegFunction("New", _CreateLuaAStar);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaAStar(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				LuaAStar obj = new LuaAStar();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: LuaAStar.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaAStar obj = (LuaAStar)ToLua.CheckObject<LuaAStar>(L, 1);
			CSMap arg0 = (CSMap)ToLua.CheckObject<CSMap>(L, 2);
			obj.Init(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Search(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaAStar obj = (LuaAStar)ToLua.CheckObject<LuaAStar>(L, 1);
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
			System.Collections.Generic.List<CSCell> o = obj.Search(arg0, arg1);
			ToLua.PushSealed(L, o);
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
			ToLua.PushObject(L, LuaAStar.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

