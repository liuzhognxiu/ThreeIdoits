require "Common/define"

local cjson = require("cjson")
require("3rd/pblua/LogicMsg_pb")
require("3rd/pblua/gamecmd_pb")

LoginTask = class(Task)

local this = {}

function LoginTask:ctor( username, password, channel, callback )
	this.username = username
	this.password = password
	this.channel = channel
	this.callback = callback
end

function LoginTask:Perform()
	local msg = LogicMsg_pb.Login();
	msg.account = this.username;
	msg.pwd = this.password;
	local pb_data = msg:SerializeToString();
	local buffer = ByteBuffer.New();
	buffer:WriteBuffer(pb_data);
	msg:ParseFromString(pb_data);
	Net:Send(GameCmd_pb.LOGIN, GameCmd_pb.LOGIN_RESP, self.callback, buffer);
end

function LoginTask.callback( data )
	if (data ~= nil) then
		local msg = LogicMsg_pb.LoginResp();
		msg:ParseFromString(data)
		print('login callback data is: '.. msg.result .. ' Account id ' .. msg.account_id)
	end
end