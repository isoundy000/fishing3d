BaseState = 
{
	gameObject = nil;
	transform = nil;
	controller = nil;
	animGroup = nil;
	weaponManager = nil;
}

function BaseState:OnAwake(gameobj)
	self.gameObject = gameobj;
	self.transform = self.gameObject.transform;
	self.controller = self.gameObject:GetComponent(typeof(BlingARPG.ActorController));
	self.animGroup = self.gameObject:GetComponent(typeof(BlingARPG.BlingAnimationGroup));
	self.weaponManager = self.gameObject:GetComponent(typeof(WeaponManager));
	print("awake called");
end;