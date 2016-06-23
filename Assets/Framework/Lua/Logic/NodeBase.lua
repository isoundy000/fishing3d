--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local NodeBase = class("NodeBase")

function NodeBase:ctor(obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
end

function NodeBase:setPosition(position)
    self.transform_.localPosition = position
end

function NodeBase:setScale(scale)
    self.transform_.localScale = scale
end

function NodeBase:getPosition()
    return self.transform_.localPosition
end

function NodeBase:setEulerAngles(eulerAngle)
    self.transform_.eulerAngles = eulerAngle
end

function NodeBase:setIdentity()
    self.transform_.localPosition = Vector3.zero
    self.transform_.localScale = Vector3.one
    self.transform_.eulerAngles = Vector3.zero
end

return NodeBase
--endregion
