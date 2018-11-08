require "Framework/lua_base"
ViewBase = class()
local this = {}

function ViewBase:ctor(name)
	this.btnBehaviour= nil
	this.PanelName = name
end

function ViewBase:Show()
	self:init();
end

function ViewBase:init()
	
end

function ViewBase:Close()

end

function ViewBase:AddCollider()

end

 