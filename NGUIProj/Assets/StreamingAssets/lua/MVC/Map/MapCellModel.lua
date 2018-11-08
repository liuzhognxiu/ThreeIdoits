require "Framework/NotifyPropChanged"

MapCellModel = class(NotifyPropChanged)
local this ={}

function MapCellModel:ctor(  )
	self.m_status = Normal
	self.x = -1
	self.y = -1
	self.MAPCELLMODEL_STATUS = "MAPCELLMODEL_STATUS"
end

function MapCellModel:getStatus(  )
	return self.m_status
end

function MapCellModel:setStatus( status )
	self.m_status = status
	self:OnPropertyChanged(self.MAPCELLMODEL_STATUS, self.m_status)
end

function MapCellModel:getX( )
	return self.x;
end

function MapCellModel:setX( data )
	self.x = data
end

function MapCellModel:getY(  )
	return self.y;
end

function MapCellModel:setY( data )
	self.y = data;
end