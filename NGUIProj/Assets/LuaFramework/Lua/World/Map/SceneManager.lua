SceneManager = class()

local this = {}

function SceneManager:Ctor()
	this.MainPlayer = nil 
	this.Map = nil 
	this.gameObject = nil 
	this.transform = nil 
	this.world = nil 
	this.ScaleMap = nil
end

function SceneManager:SetMainPlayer( MainPlayer )
	this.MainPlayer = MainPlayer
end

function SceneManager:SetMap( Map)
	this.Map = Map
end

function SceneManager:SetWorld(world )
	this.world = world
end

function SceneManager:GetScaleMap()
	return	this.ScaleMap 
end

function SceneManager:GetSelfGameObject()
	return this.gameObject
end


function SceneManager:CreatrScene()
	if this.gameObject == nil then
	this.gameObject = GameObject.New()
	this.transform = this.gameObject.transform 
	this.gameObject.name = "Scene"
		if  this.ScaleMap == nil then
			this.ScaleMap = GameObject.New()
			this.ScaleMap.name = "ScaleMap"
			this.ScaleMap.transform.parent = this.transform		
			this.MainPlayer:init(this.transform)
		end
	end
end