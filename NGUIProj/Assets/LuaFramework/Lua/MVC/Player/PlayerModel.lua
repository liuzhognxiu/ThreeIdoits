require "Framework/NotifyPropChanged"


PlayerModel = class(NotifyPropChanged)
local this = {};

function PlayerModel:ctor( ... )
    this.name = ""
    this.Position = 1
    this.EQ =  0
    this.charm = 0
end

function PlayerModel:GetName()
    return this.name
end

function PlayerModel:SetPlayerName(Name)
    this.name = Name
end

function PlayerModel:SetPlayerEQ(EQ)
    this.EQ = EQ
end

function PlayerModel:SetPlayerPosition(Position)
   this.Position = Position
end

function PlayerModel:SetPlayerCharm( charm )
    this.charm = charm
end
