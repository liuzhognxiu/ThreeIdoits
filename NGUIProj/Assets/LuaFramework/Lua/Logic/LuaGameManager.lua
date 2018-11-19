require "Framework/EventCenter"
require "MVC/Player/PlayerModeldata"


local csv = require('Framework/CSVTools')

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
	this.Player = PlayerModeldata.new()
	self:InitNPCData()
end

function LuaGameManager:InitNPCData()
	local NPCTable = loadCsvFile("CSVData/NPC")
	GameData.NPCTable = NPCTable
end
