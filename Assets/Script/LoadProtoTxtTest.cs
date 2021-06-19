using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LoadProtoTxtTest
{
    public static string LoadProtoBufFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("fileName文件名为空！");
            return null;
        }
        string luaPath = Application.dataPath + "/Proto/" + fileName;
        string strLuaContent = File.ReadAllText(luaPath);
        return strLuaContent;
    }
}
