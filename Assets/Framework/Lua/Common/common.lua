function count(_tbl)
	local count = 0;
	if (_tbl) then
		for i,v in pairs(_tbl) do
			count = count+1;
		end
	end	
	return count;
end


function new(_obj, norecurse)
	if (type(_obj) == "table") then
		local _newobj = {};
		if (norecurse) then
			for i,f in pairs(_obj) do
				_newobj[i] = f;
			end
		else
			for i,f in pairs(_obj) do
				if ((type(f) == "table") and (_obj~=f)) then -- avoid recursing into itself
					_newobj[i] = new(f);
				else
					_newobj[i] = f;
				end
			end
		end
		return _newobj;
	else
		return _obj;
	end
end


function merge(dst, src, recurse)
	for i,v in pairs(src) do
		if (type(v) ~= "function") then
			if(recurse) then
				if((type(v) == "table") and (v ~= src))then  -- avoid recursing into itself
					if (dst[i] == nil) then
						dst[i] = {};
					end
					merge(dst[i], v, recurse);
				elseif (dst[i] == nil) then
					dst[i] = v;
				end
			elseif (dst[i] == nil) then
				dst[i] = v;
			end
		end
	end
	
	return dst;
end


function mergef(dst, src, recursive)
	for i,v in pairs(src) do
		if (recursive) then
			if((type(v) == "table") and (v ~= src))then  -- avoid recursing into itself
				if (dst[i] == nil) then
					dst[i] = {};
				end
				mergef(dst[i], v, recursive);
			elseif (dst[i] == nil) then
				dst[i] = v;
			end
		elseif (dst[i] == nil) then
			dst[i] = v;
		end
	end
	
	return dst;
end


function Vec2Str(vec)
	return string.format("(x: %.3f y: %.3f z: %.3f)", vec.x, vec.y, vec.z);
end

function MakeDerived(child,parent)
	parent.__index = parent;
	setmetatable(child,parent);
end;

function MakeNew(classType)
	function classType.New()
		local ret = {};
		--只拷贝数据
		merge(ret,classType,1);
		classType.__index = classType;
		setmetatable(ret,classType);
		return ret;
	end;
end;