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

    this.btnappoint = findChildRecursively(this.transform,'Event/btn_appoint').gameObject
    
    this.btnfire = findChildRecursively(this.transform,'Event/btn_fire').gameObject

    this.luaBehavior:AddClick(this.btnappoint,self.Appoint)

    this.luaBehavior:AddClick(this.btnfire,self.BeFire)

    layerManager:SetLayer(this.gameObject,UILayerType.Window)
end

function UINPCEvent.Appoint(go)
    UIManager.Create("NPCchooseEventPanel")
end

function UINPCEvent.BeFire(go)
    UIManager.Create("UIPromptips"):ShowByID(12,function (btn,value)
        if btn == "left" then
           Utility.ShowCenterInfo(this.lbNpcName.text.."被解雇",UnityEngine.Color.red);
        else
            UIManager.Close("UIPromptips")
        end
    end)
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
    this.lbPosition.text = GetEunmName(tonumber(NpcData.Position),PositionName)
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