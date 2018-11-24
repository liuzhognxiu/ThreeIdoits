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

    this.test_class_func();
    this.test_pblua_func();
    this.test_cjson_func();
    this.test_pbc_func();
    this.test_lpeg_func();
    this.test_sproto_func();
    coroutine.start(this.test_coroutine);

	layerManager:layerInit()
    --[[
    CtrlManager.Init();
    local ctrl = CtrlManager.GetCtrl(CtrlNames.Prompt);
    if ctrl ~= nil and AppConst.ExampleMode == 1 then
        ctrl:Awake();
    end

    local test = testView.new();
    test:init();
    ]]--
    ViewBinding.init()
    print("Create UItips")

    --创建tips面板代码
    UIManager.Create("UItips")
	UIManager.GetPanel("UItips"):SelfWritingText("请回答1988","所谓爱一个人，不是喜欢对方的体温，而是要跟对方的体温越来越接近。  所谓爱一个人是：即使对方一直折磨你，你想要一直讨厌对方，但怎么也讨厌不起来。所谓爱是，不是不讨厌，而是绝对不能讨厌的意思。  ")
    
    local testdata = {}
    testdata[1] = 1
    testdata[2] = 3
    testdata[3] = 5
    testdata[4] = 7
    testdata[5] = 8
    
    print(UnityEngine.Application.persistentDataPath)
    --测试存文档
    Utility.CreateTextFile(UnityEngine.Application.persistentDataPath.."/TestData",TableToStr(testdata),true)
    local ccc = Utility.LoadTextFile(UnityEngine.Application.persistentDataPath.."/TestData",true)
    -- 使用TIps消息提示      
    -- Utility.ShowCenterInfo("123456",UnityEngine.Color.blue)
    
    --read table
    -- local testTable = goods_info_pb.Goods_Info_Array()
    -- local data = TableManager:LuaReadDataConfig("goods_info.data")
    -- if testTable ~= nil and data ~= nil then
    --     testTable:ParseFromString(data)
    --     log("@@@@@@@@@@@@@@@@@@@@@@@ " ..  testTable.items[1].goods_id)
    -- end
    
    --读取地图数据测试
    -- local mapTable = mapEditor_pb.MapEditorData();
    -- local mapData = MapManager:LuaReadDataConfig("map_02.bytes")
    -- if mapTable ~= nil and mapData ~= nil then
    --     mapTable:ParseFromString(mapData)
    --     GameData.MapModel():setMapWidth(mapTable.Width);
    --     GameData.MapModel():setMapHeight(mapTable.Height);
    --     GameData.UpdateMapCells(mapTable.MapCells);

    --     -- for k, v in ipairs(mapTable.MapCells) do
    --     --     if (v.Status ~= nil) then
    --     --         local key = v.X * 100000 + v.Y
    --     --         log("GameData.getMapCell(key).status " .. GameData.getMapCell(v.X, v.Y):getStatus()); 
    --     --     end
    --     -- end

    --     local map = CSMap.New();
    --     map.Width = mapTable.Width;
    --     map.Height = mapTable.Height;
    --     map.Cells = {};
    --     for k, v in ipairs(mapTable.MapCells) do
    --         local cell = CSCell.New()
    --         cell.X = v.X;
    --         cell.Y = v.Y;
    --         cell.Value = v.Status;
    --         -- map.Cells.Add(cell);
    --         table.insert(map.Cells, cell)
    --     end
    --     LuaAStar.Instance:Init(map);
    --     local paths = LuaAStar.Instance:Search(Vector3(1, 1, 0), Vector3(7,8,0));
    --     local iter = paths:GetEnumerator();

    --     while iter:MoveNext() do
    --         local v = iter.Current
    --         log("PATH X: " .. v.X .. " Y: " .. v.Y);
    --     end

        --[[for k, v in ipairs(paths) do
            
        end--]]
    --     for i = 1,  mapTable.Width do
    --         for j = 1, mapTable.Height do
    --             local key = (i - 1) * 100000 + (j - 1);
    --             if GameData.getMapCell(key) ~= nil then
    --                 log("GameData.MapModel().getMapCell(key) " .. GameData.getMapCell(key).Status); 
    --             else
    --                 log("GameData.MapModel().getMapCell(key) aaaaaaaaaaa " .. key);
    --             end
    --         end
    --     end
	

	--CreateScene
	--CreatePlayer
	-- local TestPlayer = PlayerTest.new()		
	-- this.SceneMgr = SceneManager.new()
	-- this.SceneMgr:SetMainPlayer(TestPlayer)
	-- this.SceneMgr:CreatrScene()
    --end

    logWarn('LuaFramework InitOK--->>>');
