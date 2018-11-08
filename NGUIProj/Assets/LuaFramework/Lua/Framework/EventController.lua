require "Framework/lua_base"

EventController = class()

local this = {}

function EventController:ctor()
	-- this.m_permanentEvents = {};
	self.m_theRouter = {}
end

function EventController:AddEventListener( eventType, handler )
	if (not ContainsInTable(self.m_theRouter, eventType)) then
		self.m_theRouter[eventType] = handler;
	end

	-- logWarn("############### EventController:AddEventListener " .. tableCount(self.m_theRouter) .. " " .. eventType)
end

function EventController:Cleanup()
	logWarn("before cleanup self.m_theRouter count is: " .. tableCount(self.m_theRouter))
	CleanupTable(self.m_theRouter);
end

function EventController:ContainsEvent( eventType )
	return ContainsInTable(self.m_theRouter, eventType);
end

function EventController:RemoveEventListener( eventType )
	RemoveFromTableBykey(self.m_theRouter, eventType)
end

function EventController:TriggerEvent( eventType , ...)
	-- logWarn("TriggerEvent self.m_theRouter count is: " .. tableCount(self.m_theRouter));
	if (ContainsInTable(self.m_theRouter, eventType)) then
		self.m_theRouter[eventType](...);
	else
		logError('TriggerEvent ' .. eventType .. ' error: types of parameters are not match.')
	end
end

