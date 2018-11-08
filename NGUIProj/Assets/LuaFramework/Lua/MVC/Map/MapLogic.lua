require "Framework/LuaLogic"
require "MVC/Map/MapModel"
require "MVC/Map/MapCellView"

MapLogic = class(LuaLogic);

local this = {}

function MapLogic:ctor( )
	self.m_width = 0
	self.m_height = 0
	self.m_mapCellViews = {}
end

function MapLogic:Initialize( view )
	self:ItemSource(GameData.MapModel())
	local cbWidth = function ( width )
		self.m_width = width
		view:UpdateWidth(width)
	end
	local cbHeight = function ( height )
		self.m_height = height
		view:UpdateHeight(height)
	end

	self:SetBinding(GameData.MapModel().MAPMODEL_WIDTH, cbWidth);
	self:SetBinding(GameData.MapModel().MAPMODEL_HEIGHT, cbHeight);
end

function MapLogic:InitDefaultMap(  )
	for i = 1, self.m_width do
		for j = 1, self.m_height do
			local key = (i - 1) * 100000 + (j - 1)
			local cellView = MapCellView.new();
			cellView:init(i - 1, j - 1);
			if (not ContainsInTable(self.m_mapCellViews, key)) then
				self.m_mapCellViews[key] = cellView;
			end
		end
	end
end