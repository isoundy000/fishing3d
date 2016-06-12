--region TouchEventLayer.lua
--Date
--此文件由[BabeLua]插件自动生成
TouchEventLayer = class("TouchEventLayer")

function TouchEventLayer:ctor(obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
    self:initEventCallback()
end

function TouchEventLayer:initEventCallback()
    print("touch event layer created!")
    local eventTrigger = self.transform_:GetChild(0):GetComponent("JJEventTrigger")
    if eventTrigger then
        eventTrigger:AddPressCallback(handler(self,self.onPressed),nil)
        eventTrigger:AddReleaseCallback(handler(self,self.onReleased),nil)
    end
end

function TouchEventLayer:onPressed(args)
    print("pressed!")
end

function TouchEventLayer:onReleased(args)
    print("released!")
end

--endregion
