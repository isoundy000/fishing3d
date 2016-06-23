--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
Cannon = class("Cannon",require("View.ViewBase"))

function Cannon:ctor(obj)
    Cannon.super.ctor(self,obj)
    self.gameObject_ = obj
    self.transform_ = obj.transform
end

function Cannon:initView()
    
end


--endregion
