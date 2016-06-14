--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local GameUIManager = require("Logic.GameUIManager")
local ViewBase = class("ViewBase")

function ViewBase:ctor(args)
    self.uiManager_ = GameUIManager
    self.uiRootObj_ = GameUIManager.uiRoot_ 
end

return ViewBase
--endregion
