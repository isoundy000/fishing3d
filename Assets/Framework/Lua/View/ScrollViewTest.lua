ScrollViewTest = class("ScrollViewTest")

function ScrollViewTest:ctor(gameObject)
	print("ScrollViewTest created!")
	self.gameObject = gameObject
	self.transform = gameObject.transform
    self.transform.localPosition = Vector3.New(0,0,0)
	self:initView()
end

function ScrollViewTest:initView()
    local scrollview = self.transform:FindChild("ScrollView")
	local grid = scrollview:FindChild("Grid"):GetComponent("UIGrid")
    local itemtemplate = scrollview:FindChild("ItemTemplate").gameObject
    for i = 0,10 do
        local item = GameObject.Instantiate(itemtemplate)
        grid:AddChild(item.transform)
        item.transform.localScale = Vector3.one
        
    end
end