require "Framework/lua_base"
require "Framework/EventController"

LuaLogic = class()
local this = {}

function LuaLogic:ctor()
	-- this.m_itemSources = nil
	self.m_eventController = EventController.new();
end

function LuaLogic:ItemSource(notifyPropChanged)
	notifyPropChanged:SetEventController(self.m_eventController);
	-- this.m_itemSources = notifyPropChanged;
end

function LuaLogic:SetBinding( key, action )
	if self.m_eventController:ContainsEvent(key) then
		return;
	end

	self.m_eventController:AddEventListener(key, action);
end

function LuaLogic:Release()
	-- if this.m_itemSources then
	-- 	this.m_itemSources:RemoveEventController(this.m_eventController);
	-- end

	self.m_eventController:Cleanup();
end





