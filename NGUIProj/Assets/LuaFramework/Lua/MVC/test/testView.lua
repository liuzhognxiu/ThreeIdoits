require "Framework/lua_base"
require "MVC/test/testLogic"
require "Framework/ViewBase"

testView = class(ViewBase)

local this = {}

--[[
countLabel表的创建是为了能让c#的awake, ondestroy能够回调到！
表名是prefab的名字，luaframework是根据prefab名进行查询的
]]--
countLabel = {}

this.super = ViewBase
function testView:ctor()
	this.super:ctor("countLabel")
	this.gameObject = nil;
	this.transform = nil;
end

function testView:init()
	this.testLogic = testLogic.new();
	this.testLogic:Initialize(self);
	this.btnBehaviour = nil;	
	
	this.root = GameObject.Find("Center")
	this.gameObject = resMgr:LoadPrefab("Prefabs/countLabel", this.root);
	this.transform = this.gameObject.transform;
	this.btn = this.transform:Find("Button").gameObject;
	this.btnBehaviour = this.gameObject:GetComponent('LuaBehaviour');
	this.btnBehaviour:AddClick(this.btn, self.OnClick);
	this.countLabelgo = this.transform:Find("label").gameObject;
	this.countLabel = this.countLabelgo:GetComponent('UILabel');
	this.doubleLabelgo = this.transform:Find("double").gameObject;
	this.doubleLabel = this.doubleLabelgo:GetComponent('UILabel');

	this.addbtn1 = this.transform:Find("add1").gameObject;
	this.btnBehaviour:AddClick(this.addbtn1, self.AddEvent1);
	this.addbtn2 = this.transform:Find("add2").gameObject;
	this.btnBehaviour:AddClick(this.addbtn2, self.AddEvent2);
	this.triggerbtn = this.transform:Find("trigger").gameObject;
	this.btnBehaviour:AddClick(this.triggerbtn, self.TriggerEvent)
	this.removebtn1 = this.transform:Find("remove1").gameObject;
	this.btnBehaviour:AddClick(this.removebtn1, self.RemoveEvent1)

	this.text1 = findChildRecursively(this.transform, "value1"):GetComponent('UILabel')
	this.text1.text = "0"
	this.text2 = findChildRecursively(this.transform, "value2"):GetComponent('UILabel')
	this.text2.text = "0"

	--测试UIGridContainer
	this.ScrollUGC = findChildRecursively(this.transform,"ItemList"):GetComponent("LoopScrollView")

	self:UpdateCount(GameData.TestModel():getCount());
	self:UpdateDouble(GameData.TestModel():getDouble());
	--self:AddCollider()
end

function testView:AddCollider()
	local box = UILuaTools.AddCollider(this.gameObject)
	this.btnBehaviour:AddClick(box,self.ClosePanel)
end

function testView.ClosePanel(go)
	--UIManager.Close(this.PanelName)
end

function testView:UpdateDouble( data )
	this.doubleLabel.text = data;
end

-----------------------eventCallback callback start
local eventCallback = {}

function eventCallback.add1Callback(data)
	this.text1.text = tonumber(this.text1.text) + 1 .. ""

end

function eventCallback.login( data )
	print('LoginLogicCallback.login Error Code is: ' .. data)
end
-----------------------login callback end

function testView:UpdateCount( count )
	this.countLabel.text = count;
	--this.ScrollUGC.MaxShowCount = count
	--testlistdata，只是为了测试
--[[	local a1 = TestListData.New(1)
	local a2 = TestListData.New(2)
	local a3 = TestListData.New(3)
	local a = {}

	for i = 0 , count do
		local c = TestListData.New(i)
		table.insert(a,c)
	end
	this.ScrollUGC:Init(a,eventCallback.add1Callback)--]]
end

function testView.OnClick( go )
	GameData.TestModel():setCount(GameData.TestModel():getCount() + 1);
	GameData.TestModel():setDouble(GameData.TestModel():getDouble() + 2);
end

function testView.add1Callback()
	this.text1.text = tonumber(this.text1.text) + 1 .. ""
end

function testView.AddEvent1( go )
	LuaGameManager.GetEventDispatcher():Add(1, eventCallback.add1Callback)
end

function testView.RemoveEvent1( go )
	LuaGameManager.GetEventDispatcher():Remove(1, eventCallback.add1Callback)
end

function testView.AddEvent2( go )
	local callback = function()
		this.text2.text = tonumber(this.text2.text) + 2 .. ""
	end
	LuaGameManager.GetEventDispatcher():Add(1, callback)
end

function testView.TriggerEvent(go)
	LuaGameManager.GetEventDispatcher():TriggerEvent(1);
end


--[[
	执行回调函数，在资源被销毁的时候，清除绑定事件
]]--
function countLabel.OnDestroy()
	print(".........OnDestroy")
	this.testLogic:Release()
end
