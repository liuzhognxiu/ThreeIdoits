require "World/Player/PlayerModel"
require "World/Player/PlayerEntity"

PlayerTest = class(PlayerEntity)

local this = {}

--[[
countLabel表的创建是为了能让c#的awake, ondestroy能够回调到！
表名是prefab的名字，luaframework是根据prefab名进行查询的
]]--
countLabel = {}

this.super = PlayerEntity
function PlayerTest:ctor()
	this.super:ctor("PlayerTest")
	this.gameObject = nil;
	this.transform = nil;
end

function PlayerTest:init(Parent)		
	this.PlayerModel = PlayerModel.new(1);
	this.PlayerModel:setPlayerName("TestMianPlayer")
	
	this.gameObject = GameObject.Instantiate(resMgr:LoadPrefab("Prefabs/TestMianPlayer"))	
	
	this.gameObject.name = "MainPlayer"

	this.transform = this.gameObject.transform	
	this.transform.localPosition = Vector3.zero	

	this.transform.parent = Parent
end