end

function Game.test_closeLoginView(  )
    -- coroutine.wait(2)
    -- UIManager.Close("LoginView");
    -- coroutine.wait(1)
    -- UIManager.Create("LoginView");
	
	--local testView = UIManager.GetPanel("TestView")
	--testView:AddCollider(testView.gameObject,"TestView")
end

--测试协同--
function Game.test_coroutine()    

    logWarn("1111");
    coroutine.wait(1);	
    logWarn("2222");
	
    -- local www = WWW("http://bbs.ulua.org/readme.txt");
    -- coroutine.www(www);
    -- logWarn(www.text);    	

		Utility.ShowTips("我是左手边",UnityEngine.Color.red)
	coroutine.wait(0.3);
		Utility.ShowTips("我是左手边",UnityEngine.Color.blue)
	coroutine.wait(0.3);
		Utility.ShowTips("我是左手边",UnityEngine.Color.black)
	coroutine.wait(0.3);
		Utility.ShowTips("我是左手边",UnityEngine.Color.yellow)
	coroutine.wait(0.3);	
		Utility.ShowTips("我是左手边",UnityEngine.Color.red)	

end

--测试sproto--
function Game.test_sproto_func()
    logWarn("test_sproto_func-------->>");
    local sp = sproto.parse [[
    .Person {
        name 0 : string
        id 1 : integer
        email 2 : string

        .PhoneNumber {
            number 0 : string
            type 1 : integer
        }

        phone 3 : *PhoneNumber
    }

    .AddressBook {
        person 0 : *Person(id)
        others 1 : *Person
    }
    ]]

    local ab = {
        person = {
            [10000] = {
                name = "Alice",
                id = 10000,
                phone = {
                    { number = "123456789" , type = 1 },
                    { number = "87654321" , type = 2 },
                }
            },
            [20000] = {
                name = "Bob",
                id = 20000,
                phone = {
                    { number = "01234567890" , type = 3 },
                }
            }
        },
        others = {
            {
                name = "Carol",
                id = 30000,
                phone = {
                    { number = "9876543210" },
                }
            },
        }
    }
    local code = sp:encode("AddressBook", ab)
    local addr = sp:decode("AddressBook", code)
    print_r(addr)
end

--测试lpeg--
function Game.test_lpeg_func()
	logWarn("test_lpeg_func-------->>");
	-- matches a word followed by end-of-string
	local p = lpeg.R"az"^1 * -1

	print(p:match("hello"))        --> 6
	print(lpeg.match(p, "hello"))  --> 6
	print(p:match("1 hello"))      --> nil
end

--测试lua类--
function Game.test_class_func()
    LuaClass:New(10, 20):test();
end

--测试pblua--
function Game.test_pblua_func()
    local login = login_pb.LoginRequest();
    login.id = 2000;
    login.name = 'game';
    login.email = 'jarjin@163.com';
    
    local msg = login:SerializeToString();
    LuaHelper.OnCallLuaFunc(msg, this.OnPbluaCall);
end

--pblua callback--
function Game.OnPbluaCall(data)
    local msg = login_pb.LoginRequest();
    msg:ParseFromString(data);
    print(msg);
    print(msg.id..' '..msg.name);
end

--测试pbc--
function Game.test_pbc_func()
    local path = Util.DataPath.."lua/3rd/pbc/addressbook.pb";
    log('io.open--->>>'..path);

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)

    local addressbook = {
        name = "Alice",
        id = 12345,
        phone = {
            { number = "1301234567" },
            { number = "87654321", type = "WORK" },
        }
    }
    local code = protobuf.encode("tutorial.Person", addressbook)
    LuaHelper.OnCallLuaFunc(code, this.OnPbcCall)
end

--pbc callback--
function Game.OnPbcCall(data)
    local path = Util.DataPath.."lua/3rd/pbc/addressbook.pb";

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)
    local decode = protobuf.decode("tutorial.Person" , data)

    print(decode.name)
    print(decode.id)
    for _,v in ipairs(decode.phone) do
        print("\t"..v.number, v.type)
    end
end

--测试cjson--
function Game.test_cjson_func()
    local path = Util.DataPath.."lua/3rd/cjson/example2.json";
    local text = util.file_load(path);
    LuaHelper.OnJsonCallFunc(text, this.OnJsonCall);
end

--cjson callback--
function Game.OnJsonCall(data)
    local obj = json.decode(data);
    print(obj['menu']['id']);
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
