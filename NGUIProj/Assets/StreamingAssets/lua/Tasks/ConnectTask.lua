require "Framework/Task"
require "Framework/TaskQueue"
local cjson = require("cjson")

ConnectTask = class(Task)
local this = {}

function ConnectTask:ctor( callback )
	this.callback = callback
end

function ConnectTask:Perform(  )
	PomeloNetworkManager:Connect('127.0.0.1', 3014, self.callback)
end

function ConnectTask.callback( data )
	local decodedData=cjson.decode(data)
	print('Error Code is: ' .. decodedData.code)
	if decodedData.code ~= 200 then
		TaskQueue.Abort()
	else
		this.callback(decodedData.code);
		TaskQueue.TaskFinished()
	end
end