
function Utils.split( str,reps )
    local resultStrList = {}
    string.gsub(str,'[^'..reps..']+',function ( w )
        table.insert(resultStrList,w)
    end)
    return resultStrList
end

--用于获取csv表
function Utils.loadCsvFile(filePath) 
    -- 读取文件
    local data = resMgr:LoadText(filePath);
    print(data)
    -- 按行划分
    local lineStr = Utils.split(data, '\n\r');
 
    --[[
        第一行是字段，第二行是类型，第三行是描述
    ]]
    local titles = string.split(lineStr[1], ",");
    local types = string.split(lineStr[2], ",");
    local ID = 1;
    local arrs = {};
    local fillContent
    for i = 4, #lineStr, 1 do
        -- 一行中，每一列的内容
        local content = string.split(lineStr[i], ",");
 
        -- 以标题作为索引，保存每一列的内容，取值的时候这样取：arrs[1].Title
        arrs[ID] = {};
        for j = 1, #titles, 1 do
        	if types[j] == "string" then
        		fillContent = content[j]
        	else 
        		fillContent = tonumber(content[j] == "" and "0" or content[j])
        	end
            arrs[ID][titles[j]] = fillContent
        end
 
        ID = ID + 1;
    end
 
    return arrs;
end
