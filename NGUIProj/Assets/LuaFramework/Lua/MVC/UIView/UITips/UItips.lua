require "Framework/ViewBase"

UItips = class(ViewBase)

local this = {}

local SpeakMessage= {}
this.WritingCoroutine = nil


function UItips:ctor()
end

function UItips:Reset()
    if this.gameObject ~= nil then
        GameObject.Destroy(this.gameObject)
        this.gameObject = nil
    end

    -- if this.luaBehavior ~= nil then
    --     this.luaBehavior:RemoveClick(this.btnLogin)
	-- 	this.luaBehavior = nil;
    -- end
end

function UItips:init()
	this.btnBehaviour = nil;	
	
	this.root = GameObject.Find('UI Root');
    this.gameObject = resMgr:LoadPrefab("Prefabs/NpcDialog",this.root)
    this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')
    this.transform = this.gameObject.transform

    this.sp_npcHead = findChildRecursively(this.transform,"sp_npcHead").gameObject:GetComponent('UISprite')
    this.sp_bg = findChildRecursively(this.transform,"sp_bg").gameObject:GetComponent('UISprite')
    this.lb_NpcName = findChildRecursively(this.transform,"lb_NpcName").gameObject:GetComponent('UILabel')
    this.lb_NpcMessage = findChildRecursively(this.transform,"lb_NpcMessage").gameObject:GetComponent('UILabel')

    this.luaBehavior:AddClick(this.sp_bg.gameObject,self.OnNextClick)
   -- self:AddCollider()

    layerManager:SetLayer(this.gameObject,UILayerType.Tips)
end

function UItips:show(NpcName,NpcSpeak)
	this.lb_NpcName.text = NpcName
	this.lb_NpcMessage.text = NpcSpeak
end


--之后支持读表播放
function UItips:SelfWritingText(NpcName,NpcSpeak)
    this.lb_NpcName.text = NpcName
    this.SpeakMessage = NpcSpeak
	this.WritingCoroutine = coroutine.start(self.Writing,NpcSpeak)
end


function UItips.Writing(NpcSpeak)
  
	k=string.len(NpcSpeak)
	list1={}
	for i=1,k,3 do	
		list1[i]=string.sub(NpcSpeak,i,i+2)
	end
	this.lb_NpcMessage.text =" "
	for i=1,k,3 do
		this.lb_NpcMessage.text = this.lb_NpcMessage.text..tostring(list1[i]) 
		coroutine.wait(0.12)
	end
end

function UItips.OnNextClick()
    coroutine.stop(this.WritingCoroutine)
    this.lb_NpcMessage.text = this.SpeakMessage

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