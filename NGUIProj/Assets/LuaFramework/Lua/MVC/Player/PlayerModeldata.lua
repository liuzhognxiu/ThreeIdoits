PlayerModeldata = class() 
local this = {}

this.table = {}

function PlayerModeldata:ctor()
    this.name = ""
    this.Position = 1
    this.EQ =  0
    this.charm = 0
    this.interpersonal = {}
    this.age = 0
end

function PlayerModeldata:getName( )
    return this.name
end

function PlayerModeldata:SetPlayerName(Name)
    this.name = Name
    print(this.name)
    this.table["name"] = Name
end

function PlayerModeldata:SetPlayerEQ(EQ)
    this.EQ = EQ
    this.table["EQ"] = EQ
end

function PlayerModeldata:SetPlayerPosition(Position)
   this.Position = Position
   this.table["Position"] = Position
end

function PlayerModeldata:SetPlayerCharm( charm )
    this.charm = charm
    this.table["charm"] = charm
end

function PlayerModeldata:SetInterpersonalI(interpersonal)
    this.table["interpersonal"] = interpersonal
    this.interpersonal = interpersonal
end

function PlayerModeldata:AddInterpersonal( NpcPlayer )
    table.insert(this.table["interpersonal"], NpcPlayer)
end

function PlayerModeldata:GetInterpersonal()
    return  this.interpersonal
end

function PlayerModeldata:SetPlayerAge(age)
    this.age = age
    this.table["age"] = age
end

function PlayerModeldata:GetTable()
    return this.table
end

function PlayerModeldata:SetTable(PlayerTabel)
    this.table = PlayerTabel
    this.name = PlayerTabel['name']
    this.EQ = PlayerTabel['EQ']
    this.Position = PlayerTabel['Position']
    this.charm = PlayerTabel['charm']
    this.age = PlayerTabel['age']
    this.interpersonal = PlayerTabel['interpersonal']
end