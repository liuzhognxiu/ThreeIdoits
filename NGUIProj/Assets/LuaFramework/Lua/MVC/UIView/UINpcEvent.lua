UINPCEvent = class(ViewBase)
 
local this = {} 

function UINPCEvent:ctor() 
    this.gameObject = nil
    this.transform = nil 
    this.luaBehavior = nil
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
    
    this.slidLoyalty = findChildRecursively(this.transform,'View/NpcProperty/Loyalty/slid_Loyalty').gameObject:GetComponent('UISlider')
    
    this.slidEQ = findChildRecursively(this.transform,'View/NpcProperty/EQ/slid_EQ').gameObject:GetComponent('UISlider')
    
    this.slidcharm = findChildRecursively(this.transform,'View/NpcProperty/charm/slid_charm').gameObject:GetComponent('UISlider')
    layerManager:SetLayer(this.gameObject,UILayerType.Window)
end

function UINPCEvent:Reset()
    if this.gameObject ~= nil then
        GameObject.Destroy(this.gameObject)
        this.gameObject = nil
    end
end

function UINPCEvent:ShowBydata(NpcData)
    this.spNpcHead.spriteName = NpcData.Head
    this.lbNpcName.text = NpcData.name
    this.Position = NpcData.Position
    this.slidLoyalty.value = 1
    this.slidEQ.value = 1
    this.slidcharm.value = 1
end


function UINPCEvent.OnDestroy()
    this.UINPCEventlogic:Release()
end

function UINPCEvent:Close( ... )
   self:Reset()
end