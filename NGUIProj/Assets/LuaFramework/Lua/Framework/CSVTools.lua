---
--@module csv
local csv = {
    _VERSION     = '1.0.0',
    _DESCRIPTION = 'A Lua library for Changing the csv 2 table.',
}

---
--@function
local ResolveLine = function (str)
    local f, s = 1, 1
    if not string.find(str, ",") then
        return nil
    end

    local temp = {}
    while true do
        f = string.find(str, ",", f)
        if nil == f then
            table.insert(temp, string.sub(str, s, #str))
            break
        end
        table.insert(temp, string.sub(str, s, f-1))
        s = f + 1
        f = s
    end

    local data = {}
    local t_str = ""
    
    for key, var in ipairs(temp) do
        t_str = t_str .. var
        local _, count = string.gsub(t_str, "\"", "\"")
        if count % 2 == 0 then
            if count > 2 then
                t_str = string.sub(t_str, 2, #t_str-1)
            end
            t_str = tonumber(t_str) or t_str
            table.insert(data, t_str)
            t_str = ""
        else
            t_str = t_str .. ","
        end
    end

    return data
end

---
--@function [parent=#csv]
csv.decode = function (filename, key, title_line, data_line)

    --defult
    key = tostring(key)
    if key == "nil" then key = 0 end
    title_line = tonumber(title_line) or 0
    data_line = tonumber(data_line) or 1

    if title_line >= data_line then
        error(("line num error %s: %s"):format(title_line, data_line))
    end

    local file = nil
    if filename == nil then
        file = io.stdin
    else
        local err
        file, err = io.open(filename, "r")
        if file == nil then
            error(("Unable to read '%s': %s"):format(filename, err))
        end
    end


    local data = {}
    local t_title, t_data
    local is_finded = false
    --行号
    local line_num = 0
    for line in file:lines() do
        line_num = line_num + 1
        if line_num == title_line then
            t_title = ResolveLine(line)
            if t_title then
                for k, v in ipairs(t_title) do
                    if v == key then is_finded = true end
                end
            end

        elseif line_num >= data_line then
            t_data = ResolveLine(line)
            if t_data then
                if not is_finded or key == 0 then
                    local a = {}
                    for key, var in ipairs(t_data) do
                        a[var] = t_data[key]
                    end
                    --table.insert(data, a)
                    table.insert(data, t_data)
                else
                    local a = {}
                    for key, var in ipairs(t_data) do
                        a[t_title[key]] = var
                    end
                    data[a[key]] = a
                end
            end

        end
    end

    if filename ~= nil then
        file:close()
    end

    return data
end

return csv