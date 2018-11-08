require "Common/enumDefine"
layerManager = {}

function layerManager:layerInit()
	self.mLayerDic = {}
	self.mParent = GameObject.Find("UI Root")
	--GameObject.DontDestroyOnLoad(mParent)	
	local num = GetEunmNum(UILayerType)
	for i = -1, num-2 do
		local obj = GetEunmName(i*30,UILayerType)		
		if is_include(UILayerType[obj],self.mLayerDic) then
			self.mLayerDic[UILayerType[obj]] = self:CreateGameObjerct(obj,UILayerType[obj])
		else
			table.insert(self.mLayerDic,UILayerType[obj],self:CreateGameObjerct(obj,UILayerType[obj]))
		end
	end
end

--创建层级对应的父物体
function layerManager:CreateGameObjerct(name,UILayerType)
	local layer = GameObject.New()
	layer.transform.name = name
	layer.transform.parent = self.mParent.transform;
    layer.transform.localPosition = Vector3(0, 0, UILayerType * -1);
    layer.transform.localScale = Vector3.one;
	return layer
end

function layerManager:SetLayer(currentGameObject,LayerType)		
	local t = LayerType
	local layerTypeName = GetEunmName(LayerType,UILayerType)
	local layerObj =  self.mLayerDic[LayerType]
	local depth = t
		
	if Utility.GetMaxDepth(layerObj,depth) == false then
		 depth = t
	end
	if depth<t then
		depth = t
	end	
	currentGameObject.transform.parent = layerObj.transform
	local panelarr = UILuaTools.GetComponentsInChildren(currentGameObject)
	
	for i=0,panelarr.Length-1 do
		local panel = panelarr[i]
		depth = depth+ 1
		panel.depth = depth;
	end
end