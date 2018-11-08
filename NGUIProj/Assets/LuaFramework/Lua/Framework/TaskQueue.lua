TaskQueue = {}
local this = {}

local m_taskList = {}
local m_currentTask = nil

function TaskQueue.AddTask( task )
	if task ~= nil then
		table.insert(m_taskList, task)
	end

	if m_currentTask == nil then
		TaskQueue.ProcessNext()
	end
end

function TaskQueue.ProcessNext()
	local count = table.maxn(m_taskList);

	if count <= 0 then
		m_currentTask = nil
	end

	m_currentTask = m_taskList[1];
	table.remove(m_taskList, 1);
	if m_currentTask ~= nil then
		m_currentTask:Perform()
	end
end

function TaskQueue.TaskFinished()
	TaskQueue.ProcessNext();
end

function TaskQueue.Abort()
	if m_currentTask ~= nil then
		m_currentTask = nil
	end

	CleanupTable(m_taskList);
end