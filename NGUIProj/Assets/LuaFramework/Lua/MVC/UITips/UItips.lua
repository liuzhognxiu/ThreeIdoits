require "Framework/ViewBase"

UItips = class(ViewBase)

local this = {}

function UItips:ctor()
end

function UItips:Reset()
    if this.gameObject ~= nil then
        GameObject.Destory(this.gameObject)
        this.gameObject = nil
    end

    -- if this.luaBehavior ~= nil then
    --     this.luaBehavior:RemoveClick(this.btnLogin)
	-- 	this.luaBehavior = nil;
    -- end
end

function UItips:init()
    this.gameObject = resMgr:loadPrefab("Prefabs/NpcDialog")
    this.luaBehavior = this.gameObject:GetComponent('LuaBehavior')
    this.transform = this.gameObject.transform

    this.sp_npcHead = findChildRecursively(this.transform,"sp_npcHead").gameObject:GetComponent('UISprite')
    this.sp_bg = findChildRecursively(this.transform,"sp_bg").gameObject:GetComponent('UISprite')
    this.lb_NpcName = findChildRecursively(this.transform,"lb_NpcName").gameObject:GetComponent('UILabel')
    this.lb_NpcMessage = findChildRecursively(this.transform,"lb_NpcMessage").gameObject:GetComponent('UILabel')

    this.luaBehavior:AddClick(this.sp_bg.transform,self.OnNextClick)
    self:AddCollider()

    layerManager:SetLayer(this.gameObject,UILayerType.Tips)
end

function UItips:AddCollider()
    local box = UILuaTools.AddCollider(this.gameObject)
    this.luaBehavior:AddClick(box,self.ClosePanel)
end

function UItips.ClosePanel(go)
	UIManager.Close("UItips")
end

function UItips:Cloase( ... )
    self:Reset()
end