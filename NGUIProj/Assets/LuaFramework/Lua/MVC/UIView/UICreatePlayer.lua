require("Framework/ViewBase")
local json = require "cjson"
local json_safe = require "cjson.safe"
local util = require "cjson.util"

UICreatePlayer = class(ViewBase)

local this = {}

function UICreatePlayer:ctor()
    
end

function UICreatePlayer:Reset()
    if this.gameObject ~= nil then
        GameObject.Destroy(this.gameObject)
        this.gameObject = nil
    end
end

function UICreatePlayer:init()
    this.btnBehaviour = nil

    this.root = GameObject.Find('UI Root')
    this.gameObject = resMgr:LoadPrefab("Prefabs/UICreatePlayer",this.root)
    this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')
    this.transform = this.gameObject.transform

    this.NameText = findChildRecursively(this.transform,'View/PlayerName/Label').gameObject:GetComponent('UILabel')    
    this.AgeText = findChildRecursively(this.transform,'View/PlayerAge/Label').gameObject:GetComponent('UILabel')

    this.BtnCreate = findChildRecursively(this.transform,'Event/btn_CreatePlayer').gameObject

    this.luaBehavior:AddClick(this.BtnCreate,self.OnCreate)

    layerManager:SetLayer(this.gameObject,UILayerType.Window)
end

function UICreatePlayer:OnCreate()
    Game.GameManager.Player:SetPlayerName(this.NameText.text)
    Game.GameManager.Player:SetPlayerAge(this.AgeText.text)

    local NpcList ={}

    math.newrandomseed()

    for i=1,4 do
        local a = math.random(0,#GameData.NPCTable)
        NpcList[i] = GameData.NPCTable[a]
        table.remove(GameData.NPCTable,a)
    end
    
    Game.GameManager.Player:SetInterpersonalI(NpcList)

    Utility.CreateTextFile(UnityEngine.Application.persistentDataPath.."/PlayerData",json.encode(Game.GameManager.Player:GetTable()),true)
    local ccc = Utility.LoadTextFile(UnityEngine.Application.persistentDataPath.."/PlayerData",true)
    print( ccc)
end