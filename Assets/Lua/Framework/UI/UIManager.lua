---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by 蓝色幻想.
--- DateTime: 2021/2/15 17:30
---

local UIManager = BaseClass("UIManager", Singleton)

---@这里设置为local的都是外界无法访问到的没必要放在对象成员的数据
--- UIRoot路径
local UIRootPath = "UIRoot"
--- EventSystem路径
local EventSystemPath = "EventSystem"

function UIManager.ctor(self)
    self.uiClasses = {}
end

---------------------------------------- Start 生命周期函数 ----------------------------------------------
function UIManager:Init()
    ---设置两个重要的UIPrefab不销毁
    self.rootObj = CS.UnityEngine.GameObject.Find(UIRootPath)
    self.rootTrans = self.rootObj.transform
    CS.UnityEngine.Object.DontDestroyOnLoad(self.rootObj)
    local event_system = CS.UnityEngine.GameObject.Find(EventSystemPath)
    CS.UnityEngine.Object.DontDestroyOnLoad(event_system)
    ---初始化UI的各个layer
    self.MainLayer = self.rootTrans:Find("MainLayer")
    self.FunctionLayer  = self.rootTrans:Find("FunctionLayer ")
    self.TipLayer = self.rootTrans:Find("TipLayer")
    self.TopLayer = self.rootTrans:Find("TopLayer")
    self.DeactiveLayer = self.rootTrans:Find("DeactiveLayer")

    self:_preload()
end
---------------------------------------- End 生命周期函数 ----------------------------------------------
---####################################################################################################
---####################################################################################################
---####################################################################################################
---------------------------------------- Start 私有函数 ----------------------------------------------
function UIManager:_preload()
    ---预加载几个最常用的UI
    self:_activeUI("Test")
end

function UIManager:_activeUI(uiName)
    ---加载UI，初始化UI的控制类
    local uiClass = self:_getUIClass(uiName)
    ---具体的UI的各种配置，如层级，打开方式等等，都在这个具体界面的class的构造函数设置，这里能够取到，并决定了其打开方式
    uiClass:Load(nil)
end

function UIManager:_getUIClass(uiName)
    local uiClassName = uiName .. "Ctrl"
    local currentClass = self.uiClasses[uiClassName]
    if currentClass == nil then
        local uiClassType = require("UI/Ctrl/"..uiClassName)
        currentClass = uiClassType.New()
        currentClass.name = uiName
        self.uiClasses[uiClassName] = currentClass
    end

    return currentClass
end

---设置layer对应的canvas组件是否enable
function UIManager:_setLayerEnable(layerName, enable)
    local canvas = container.gameObject:GetComponent("Canvas")
    if canvas then
        canvas.enabled = enable
    end
end
---------------------------------------- End 私有函数 --------------------------------------------------
---####################################################################################################
---####################################################################################################
---####################################################################################################
---------------------------------------- Start 外部调用函数 ----------------------------------------------
---打开界面
function UIManager:ActiveUI(uiName)
    ---打开一个界面还可能顺带打开另一个界面，这是因为一个功能可能是两个UI界面共同作用的。这两个界面就需要定义为一个组Group。
    ---之后，我们要获取这个UI的所有配置，成为一张表，叫做uiConfigData
    self:_activeUI("uiName")
end
---关闭界面
function UIManager:DeActiveUI(uiName)

end
---显示界面
function UIManager:ShowUI(uiName)

end
---隐藏界面
function UIManager:HideUI(uiName)

end

---显示主界面UI，原理是关闭除主界面UI组，预设常驻的UI，以及参数指定的UI的其他UI
function UIManager:ShowMainUI()

end
---------------------------------------- End 外部调用函数 ------------------------------------------------
return UIManager