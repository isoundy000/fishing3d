BaseUIComponent = 
{
	gameObject = nil;
	transform = nil;
}

function BaseUIComponent:OnAwake(gameobj)
	self.gameObject = gameobj;
	self.transform = self.gameObject.transform;
	print("awake called");
end;