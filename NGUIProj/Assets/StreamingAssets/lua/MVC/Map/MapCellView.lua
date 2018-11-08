require "MVC/Map/MapCellLogic"
require "3rd/pblua/mapEditor_pb"

MapCellView = class(ViewBase)
local this = {}

function MapCellView:ctor(  )
	
end

function MapCellView:init( x, y )
	self.logic = MapCellLogic.new()
	self.logic:Initialize(self, x , y)

	self:InitUI(x, y)
end

function MapCellView:InitUI( x, y )
	self.root = GameObject.Find("MapView")
	self.m_cell = resMgr:LoadPrefab("Prefabs/MapCell", self.root);
	self.m_transform = self.m_cell.transform;
	self.m_transform.localPosition = Vector3(x*MapCellWidth, y * MapCellHeight, 0);
end

function MapCellView:UpdateStatus( status )
	if status == mapEditor_pb.Normal then
		log("aaaaaaaaaaaa")
		self.m_transform:GetComponent('UISprite').spriteName = "normal";
	elseif status == mapEditor_pb.Block then
		log("bbbbbbbbbbbb")
		self.m_transform:GetComponent('UISprite').spriteName = "block";
	end
end