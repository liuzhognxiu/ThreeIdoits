require "Framework/ViewBase"
require "MVC/Map/MapLogic"

MapView = class(ViewBase)
local this = {}


function MapView.Awake(obj)
	-- body
end

function MapView:ctor()
	self.gameObject = nil;
	self.transform = nil;
end

function MapView:init(  )
	self.logic = MapLogic.new();
	self.logic:Initialize(self)

	self:InitUI()
	--init ui
end

function MapView:InitUI( )
	self.root = GameObject.Find("Center")
	self.gameObject = resMgr:LoadPrefab("Prefabs/MapView", self.root);
	self.transform = self.gameObject.transform;
end

function MapView:UpdateWidth( width )
	log("width is: " .. width)	
	self.logic:InitDefaultMap()
end

function MapView:UpdateHeight( height )
	log("height is: " .. height)
	self.logic:InitDefaultMap()
end
