require "Framework/NotifyPropChanged"

PlayerModel = class(NotifyPropChanged)
local this = {}

function PlayerModel:ctor()
	self.m_Name = 0
	self.Id = 0
	self.PLAYE_RNAME_CREATE = "PLAYE_RNAME_CREATE"
end

function PlayerModel:getName()
	return self.m_Name;
end

function PlayerModel:setPlayerName( Name )
	self.m_Name = Name
	--通知UI组件更新
	--self:OnPropertyChanged(self.PLAYER_NAME_CREATE, self.m_Name)
end

-- function MapModel:( ... )
-- 	-- body
-- end