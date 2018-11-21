require "Framework/ViewBase"

UIPromptips = class(ViewBase)

local this = {}

this.callback = nil
this.Promptipsid = nil
function UIPromptips:ctor()
end

function UIPromptips:Reset()
    if this.gameObject ~= nil then
        GameObject.Destroy(this.gameObject)
        this.gameObject = nil
    end
end

function UIPromptips:init()
    this.luaBehavior = nil;	
	
	this.root = GameObject.Find('UI Root');
    this.gameObject = resMgr:LoadPrefab("Prefabs/UIPromptips",this.root)
    this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')
    this.transform = this.gameObject.transform

    layerManager:SetLayer(this.gameObject,UILayerType.Tips)

    this.lb_message = findChildRecursively(this.transform,"View/lb_Message").gameObject:GetComponent('UILabel')

    this.lb_Right = findChildRecursively(this.transform,"Event/Right/lb_Right").gameObject:GetComponent('UILabel')

    this.lb_left = findChildRecursively(this.transform,"Event/left/lb_left").gameObject:GetComponent('UILabel')

    this.btnRigth = findChildRecursively(this.transform,"Event/Right").gameObject

    this.btnleft = findChildRecursively(this.transform,"Event/left").gameObject

   -- this.btnClose = findChildRecursively(this.transform,"Event/Close").gameObject
end

function UIPromptips:ShowByID(id,callback)
    this.callback = callback
    this.Promptipsid = id
    local Promptipsword = GameData.Promptipsword[id]

    this.lb_message.text = Promptipsword['Dec']
    this.lb_Right.text = Promptipsword['RightBtn']
    this.lb_left.text = Promptipsword['LeftBtn']

    LuaGameManager.GetEventDispatcher():Add(id,this.callback)
    this.luaBehavior:AddClick(this.btnleft,self.OnLeftClick)
    this.luaBehavior:AddClick(this.btnRigth,self.OnRightClick)
  --  this.luaBehavior:AddClick(this.btnClose,self.OnClosePanel)
end

function UIPromptips.OnLeftClick(go)
    this.callback("left",true)    
end

function UIPromptips.OnRightClick(go)
    this.callback("Right",false)    
end

function UIPromptips.OnClosePanel(go)
    UIManager.Close("UIPromptips")
end 

function UIPromptips:Reset()
    if this.gameObject ~= nil then
        GameObject.Destroy(this.gameObject)
        this.gameObject = nil
    end
end

-- UIManager.Create("UIPromptips"):ShowByID(12,function (btn,value)
--     if btn == "left" then
--         print(value)
--     else
--             UIManager.Close("UIPromptips")
--     end
--     end)

