require("Framework/ViewBase")

UICreatePlayer = class(ViewBase)

local this = {}

function UICreatePlayer:ctor()
    
end

function UICreatePlayer:Reset()
    if this.gameObject ~= nil then
        GameObject.Destory(this.gameObject)
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

    Utility.CreateTextFile(UnityEngine.Application.persistentDataPath.."/PlayerData",TableToStr(Game.GameManager.Player:getTable()),true)
    local ccc = Utility.LoadTextFile(UnityEngine.Application.persistentDataPath.."/PlayerData",true)
    print(TableToStr(Game.GameManager.Player:getTable()))
    print(StrToTable(ccc)['name'])
end