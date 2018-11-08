
MapCellLogic = class(LuaLogic)
local this = {}

function MapCellLogic:ctor()
	-- body
end

function MapCellLogic:Initialize( view, x, y )
	self:ItemSource(GameData.CreateAndGetMapCell(x, y))
	local cbStatus = function ( status )
		view:UpdateStatus(status)
	end

	self:SetBinding(GameData.getMapCell(x, y).MAPCELLMODEL_STATUS, cbStatus);
end