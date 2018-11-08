require "Framework/NotifyPropChanged"

LoginModel = class(NotifyPropChanged)

local this = {}

function LoginModel:ctor(  )
	this.username = nil
	this.password = nil;
	this.channel = nil;
end