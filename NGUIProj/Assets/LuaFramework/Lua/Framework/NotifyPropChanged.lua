require "Framework/EventController"

NotifyPropChanged = class()
local this = {}

function NotifyPropChanged:ctor(  )
	self.m_uiBinding = nil;
end

function NotifyPropChanged:SetEventController( eventController )
	if self.m_uiBinding then
		logError('self NotifyPropChanged has been binded!');
		return;
	end

	self.m_uiBinding = eventController;
end

function NotifyPropChanged:OnPropertyChanged( propertyName, ... )
	if self.m_uiBinding then 
		self.m_uiBinding:TriggerEvent(propertyName, ...);
	else
		logError('NotifyPropChanged has not been binded!');
	end
end

function NotifyPropChanged:RemoveEventController()
	if self.m_uiBinding then
		self.m_uiBinding = nil;
	end
end