require("MVC/Player/NPCModelBase")

NpcModel = class(NPCModelBase)

local this = {}

this.super = NPCModelBase

function NpcModel:Init()
    print("创建了一个新的NPC")
end
 
function NpcModel:ctor()
    this.name = nil
    this.sex = nil
    this.Position = nil
    this.Head = nil
    this.EQ = nil
    this.Loyalty = nil
    this.IQ = nil
end



function NpcModel:initForData( data )
    this.name = data.name
    this.sex = data.sex
    this.Position = data.Position
    this.Head = data.Head
    this.EQ = data.EQ
    this.Loyalty = data.Loyalty
    this.IQ = data.IQ
end

return NpcModel
