using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;
using XLua;
using LuaAPI = XLua.LuaDLL.Lua;

public class xluaTest : MonoBehaviour
{
    LuaEnv luaenv = null;
    void Start()
    {
        luaenv = new LuaEnv();
        luaenv.AddLoader(CustomLoader);
        luaenv.DoString("require 'Main'");
        luaenv.DoString("GameMain()");
    }

    private void OnApplicationQuit()
    {
        luaenv.Dispose();
        luaenv = null;
    }

    public byte[] CustomLoader(ref string filepath)
    {
        string path = Application.dataPath + "/Lua/" + filepath + ".lua";
        if (File.Exists(path))
            return File.ReadAllBytes(path);
        else
            return null;
    }
}
