--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
require("Logic.Cannon")
require("Logic.Bullet")
require("Logic.Bomb")
local EventManager = require("Logic.EventManager")
MainView = class("MainView",require("View.ViewBase"))
MainView.cannons_ = {}
MainView.bulletsPool_ = {}
MainView.bombPool_ = {}
function MainView:ctor(obj)
    MainView.super.ctor(self)
    self.gameObject_ = obj
    self.gameObject_.name = "Main"
    self.transform_ = obj.transform
    self.transform_.parent = self.uiRootObj_.transform
    self.transform_.localScale = Vector3.one
    self.transform_.localPosition = Vector3.zero

    self.cannonRoot_ = self.transform_:FindChild("CannonsRoot")
    self.bulletRoot_ = self.transform_:FindChild("BulletsRoot")
    self.bombRoot_ = self.transform_:FindChild("BombsRoot")

    self:initBulletsPool()
    self:initBombPool()
    self:initView()
end

function MainView:initView()
    local backBtn = self.transform_:FindChild("Btn_Back"):GetComponent("JJButton")
    if backBtn then
        backBtn:AddClickCallback(handler(self,self.onClickBackBtn),nil)
    end

    self:createCannon(0)

    EventManager:getInstance():loadEventConfig()

    local fish = ResourceManager:CreateObjectWithOutScript("fish0","Fish_0","Fish")
    fish.transform.localPosition = Vector3.zero
    fish.transform.localScale = Vector3.one
end

function MainView:onClickBackBtn(args)
    self.uiManager_:hideAll()
    self.uiManager_:showView("IslandSelect")
end

function MainView:createCannon(seatid)
    if seatid == 0 then
        local cannon = ResourceManager:CreateObject("cannon","Cannon_01","Cannon","UI")
        cannon.gameObject_.name = "Cannon0"
        cannon.transform_.parent = self.cannonRoot_
        cannon.transform_.localPosition = Vector3.New(0,-320,0)
        cannon.transform_.localScale = Vector3.one
        self.cannons_[0] = cannon
    elseif seatid == 1 then
    elseif seatid == 2 then
    elseif seatid == 3 then
    end
end

function MainView:playCannonAnimation(seatid,angle)
    local animation = self.cannons_[seatid].transform_:FindChild("Animation"):GetComponent("UISpriteAnimation")
    animation:ResetToBeginning()
    animation:Play()
    self.cannons_[seatid].transform_.eulerAngles = Vector3.New(0, 0, angle);
end

function MainView:initBulletsPool()
    for i=0,50 do
        local bullet = ResourceManager:CreateObject("bullet","Bullet","Bullet","UI")
        bullet.transform_.parent = self.bulletRoot_
        bullet.transform_.localPosition = Vector3.New(-10000,-10000,0)
        bullet.transform_.localScale = Vector3.one
        self.bulletsPool_[i] = bullet
    end
    
end

function MainView:initBombPool(args)
    for i=0,50 do
        local bomb = ResourceManager:CreateObject("bomb","Bomb_01","Bomb","UI")
        bomb.transform_.parent = self.bombRoot_
        bomb.transform_.localPosition = Vector3.New(-10000,-10000,0)
        bomb.transform_.localScale = Vector3.one
        self.bombPool_[i] = bomb
    end
    
end

function MainView:getBulletFromPool(a)
    for i,value in pairs(self.bulletsPool_) do
        if value.active_ == false then
            value.active_ = true
            value.gameObject_:SetActive(true)
            return value
        end
    end
    return nil
end

function MainView:createBullet(seatid,dir)
    local bullet = self:getBulletFromPool()
    local cannon = self.cannons_[seatid]
    if bullet then
        local normaldir = dir.normalized
        local angle = Vector3.Angle(dir, Vector3.right);
        bullet.transform_.eulerAngles = Vector3.New(0,0,angle)
        bullet.transform_.localPosition = cannon.transform_.localPosition + normaldir * 50;
        bullet.transform_.localScale = Vector3.one;
        bullet.dir_ = normaldir
    end
end

function MainView:recycleBullet(bullet)
    bullet.active_ = false
    bullet.gameObject_:SetActive(false)
    bullet.transform_.localPosition = Vector3.New(-1000,-1000,0)
end

--endregion
