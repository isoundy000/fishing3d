--region TouchEventLayer.lua
--Date
--此文件由[BabeLua]插件自动生成
TouchEventLayer = class("TouchEventLayer",require("View.ViewBase"))

function TouchEventLayer:ctor(obj)
    TouchEventLayer.super.ctor(self)
    self.gameObject_ = obj
    self.transform_ = obj.transform
    self.transform_.parent = self.uiRootObj_.transform
    self.gameObject_.name = "TouchEventLayer"
    self.pressedTime_ = 1
    self.pressed_ = false
    self:initEventCallback()
end

function TouchEventLayer:initEventCallback()
    local eventTrigger = self.transform_:GetChild(0):GetComponent("JJEventTrigger")
    if eventTrigger then
        eventTrigger:AddPressCallback(handler(self,self.onPressed),nil)
        eventTrigger:AddReleaseCallback(handler(self,self.onReleased),nil)
    end
end

function TouchEventLayer:onPressed(args)
    self.pressed_ = true
end

function TouchEventLayer:onReleased(args)
    self.pressed_ = false
    self.pressedTime_ = 1
end

function TouchEventLayer:OnUpdate(dt)
    if not self.pressed_ then
        return
    end
    self.pressedTime_ = self.pressedTime_ + dt
    if self.pressedTime_ > 0.3 then
        self.pressedTime_ = 0
        local touchPos = MathUtil.ScreenPos_to_NGUIPos(UnityEngine.Input.mousePosition);
        local from = touchPos - Vector3.New(0, -320,0);
        if from.y < 0 then
            from.y = 0
        end
        local angle = Vector3.Angle(from, Vector3.right);
        MainView:playCannonAnimation(0,angle)
        MainView:createBullet(0,from)
    end
end

--endregion
