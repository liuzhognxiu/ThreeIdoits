require "Common/ViewBinding"

UIManager = {}

function UIManager.Create(name)
	if (name ~= nil) then
		ViewBinding.Get(name):Show()
	else
		logError("can't find obj by name: " .. name)
	end
	
end

function UIManager.Close(name)
	if (name ~= nil) then
		ViewBinding.Get(name):Close()
	else
		logError("can't find obj by name: " .. name)
	end
end

function UIManager.GetPanel(name)
    if name ~= nil then
		if ViewBinding.Get(name)==nil then
			return false				
		end
		return ViewBinding.Get(name)
	end
end

function UIManager.CloseAllPanel()
	for i = 0,#ViewBinding.bindings do
		ViewBinding.Get(ViewBinding.bindings[i].name):Close()
	end
end