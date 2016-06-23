--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local BulletLayer = class("BulletLayer")
function BulletLayer:ctor()
    self:initView()
end

function BulletLayer:initView()
    self.layerObj_ = GameObject.New("Layer_Bullet")
    local uiManager = require("Logic.GameUIManager")
    self.layerObj_.transform.parent = uiManager:getInstance().uiRoot_.transform
    self.layerObj_.transform.localPosition = Vector3.zero
    self.layerObj_.transform.localScale = Vector3.one
    self.layerObj_.transform.eulerAngles = Vector3.zero
    self.transform_ = self.layerObj_.transform
    self.layerObj_.layer = LayerMask.NameToLayer("UI")

    local panel = self.layerObj_:AddComponent(typeof(UIPanel))
    panel.depth = 1
end

return BulletLayer
--endregion
