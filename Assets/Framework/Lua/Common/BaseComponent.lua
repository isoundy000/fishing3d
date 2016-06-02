local BaseComponent = class("BaseComponent")

function BaseComponent:ctor()
	self.gameObject = nil
	self.transform = nil

end

function BaseComponent:OnAwake(gameobj)
	self.gameObject = gameobj;
	self.transform = self.gameObject.transform;
	print("awake called");
end;

return BaseComponent