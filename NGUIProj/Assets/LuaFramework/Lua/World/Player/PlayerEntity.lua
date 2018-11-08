require "Framework/lua_base"

PlayerEntity = class()
local this = {}

function PlayerEntity:ctor(name)
	this.Behaviour= nil
	this.EntityName = name
	this.PlayerModel = nil
end

function PlayerEntity:init()
	
end

function PlayerEntity:Create()
	
end

function PlayerEntity:Destory()

end


 