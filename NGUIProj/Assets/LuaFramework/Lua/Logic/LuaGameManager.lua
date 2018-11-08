require "Framework/EventCenter"

LuaGameManager = {}

local this = LuaGameManager

local m_eventDispatcher = nil

function LuaGameManager.GetEventDispatcher( )
	if m_eventDispatcher == nil then
		m_eventDispatcher = EventCenter.new();
	end

	return m_eventDispatcher;
end