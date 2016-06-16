--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
Bomb = class("Bomb",require("Logic.NodeBase"))

function Bomb:ctor(obj)
    Bomb.super.ctor(self,obj)
    self.active_ = false
end

function Bomb:bomp()
    local animation = self.transform_:FindChild("Animation"):GetComponent("UISpriteAnimation")
    animation:Play()
    animation.animationCallback = handler(self,self.bombCallback)
end

function Bomb:bombCallback()
    MainView:recycleBomb(self)
end
--endregion
