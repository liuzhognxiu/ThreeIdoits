AIPlayerModeldata = class(PlayerModeldata) 

local this = {}
this.table = {}

this.super = PlayerModeldata

function AIPlayerModeldata:ctor()
    this.super:ctor()
    this.AI = 100 
end

function AIPlayerModeldata:SetAI( ai )
    this.AI = ai
end