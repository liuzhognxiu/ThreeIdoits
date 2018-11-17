require "MVC/UIModel/NPCEventModel"
require "Framework/LuaLogic"

NPCEventLogic = class(LuaLogic)

local this = {}

function NPCEventLogic:ctor()
end

function NPCEventLogic:Initialize(view)
   this.view  = view
   self:ItemSource(GameData.NPCEventModel())
end
