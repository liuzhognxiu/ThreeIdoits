require "MVC/Login/LoginView"
require "MVC/test/testView"
require "MVC/Map/MapView"

ViewBinding = {}
local this= {}

function ViewBinding.init( )
	this.bindings = {};
	this.bindings["LoginView"] = LoginView.new()
	this.bindings["TestView"] = testView.new()
	--this.bindings["MapView"] = MapView.new();
end

function ViewBinding.Add( name, obj )
	if (not ContainsInTable(bindings, name)) then
		this.bindings[name] = obj
	end
end

function ViewBinding.Get(name)
	local view = nil
	if (ContainsInTable(this.bindings, name)) then
		view = this.bindings[name]
	end
	return view;
end