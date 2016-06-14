Bullet = class("Bullet")

function Bullet:ctor(obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
    self.active_ = false
    self.dir_ = Vector3.right
    self.speed_ = 700
end

function Bullet:setSpeed(speed)
    self.speed_ = speed
end

function Bullet:OnUpdate(dt)
    self.transform_.localPosition = self.transform_.localPosition + self.dir_ * dt * self.speed_;
    if self.transform_.localPosition.x < -640 or self.transform_.localPosition.x > 640 or self.transform_.localPosition.y < -360 or self.transform_.localPosition.y > 360 then
        MainView:recycleBullet(self)
        return;
    end
end