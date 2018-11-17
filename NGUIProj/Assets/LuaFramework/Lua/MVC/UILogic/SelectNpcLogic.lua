require "MVC/UIModel/SelectNpcModel"
require "Framework/LuaLogic"

SelectNpcLogic = class(LuaLogic)

local this = {} 

function SelectNpcLogic:ctor()
end

function SelectNpcLogic:Initialize(view)
    this.view  = view
    self:ItemSource(GameData.SelectNpcModel())

    local UpdateNpclist = function (NpcList)
        view:UpdateNpcList(NpcList)
    end
    
    self:SetBinding(GameData.SelectNpcModel().KEY_NPCGRID,UpdateNpclist)
end


