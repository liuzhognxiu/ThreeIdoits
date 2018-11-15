require("MVC/UILogic/SelectNpcLogic")

UISelectNpcList  = class(ViewBase)

local  this = {}

this.super = ViewBase

function UISelectNpcList:ctor( ... )
    this.gameObject = nil
    this.transform = nil 
    this.luaBehavior =nil
    this.NPClistGrid = nil
end

function UISelectNpcList:init( ... )
   this.Logic = SelectNpcLogic.new()
   this.Logic:Initialize(self)
   this.btnBehavior = nil

   this.root = GameObject.Find('UI Root')

   this.gameObject = resMgr:LoadPrefab("Prefabs/UISelectNpc",this.root)
   this.transform = this.gameObject.transform
   this.NPCListGO = this.transform:Find("Event/Scroll View/NpcList").gameObject

   this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')
   this.NPClistGrid = this.NPCListGO:GetComponent("UIGridContainer")

   local table = {}
   --self:UpdateNpcList(table)

   layerManager:SetLayer(this.gameObject,UILayerType.Window)
end

function UISelectNpcList:UpdateNpcList(NpcList)
    this.NPClistGrid.MaxCount = #NpcList
    for i=1,#NpcList do
        local lb_NpcName = this.NPClistGrid.controlList[i-1].transform:Find('lb_NpcName'):GetComponent('UILabel')
        lb_NpcName.text = NpcList[i].name
        local sp_NpcHead = this.NPClistGrid.controlList[i-1].transform:Find('sp_NpcHead'):GetComponent('UISprite')
        sp_NpcHead.spriteName = NpcList[i].Head
        local fun = function()          
            print(lb_NpcName.text)
        end 
        --print(#NPClistGrid.controlList)
        this.luaBehavior:AddClick(this.NPClistGrid.controlList[i-1],fun)
    end
end