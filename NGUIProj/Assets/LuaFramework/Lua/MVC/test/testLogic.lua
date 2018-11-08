require "Framework/LuaLogic"
require "MVC/test/testModel"

testLogic = class(LuaLogic);

local this = {}

function testLogic:ctor()
end

function testLogic:Initialize( view )
	self:ItemSource(GameData.TestModel());
	local callback1 = function ( count )
		view:UpdateCount(count);
	end
	local callback2 = function ( data )
		view:UpdateDouble(data);
	end
	self:SetBinding(GameData.TestModel().KEY_COUNT, callback1);
	self:SetBinding(GameData.TestModel().TESTMODEL_DOUBLE, callback2);
end

function testLogic:AddEvent1()
	
end
