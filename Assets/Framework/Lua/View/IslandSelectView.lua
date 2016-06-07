IslandSelectView = class("IslandSelectView")

function IslandSelectView:ctor(gameObject)
	print("IslandSelectView created!")
	self.gameObject = gameObject
	self.transform = gameObject.transform
    self.transform.localPosition = Vector3.New(0,20,0)
	self:initView()
end

function IslandSelectView:initView()
	for i = 1,3 do
		local island1Btn = self.transform:FindChild("Button" .. i):GetComponent("JJButton")
		island1Btn:AddClickCallback(self,self.onClickIslandBtn)
	end
	self.label_ = self.transform:FindChild("Text"):GetComponent("JJLabel")
end

function IslandSelectView:onClickIslandBtn(obj)
	self.label_.Text = obj.name .. "   378"
    local index = tonum( string.sub(obj.name,string.len(obj.name)) )
    self.label_.FontStyle = index
    self.label_.FontSize = 20 + 10 * index
    self.label_.FontColor = Color.New(0,1,0,0.7)

    --LeanTween.move(obj, Vector3.New(200,-100,0), 1)
	LeanTween.rotateAround(obj, Vector3.forward, 90, 1)
	--LeanTween.scale(obj, obj.transform.localScale*2, 1)
	--LeanTween.rotateAround(obj, Vector3.forward, -90, 1)
end