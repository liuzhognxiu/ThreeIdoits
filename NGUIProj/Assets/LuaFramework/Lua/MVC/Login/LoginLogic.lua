local cjson = require("cjson")
require "Framework/LuaLogic"
require "Tasks/ConnectTask"
require "Tasks/LoginTask"
require "Framework/TaskQueue"

LoginLogic = class(LuaLogic)

local this = {}
local IPADDR = "10.43.0.206:10000"

-----------------------login callback start
local LoginLogicCallback = {}

function LoginLogicCallback.connect(data)
	print('LoginLogicCallback.connect Error Code is: ' .. data)
end

function LoginLogicCallback.login( data )
	print('LoginLogicCallback.login Error Code is: ' .. data)
end
-----------------------login callback end

function LoginLogic:ctor()
	NetWriter.SetUrl(IPADDR);
	this.username = nil;
	this.password = nil;
	this.channel = nil;
	this.loginCallback = self.loginCallback;
end

function LoginLogic:Initialize( view )
	-- body
end

function LoginLogic:OnLogin( username, password, channel )
	-- if not NetIsConnected() then
	-- 	TaskQueue.AddTask(ConnectTask.new(LoginLogicCallback.connect))
	-- end

	TaskQueue.AddTask(LoginTask.new(username, password, channel, LoginLogicCallback.login))
end

function LoginLogic:OnRegister( username, password, channel )

	print('............. Register')
end

