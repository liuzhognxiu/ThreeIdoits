require "Framework/lua_base"

EventCenter = class()

local this = {}

function EventCenter:ctor()
	this.m_eventActions = {}
end

function EventCenter:Add( eventId, handler )
	if not ContainsInTable(this.m_eventActions, eventId) then
		this.m_eventActions[eventId] = {}	
	end

	table.insert(this.m_eventActions[eventId], handler)
end

function EventCenter:Remove( eventId, handler )
	local handlers = this.m_eventActions[eventId];
	if handlers ~= nil then
		for k, v in pairs(handlers) do
			if v == handler then
				handlers[k] = nil;
			end
		end
	end
end

function EventCenter:TriggerEvent(eventId, ...)
	local handlers = this.m_eventActions[eventId];
	if (handlers ~= nil) then
		for k, v in pairs(handlers) do
			v(...);
		end
	end
end