require("View.MainView")
require("View.TouchEventLayer")
IslandSelectView = class("IslandSelectView",require("View.ViewBase"))

function IslandSelectView:ctor(gameObject)
    IslandSelectView.super.ctor(self,gameObject)
    self.gameObject_.name = "IslandSelectView"
    self.transform_.parent = self.uiRootObj_.transform
    self:setScale(Vector3.one)
    self:setPosition(Vector3.zero)
	self:initView()
end

function IslandSelectView:initView()
	for i = 1,3 do
		local island1Btn = self.transform_:FindChild("Island" .. i):GetComponent("JJButton")
		island1Btn:AddClickCallback(handler(self,self.onClickIslandBtn),island1Btn)
	end
	self.label_ = self.transform_:FindChild("Label"):GetComponent("JJLabel")

    local eventListener = self.transform_:FindChild("Sprite"):GetComponent("UIEventListener")
    eventListener.onClick = handler(self,self.onClickedSprite)
    eventListener.onPress = handler(self,self.onPressedSprite)

    self.uiManager_:showView("View_CoinAnimation")
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

function IslandSelectView:onClickedSprite(obj)
    print(obj.name)
end

function IslandSelectView:onPressedSprite(obj,isPressed)
    print(obj.name,isPressed)
end

function IslandSelectView:testCoin()

    LeanTween.delayedCall(2,System.Action_object(handler(self,self.jumpCoin))):setOnCompleteParam(13)
end

function IslandSelectView:jumpCoin(obj)
    print("123",obj)
end

function IslandSelectView:jumpCoin2()
    print("123")
end