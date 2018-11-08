require "Framework/NotifyPropChanged"

testModel = class(NotifyPropChanged)
local this = {};

function testModel:ctor()
	this.m_count = 0;
	this.m_double = 0;
	self.KEY_COUNT = "TESTModel_Count" --绑定UI组件的唯一ID
	self.TESTMODEL_DOUBLE = "TESTMODEL_DOUBLE"
end

function testModel:getCount() 
	return this.m_count;
end

function testModel:setCount( count )
	this.m_count = count;
	--通知UI组件更新
	self:OnPropertyChanged(self.KEY_COUNT, this.m_count);
end

function testModel:getDouble()
	return this.m_double;
end

function testModel:setDouble( data )
	this.m_double = data
	self:OnPropertyChanged(self.TESTMODEL_DOUBLE, this.m_double)
end
