require "Framework/NotifyPropChanged"

MapModel = class(NotifyPropChanged)
local this = {}

function MapModel:ctor()
	self.m_width = 0
	self.m_height = 0
	self.MAPMODEL_WIDTH = "MAPMODEL_WIDTH"
	self.MAPMODEL_HEIGHT = "MAPMODEL_HEIGHT"
end

function MapModel:getMapWidth()
	return self.m_height;
end

function MapModel:setMapWidth( width )
	self.m_width = width
	--通知UI组件更新
	self:OnPropertyChanged(self.MAPMODEL_WIDTH, self.m_width);
end

function MapModel:getMapHeight(  )
	return self.m_height
end

function MapModel:setMapHeight( height )
	self.m_height = height
	self:OnPropertyChanged(self.MAPMODEL_HEIGHT, self.m_height)
end

-- function MapModel:( ... )
-- 	-- body
-- end