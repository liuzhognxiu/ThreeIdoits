UINPCEvent = Class(ViewBase)
local this = {} 
function UINPCEvent:ctor()
end
function UINpcEvent:Reset()
    if this.gameObject ~= nil then
        GameObject.Destory(this.gameObject)

        this.gameObject = nil
    end
end
function UINPCEvent:init()   
    this.btnBehaviour = nil
    this.root = GameObject.Find('UI Root')
    this.gameObject = resMgr:LoadPrefab("Prefabs/UINPCEvent",this.root)
    this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')
    this.transform = this.gameObject.transform
    
    this.spNpcHead = findChildRecursively(this.transform,'View/sp_NpcHead').gameObject:GetComponent('UISprite')
    
    this.lbNpcName = findChildRecursively(this.transform,'View/sp_NpcHead/lb_NpcName').gameObject:GetComponent('UILabel')
    
    this.lbPosition = findChildRecursively(this.transform,'View/NpcProperty/Position/lb_Position').gameObject:GetComponent('UILabel')
    
    this.lbNum = findChildRecursively(this.transform,'View/NpcProperty/Loyalty/lb_Num').gameObject:GetComponent('UILabel')
    layerManager:SetLayer(this.gameObject,UILayerType.Window)
end
function UINPCEvent.OnDestroy()
    this.UINPCEventlogic:Release()
end
