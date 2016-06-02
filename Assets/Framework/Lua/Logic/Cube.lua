local Cube = class("Cube")
function Cube:ctor()
	self.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube)
	self.gameObject.transform.localScale = Vector3.one * 20
end

function Cube:OnUpdate()
	self.gameObject.transform.position = Vector3.right * 0.2 + self.gameObject.transform.position
end

return Cube