require "Framework/ViewBase"

UItips = class(ViewBase)

local this = {}

function UItips:ctor
end

function UItips:Reset()
    if this.gameObject ~= nil then
        GameObject.Destory(this.gameObject)
        this.gameObject = nil
    end

    if this.luaBehavior ~= nil then
        this.luaBehavior:RemoveClick(this.btnLogin)
		this.luaBehavior = nil;
    end
end
