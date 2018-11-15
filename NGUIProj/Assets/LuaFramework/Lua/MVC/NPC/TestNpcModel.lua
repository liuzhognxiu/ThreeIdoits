require("MVC/Player/NPCModelBase")

TestNpcModel = class(NPCModelBase)

local this = {}

this.super = NPCModelBase

function TestNpcModel:Init()
    print("创建了一个新的NPC")
end
 
function TestNpcModel:ctor()
    this.name = nil
    this.sex = nil
    this.Position = nil
    this.Head = nil
end



function TestNpcModel:initForData( data )
    this.name = data.name
    this.sex = data.sex
    this.Position = data.Position
    this.Head = data.Head
end

return TestNpcModel
