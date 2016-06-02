IslandSelectView = class("IslandSelectView")

function IslandSelectView:ctor(gameObject)
	print("IslandSelectView created!")
	self.gameObject = gameObject
	self.transform = gameObject.transform

	self:initView()
end

function IslandSelectView:initView()
	for i = 1,3 do
		local island1Btn = self.transform:FindChild("Button" .. i):GetComponent("JJButton")
		island1Btn:AddClickCallback(self,self.onClickIslandBtn)
	end

end

function IslandSelectView:onClickIslandBtn(obj)
	print(obj.name)
end