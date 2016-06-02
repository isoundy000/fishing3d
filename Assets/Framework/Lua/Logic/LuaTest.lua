local LuaTest = class("LuaTest")
function LuaTest:ctor()
end

function LuaTest:Add(a,b)
	print("Add  wzw",a+b)
end
return LuaTest