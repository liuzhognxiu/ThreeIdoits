require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"
require "3rd/pblua/goods_info_pb"
require "3rd/pblua/mapEditor_pb"

local lpeg = require "lpeg"

local json = require "cjson"
local util = require "3rd/cjson/util"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

require "Logic/LuaClass"
require "Logic/CtrlManager"
require "Common/functions"
require "Controller/PromptCtrl"
require "Logic/GameData"
require "Logic/LuaGameManager"
require "MVC/test/testView"
require "MVC/Login/LoginView"
require "Common/ViewBinding"
require "Framework/UIManager"
require "Framework/layerManager"
require "World/Map/SceneManager"
require "World/Player/PlayerTest"
require "MVC/Player/PlayerModeldata"

--管理器--
Game = {};
local this = Game;
this.SceneMgr = nil
local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

function Game.InitViewPanels()
	for i = 1, #PanelNames do
		require ("View/"..tostring(PanelNames[i]))
	end
end

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 2012;
    AppConst.SocketAddress = "127.0.0.1";
    networkMgr:SendConnect();
    --注册LuaView--
    this.InitViewPanels();

	layerManager:layerInit()
    
    --  CtrlManager.Init();
    -- local ctrl = CtrlManager.GetCtrl(CtrlNames.Prompt);
    -- if ctrl ~= nil and AppConst.ExampleMode == 1 then
    --     ctrl:Awake();
    -- end

    -- local test = testView.new();
    -- test:init();
    this.InitGameManager()

    ViewBinding.init()
    
    -- local Playerdata =  Utility.LoadTextFile(UnityEngine.Application.persistentDataPath.."/PlayerData",true)
    -- if Playerdata ~= nil then
    --     this.GameManager.Player:SetTable(StrToTable(Playerdata))
    --     UIManager.Create("UItips")
	--     UIManager.GetPanel("UItips"):SelfWritingText(this.GameManager.Player:GetTable()['name'],"今天也要加油鸭")
    -- else
    --     UIManager.Create("UICreatePlayer")
    -- end

    UIManager.Create("UISelectNpcList")
    logWarn('LuaFramework InitOK--->>>');
end

function Game.InitGameManager()
    this.GameManager = nil
    this.GameManager = LuaGameManager
    this.GameManager:Init()
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
