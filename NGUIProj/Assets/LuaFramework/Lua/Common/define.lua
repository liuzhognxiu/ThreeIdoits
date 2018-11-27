
CtrlNames = {
	Prompt = "PromptCtrl",
	Message = "MessageCtrl"
}

PanelNames = {
	"PromptPanel",	
	"MessagePanel",
}

--协议类型--
ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}

MapCellWidth = 100
MapCellHeight = 100
--当前使用的协议类型--
TestProtoType = ProtocalType.BINARY;

IsDebug = true;

Util = LuaFramework.Util;
AppConst = LuaFramework.AppConst;
LuaHelper = LuaFramework.LuaHelper;
ByteBuffer = LuaFramework.ByteBuffer;

resMgr = LuaHelper.GetResManager();
panelMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetSoundManager();
networkMgr = LuaHelper.GetNetManager();

Color = UnityEngine.Color
WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;

DontDestroyManager = DontDestroyManager.Instance;
PomeloGameManager = PomeloGameManager.Instance;
PomeloNetworkManager = PomeloNetworkManager.Instance;
Net = Net.Instance;
Utility = Utility;
TableManager = TableManager.Instance;
MapManager = MapManager.Instance;
UILuaTools = UILuaTools
PlayerPrefs = PlayerPrefs

Animation = UnityEngine.Animation

DelegateFactory = DelegateFactory