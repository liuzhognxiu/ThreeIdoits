require "MVC/UIModel/SelectNpcModel"
--require "Framework/LuaLogic"

SelectNpcLogic = class(LuaLogic)

local this = {} 

this.view = nil

function SelectNpcLogic:Ctor()
end

function SelectNpcLogic:Initialize(view)
    this.view  = view
    self:ItemSource(GameData.SelectNpcModel())
    self:SetBinding(GameData.SelectNpcModel().KEY_NPCGRID,NpcListUpdate)
end

function SelectNpcLogic:NpcListUpdate(NpcList)
    print("============================")
end
