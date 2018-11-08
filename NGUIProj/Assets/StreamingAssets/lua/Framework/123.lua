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

--��ȡö������
function GetEunmName(Type,UILayerType)
	for k,v in pairs(UILayerType) do
		if v == Type then
			return k
		end
	end
end

--��ȡö�ٵ�����
function GetEunmNum(UILayerType)
	local Num = 0
	for k,v in pairs(UILayerType) do
		Num = Num + 1
	end
	return Num
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
 --�����
    "Panel = 30",
 --���ڲ�
    "Window = 60",
 --��ʾ��
    "Tips = 90",
 --����������
    "Guide = 120", 
 --�������
    "TopWindow = 150",
 --��������
    "Connect = 180",
 --������ʾ
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