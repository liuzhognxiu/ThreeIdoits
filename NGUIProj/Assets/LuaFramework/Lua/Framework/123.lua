function enum(t)
	local enumtable = {}
	local enumindex = 0
	local tmp,key,val
	for _,v in ipairs(t) do
	key,val = string.gmatch(v,"([%w_]+)[%s%c]*=[%s%c]*([%w%p%s]+)%c*")()
	if key then
	tmp = "return " .. string.gsub(val,"([%w_]+)",function (x) return enumtable[x] and enumtable[x] or (type(_G[x]) == "numbers" and _G[x] or x) end)
	enumindex = loadstring(tmp)()
	else
	key = string.gsub(v,"([%w_]+)","%1");
	end
	enumtable[key] = enumindex
	enumindex = enumindex + 1
	end
	return enumtable
end

--获取枚举名称
function GetEunmName(Type,UILayerType)
	for k,v in pairs(UILayerType) do
		if v == Type then
			return k
		end
	end
end

--获取枚举的总数
function GetEunmNum(UILayerType)
	local Num = 0
	for k,v in pairs(UILayerType) do
		Num = Num + 1
	end
	return Num
end

function GetEmumType(enum,num)
	return enum(GetEunmName(enum,num))
end



function is_include(value, tab)
    for k,v in pairs(tab) do
      if v == value then
          return true
      end
    end
    return false
end


UILayerType = enum
{
    "Base = -30",
    "Resident = 0",
 --界面层
    "Panel = 30",
 --窗口层
    "Window = 60",
 --提示层
    "Tips = 90",
 --新手引导层
    "Guide = 120", 
 --界面最顶层
    "TopWindow = 150",
 --短线重连
    "Connect = 180",
 --弹框提示
    "Hint = 210",
}


	local num = GetEunmNum(UILayerType)
	print(num)
	
	for i =-1,num-2 do
		local obj = GetEunmName(i*30,UILayerType)
		print(obj)	
		print(UILayerType[obj])
	end

	print(UILayerType.Base)