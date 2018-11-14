require "Framework/EventCenter"
require "MVC/Player/PlayerModeldata"

LuaGameManager = {}

local this = LuaGameManager

local m_eventDispatcher = nil

function LuaGameManager.GetEventDispatcher( )
	if m_eventDispatcher == nil then
		m_eventDispatcher = EventCenter.new();
	end

	return m_eventDispatcher;
end

function LuaGameManager:Init()
    print("-----------------------------------------")
	this.Player = PlayerModeldata.new()
end