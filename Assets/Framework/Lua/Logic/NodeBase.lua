--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local NodeBase = class("NodeBase")

function NodeBase:ctor(obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
end

return NodeBase
--endregion
