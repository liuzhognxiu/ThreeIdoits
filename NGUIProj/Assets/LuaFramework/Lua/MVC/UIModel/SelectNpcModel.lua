--require "Framework/NotifyPropChanged"

SelectNpcModel = class(NotifyPropChanged)

local  this = {}

function SelectNpcModel:ctor()
   this.NpcList = {}
   self.KEY_NPCGRID = "NpcList"
end

function SelectNpcModel:SetNpcList(npclist)
   this.NpcList = npclist
   self:OnPropertyChanged(self.KEY_NPCGRID, this.NpcList);
end

function SelectNpcModel:GetNpcList ()
    return  this.NpcList
end