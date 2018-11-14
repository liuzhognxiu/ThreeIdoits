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
   self:UpdateNpcList(table)

   layerManager:SetLayer(this.gameObject,UILayerType.Window)
end

function UISelectNpcList:UpdateNpcList( NPCList )
    this.NPClistGrid.MaxCount = 4
    for i=1,4 do
        local fun = function()          
            print(123)
        end 
        --print(#NPClistGrid.controlList)
        this.luaBehavior:AddClick(this.NPClistGrid.controlList[i-1],fun)

        local lb_NpcName = this.NPClistGrid.controlList[i-1].transform:Find('lb_NpcName'):GetComponent('UILabel')
        lb_NpcName.text = "NPC的名字"
        local sp_NpcHead = this.NPClistGrid.controlList[i-1].transform:Find('sp_NpcHead'):GetComponent('UISprite')
        sp_NpcHead.spriteName = string.format( "[emoticon0%d]",i )

    end
end