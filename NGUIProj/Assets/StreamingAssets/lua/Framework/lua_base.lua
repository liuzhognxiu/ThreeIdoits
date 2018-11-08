-- lua模拟面向对象
local _class={}
 
function class(super)
  local class_type={}
  class_type.ctor=false
  class_type.super=super
  class_type.new=function(...) 
      local obj={}
      do
        local create
        create = function(c,...)
          if c.super then
            create(c.super,...)
          end
          if c.ctor then
            c.ctor(obj,...)
          end
        end
 
        create(class_type,...)
      end
      setmetatable(obj,{ __index=_class[class_type] })
      return obj
    end
  local vtbl={}
  _class[class_type]=vtbl
 
  setmetatable(class_type,{__newindex=
    function(t,k,v)
      vtbl[k]=v
    end
  })
 
  if super then
    setmetatable(vtbl,{__index=
      function(t,k)
        local ret=_class[super][k]
        vtbl[k]=ret
        return ret
      end
    })
  end
 
  return class_type
end



-- 下面的代码是lua模拟面向对象使用例子

base_type=class()   --定义一个基类 base_type

function base_type:ctor(x)  --定义 base_type 的构造函数
  print("base_type ctor")
  self.x=x
end
 
function base_type:print_x()  --定义一个成员函数 base_type:print_x
  print(self.x)
end
 
function base_type:hello()  -- 定义另一个成员函数 base_type:hello
  print("hello base_type")
end

function base_type.test()
	print("aaaaaaaaa")
end