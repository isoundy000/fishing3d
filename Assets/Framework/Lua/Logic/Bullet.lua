Bullet = class("Bullet",require("Logic.NodeBase"))

function Bullet:ctor(obj)
    Bullet.super.ctor(self,obj)
    self.active_ = false
    self.dir_ = Vector3.right
    self.speed_ = 700
end

function Bullet:setSpeed(speed)
    self.speed_ = speed
end

function Bullet:OnUpdate(dt)
    self.transform_.localPosition = self.transform_.localPosition + self.dir_ * dt * self.speed_;
    if self.transform_.localPosition.x < -600 or self.transform_.localPosition.x > 600 or self.transform_.localPosition.y < -360 or self.transform_.localPosition.y > 300 then
        MainView:recycleBullet(self)
        return;
    end

    local worldpos1 = UICamera.currentCamera.transform:TransformPoint(self.transform_.localPosition)
    local pos = UICamera.currentCamera:WorldToScreenPoint(worldpos1)
    local ray = Camera.main:ScreenPointToRay(pos)
    local layerMask = 2 ^ LayerMask.NameToLayer("Fish")
    local flag,hitInfo = Physics.Raycast(ray, nil, 10000, layerMask)
    if flag then
        MainView:showBomb(self.transform_.localPosition)
        MainView:recycleBullet(self)
    end
end