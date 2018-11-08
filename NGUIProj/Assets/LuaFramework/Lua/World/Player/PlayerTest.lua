require "World/Player/PlayerModel"
require "World/Player/PlayerEntity"

PlayerTest = class(PlayerEntity)

local this = {}

--[[
countLabel��Ĵ�����Ϊ������c#��awake, ondestroy�ܹ��ص�����
������prefab�����֣�luaframework�Ǹ���prefab�����в�ѯ��
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