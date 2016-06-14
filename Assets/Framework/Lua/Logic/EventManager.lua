--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local json = require("cjson")
local EventManager = class("EventManager")

function EventManager:ctor()
    self.eventList_ = {}
end

function EventManager:getInstance()
    if self.instance_ == nil then
        self.instance_ = EventManager.new()
    end

    return self.instance_;
end

function EventManager:loadEventConfig()
    local str = ResourceManager:LoadFile("config","event")
    self.eventList_ = json.decode(str)
end

function EventManager:caculateBeginAndEndTime()
    
end

return EventManager
--endregion
