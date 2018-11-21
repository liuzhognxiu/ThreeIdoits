require "MVC/test/testModel"
require "MVC/Login/LoginModel"
require "MVC/Map/MapModel"
require "MVC/Map/MapCellModel"
require "MVC/UIModel/SelectNpcModel"

GameData = {}

local this = GameData;
this.mapCells = {}
local m_testModel = nil;
function GameData.TestModel() 
	if m_testModel == nil then 
		m_testModel = testModel.new();
	end
	return m_testModel;
end

local m_loginModel = nil;

function GameData.LoginModel()
	if m_loginModel == nil then 
		m_loginModel = LoginModel.new()
	end

	return m_loginModel;
end

function GameData.MapModel()
	if this.mapModel == nil then
		this.mapModel = MapModel.new()
	end

	return this.mapModel;
end

function GameData.UpdateMapCells( cells )
	for k, v in ipairs(cells) do
		if (v.Status ~= nil) then
			local cell = GameData.CreateAndGetMapCell(v.X, v.Y);
			cell:setStatus(v.Status);
		end
	end

end

function GameData.CreateAndGetMapCell(x, y)
	local key = x * 100000 + y
	if ContainsInTable(this.mapCells, tostring(key)) then
		return this.mapCells[tostring(key)]
	end

	local cell = MapCellModel.new();
	this.mapCells[tostring(key)] = cell;
	return cell;
end


function GameData.getMapCell( x, y )
	local key = x * 100000 + y
	if ContainsInTable(this.mapCells, tostring(key)) then
		return this.mapCells[tostring(key)]
	end

	return nil
end

function GameData.ctor()
	
end

local m_SelectNpcModel = nil

function GameData.SelectNpcModel()
	if m_SelectNpcModel == nil then
		m_SelectNpcModel = SelectNpcModel.new()
	end
	return m_SelectNpcModel
end

local NPCTable = nil

local AIPlayertable = nil

local NpcEventTable = nil

local Promptipsword = nil
