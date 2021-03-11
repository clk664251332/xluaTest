--这里require的顺序很重要
require("Framework/Common/BaseClass")
Singleton = require ("Framework/Common/Singleton")

UIBase = require("Framework/UI/UIBase")
UIManager = require("Framework/UI/UIManager")
require("Framework/UI/UIDefine")

--require("luaProtobufTest")
---游戏的入口函数
function GameMain()
    UIManager:GetInstance():Init()
end

---游戏退出函数
function GameExit()

end