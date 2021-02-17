--保存类类型的虚表
local _class = {}

-- 自定义类型
ClassType = {
    class = 1,
    instance = 2,
}

function BaseClass(classname, super)
    assert(type(classname) == "string" and #classname > 0)
    -- 生成一个类类型
    local class_type = {}

    -- 在创建对象的时候自动调用 如果__init/__delete被重新赋值则会被调用
    class_type.__init = false
    class_type.__delete = false
    class_type.__cname = classname
    class_type.__ctype = ClassType.class

    class_type.super = super
    class_type.New = function(...)
        -- 生成一个类对象
        local obj = {}
        --设置对象的类型信息
        obj._class_type = class_type
        --设置对象的存在状态
        obj.__ctype = ClassType.instance

        -- 在初始化之前注册基类方法
        setmetatable(obj, {
            __index = _class[class_type],
        })
        -- 调用初始化方法
        do
            local create
            --从基类开始初始化
            create = function(c, ...)
                if c.super then
                    create(c.super, ...)
                end
                if c.ctor then
                    c.ctor(obj, ...)
                end
            end

            create(class_type, ...)
        end

        -- 注册一个delete方法 因为创建时是从基类开始向子类创建的
        -- 所以一个子类的实例被创建了之后他的所有基类table都要被删除掉
        -- 从当前table向基类table逐个删除
        obj.Delete = function(self)
            local now_super = self._class_type
            while now_super ~= nil do
                if now_super.__delete then
                    now_super.__delete(self)
                end
                now_super = now_super.super
            end
        end

        return obj
    end

    --创建一个虚表(类似C++中的虚表个功能)
    local vtbl = {}
    --_class在BaseClass被加载时一直存在于内存中 所有类型都有一个对应的虚拟列表
    _class[class_type] = vtbl
    --将 class_type的__index指向他的虚表  　　 --这一步规范了class_type的操作
    setmetatable(class_type, {
        --当给类型添加新属性时存储在虚表中 class_type 只用来记录类型的基础信息
        __newindex = function(t,k,v)
            vtbl[k] = v
        end
    ,
        --去父表中查询对应的Key 如果父表还存在__index 则一直往下查询 完成虚表的功能
        __index = vtbl,
    })
    --如果class_type存在父节点则将其对应虚表的__index指向父类虚表中的key　　　　 --这一步将vtbl全部关联起来
    if super then
        setmetatable(vtbl, {
            __index = function(t,k)
                --在全局表中查找父表 返回父表对应虚表中的key
                local ret = _class[super][k]
                return ret
            end
        })
    end

    return class_type
end