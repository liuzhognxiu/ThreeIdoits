require "MVC/Login/LoginLogic"
require "Framework/ViewBase"


LoginView = class(ViewBase)

local this = {}

function LoginView:ctor( )
end

function LoginView:Reset()
	if (this.m_loginLogic ~= nil) then
		this.m_loginLogic:Release()
		this.m_loginLogic = nil;
	end

	if (this.gameObject ~= nil) then
		GameObject.Destroy(this.gameObject);
		this.gameObject = nil;
	end
	
	this.transform = nil;
	if (this.luaBehavior ~= nil) then
		this.luaBehavior:RemoveClick(this.btnLogin)
		this.luaBehavior = nil;
	end
	
	this.root = nil;
	this.usernameText = nil;
	this.passwordText = nil;
	this.channelText = nil;
	this.btnLogin = nil;
	this.btnRegister = nil;
end

function LoginView:init()
	this.m_loginLogic = LoginLogic.new()
	this.m_loginLogic:Initialize(self);

	this.root = GameObject.Find('TopLeft');
	this.gameObject = resMgr:LoadPrefab("Prefabs/LoginView",this.root)
	this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour');
	this.transform = this.gameObject.transform;

	this.usernameText = findChildRecursively(this.transform, 'usernameText').gameObject:GetComponent('UILabel');
	this.passwordText = findChildRecursively(this.transform, 'passwordText').gameObject:GetComponent('UILabel');
	-- this.channelText = findChildRecursively(this.transform, 'channelText').gameObject:GetComponent('Text');
	this.btnLogin = findChildRecursively(this.transform, 'btnLogin').gameObject

	this.luaBehavior:AddClick(this.btnLogin, self.OnLogin)
	-- this.luaBehavior:AddClick(this.btnRegister, self.OnRegister)
	self:AddCollider()
	
	layerManager:SetLayer(this.gameObject,UILayerType.Base)
end

function LoginView:AddCollider()
	local box = UILuaTools.AddCollider(this.gameObject)
	this.luaBehavior:AddClick(box,self.ClosePanel)
end

function LoginView.ClosePanel(go)
	UIManager.Close("LoginView")
end


function  LoginView:Close()
	self:Reset()
end

function LoginView.OnLogin( go )
	this.m_loginLogic:OnLogin(this.usernameText.text, this.passwordText.text, '')
end

function LoginView.OnRegister( go )
	this.m_loginLogic:OnRegister(this.usernameText.text, this.passwordText.text, '')
end