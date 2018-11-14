require("MVC/UILogic/SelectNpcLogic")

UISelectNpcList  = class(ViewBase)

local  this = {}

this.super = ViewBase

function UISelectNpcList:ctor( ... )
    this.gameObjcet = nil
    this.transform = nil 

end

function UISelectNpcList:init( ... )
   this.Logic = SelectNpcLogic.new()
   this.Logic:Initialize(self)
   this.btnBehavior = nil

   this.root = GameObject.Find("UI Root")
   this.gameObjcet = resMgr:LoadPrefab("Prefabs/UISelectNpc",this.root)
   this.transform = this.gameObjcet.transform
   this.NPCListGO = this.transform:Find("Event/Scroll View/NpcList").gameObjcet

   this.luaBehavior = this.gameObjcet:GetComponent('luaBehavior')
   this.NPClistGrid = this.NPCListGO:GetComponent("UIGridContainer")

end

function UISelectNpcList:UpdateNpcList( NPCList )
    this.NPClistGrid.MaxCount = 4
    for i=1,4 do
        local fun = function()          
            print(123)
        end 
       this.luaBehavior:AddClick(NpcList.controlList[i-1],fun)
    end
end