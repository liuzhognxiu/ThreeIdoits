
--输出日志--
function log(str)
	if IsDebug then
    	Util.Log(str);
	end
end

--错误日志--
function logError(str) 
	Util.LogError(str);
end

--警告日志--
function logWarn(str) 
	Util.LogWarning(str);
end

--查找对象--
function find(str)
	return GameObject.Find(str);
end

function destroy(obj)
	GameObject.Destroy(obj);
end

function newObject(prefab)
	return GameObject.Instantiate(prefab);
end

--创建面板--
function createPanel(name)
	PanelManager:CreatePanel(name);
end

function child(str)
	return transform:FindChild(str);
end

function subGet(childNode, typeName)		
	return child(childNode):GetComponent(typeName);
end

function findPanel(str) 
	local obj = find(str);
	if obj == nil then
		error(str.." is null");
		return nil;
	end
	return obj:GetComponent("BaseLua");
end

function ContainsInTable(t, key)
	t = t or {}
	for k,v in pairs(t) do
		if k == key then
			return true;
		end
	end

	return false;
end

function CleanupTable( t )
	if t ~= nil then
		for i = #t, 1, -1 do
			if t[i] ~= nil then
				table.remove(t)
			end
		end
	end
end

function RemoveFromTableBykey( t, key )
	local i = 1;
	for k, v in pairs(t) do 
		if k == key then
			table.remove(t, i);
			break;
		end
		i = i + 1
	end
end

function findChildRecursively(trans, str)
	local child = trans:Find(str)

	if child ~= nil then
		return child
	end

	for m = 1, trans.childCount do
		local c = trans:GetChild(m - 1)
		local child = findChildRecursively(c, str)
		if child ~= nil then
			return child;
		end
	end

	return child;
	
end

function tableCount( tbData )
	local count = 0
	if tbData then
		for k,v in pairs(tbData) do
			count = count + 1
		end
	end

	return count
end

function NetIsConnected( )
	return PomeloNetworkManager.IsConnected
end

function Connect( address, port, callback )
	PomeloNetworkManager:Connect(address, port, callback)
end

--枚举相关
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
function GetEunmName(Type,EnumName)
	for k,v in pairs(EnumName) do
		if v == Type then
			return k
		end
	end
end

--获取枚举的总数
function GetEunmNum(enumType)
	local Num = 0
	for k,v in pairs(enumType) do
		Num = Num + 1
	end
	return Num
end
------------------------------------------------------------------------------------------------------------------------------------------------------------
function is_include(value, tab)
    for k,v in pairs(tab) do
      if v == value then
          return true
      end
    end
    return false
end

function ToStringEx(value)
    if type(value)=='table' then
        return TableToStr(value)
    elseif type(value)=='string' then
        return "\'"..value.."\'"
    else
        return tostring(value)
    end
end

--使用的时候是这个
function TableToStr(t)
    if t == nil then return "" end
    local retstr= "{"

    local i = 1
    for key,value in pairs(t) do
        local signal = ","
        if i==1 then
            signal = ""
        end

        if key == i then
            retstr = retstr..signal..ToStringEx(value)
        else
            if type(key)=='number' or type(key) == 'string' then
                retstr = retstr..signal..'['..ToStringEx(key).."]="..ToStringEx(value)
            else
                if type(key)=='userdata' then
                    retstr = retstr..signal.."*s"..TableToStr(getmetatable(key)).."*e".."="..ToStringEx(value)
                else
                    retstr = retstr..signal..key.."="..ToStringEx(value)
                end
            end
        end

        i = i+1
    end

    retstr = retstr.."}"
    return retstr
end

function StrToTable(str)
	if str == nil or type(str) ~= "string" then
		return
	end
	local ret = loadstring("return "..str)()
	return ret
 end
 