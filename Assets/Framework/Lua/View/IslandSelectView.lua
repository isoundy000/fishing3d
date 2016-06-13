require("View.MainView")
require("View.TouchEventLayer")
IslandSelectView = class("IslandSelectView",require("View.ViewBase"))

function IslandSelectView:ctor(gameObject)
    IslandSelectView.super.ctor(self)
	self.gameObject = gameObject
	self.transform = gameObject.transform
    self.transform.parent = self.uiRootObj_.transform
    self.transform.localPosition = Vector3.New(0,0,0)
    self.transform.localScale = Vector3.New(1,1,1)
    self.gameObject.name = "IslandSelectView"
	self:initView()
end

function IslandSelectView:initView()
	for i = 1,3 do
		local island1Btn = self.transform:FindChild("Island" .. i):GetComponent("JJButton")
		island1Btn:AddClickCallback(handler(self,self.onClickIslandBtn),island1Btn)
	end
	self.label_ = self.transform:FindChild("Label"):GetComponent("JJLabel")
end

function IslandSelectView:onClickIslandBtn(param)
    local obj = param.gameObject
	self.label_.Text = obj.name .. "   378"
    local index = tonum( string.sub(obj.name,string.len(obj.name)) )
    self.label_.FontStyle = index
    self.label_.FontSize = 20 + 10 * index
    self.label_.FontColor = Color.New(0,1,0,0.7)

    --LeanTween.moveLocal(obj, obj.transform.localPosition + Vector3.New(100,0,0), 1):setOnCompleteCallback(handler(self,self.move1End),obj)
	--LeanTween.scale(obj, obj.transform.localScale*1.2, 1)
	--LeanTween.rotateAround(obj, Vector3.forward, -90, 1):setOnCompleteCallback(handler(self,self.rotate),"hello world!"):setEase(LeanTweenType.easeInOutElastic)
    --local a = LeanTween.value(obj, Color.red, Color.green, 1 ):setOnUpdateColor(handler(self,self.onUpdateColor))
    --a:setOnUpdateVector3(handler(self,self.onUpdateColor))

    self.uiManager_:hideView("IslandSelectView")
    self.uiManager_:showView("Main")
    self.uiManager_:showView("TouchEventLayer")
end

function IslandSelectView:move1End(param)
    LeanTween.moveLocal(param, param.transform.localPosition + Vector3.New(-100,0,0), 1)
end

function IslandSelectView:rotate(param)
    print(param)
end

function IslandSelectView:onUpdateColor(val)
    self.label_.FontColor = val
end

function IslandSelectView:showScrollView()
    self.uiManager_:showView("ScrollViewTest")
end