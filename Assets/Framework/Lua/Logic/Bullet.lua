Bullet = class("Bullet")

function Bullet:ctor(obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
end

function Bullet:setSpeed(speed)
    self.speed_ = speed
end

function Bullet:OnUpdate(args)

end