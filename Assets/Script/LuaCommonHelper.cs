using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class LuaCommonHelper
{
    public static void SetParent(GameObject go, GameObject parent, bool worldPositionStays = false)
    {
        if (go)
        {
            var trans = parent == null ? null : parent.transform;
            go.transform.SetParent(trans, worldPositionStays);
        }
    }

    #region SetPos

    public static void SetPos(GameObject go, float x, float y, float z)
    {
        if (go)
        {
            go.transform.position = new Vector3(x, y, z);
        }
    }
    public static void SetPos(GameObject go, Vector3 pos)
    {
        if (go)
        {
            go.transform.position = pos;
        }
    }
    public static void SetPosX(GameObject go, float x)
    {
        if (go)
        {
            SetPos(go, x, go.transform.position.y, go.transform.position.z);
        }
    }
    public static void SetPosY(GameObject go, float y)
    {
        if (go)
        {
            SetPos(go, go.transform.position.x, y, go.transform.position.z);
        }
    }
    public static void SetPosZ(GameObject go, float z)
    {
        if (go)
        {
            SetPos(go, go.transform.position.x, go.transform.position.y, z);
        }
    }
    public static void SetPosZero(GameObject go)
    {
        if (go)
        {
            go.transform.position = Vector3.zero;
        }
    }
    public static void SetPosToOther(GameObject go, GameObject other)
    {
        if (go)
        {
            go.transform.position = other.transform.position;
        }
    }
    public static void SetPosToOther(GameObject go, Transform other)
    {
        if (go)
        {
            go.transform.position = other.position;
        }
    }

    public static void SetLocalPos(GameObject go, float x, float y, float z)
    {
        if (go)
        {
            go.transform.localPosition = new Vector3(x, y, z);
        }
    }
    public static void SetLocalPos(GameObject go, Vector3 pos)
    {
        if (go)
        {
            go.transform.localPosition = pos;
        }
    }
    public static void SetLocalPosX(GameObject go, float x)
    {
        if (go)
        {
            SetLocalPos(go, x, go.transform.localPosition.y, go.transform.localPosition.z);
        }
    }
    public static void SetLocalPosY(GameObject go, float y)
    {
        if (go)
        {
            SetLocalPos(go, go.transform.localPosition.x, y, go.transform.localPosition.z);
        }
    }
    public static void SetLocalPosZ(GameObject go, float z)
    {
        if (go)
        {
            SetLocalPos(go, go.transform.localPosition.x, go.transform.localPosition.y, z);
        }
    }
    public static void SetLocalPosZero(GameObject go)
    {
        if (go)
        {
            go.transform.localPosition = Vector3.zero;
        }
    }
    public static void SetLocalPosToOther(GameObject go, GameObject other)
    {
        if (go)
        {
            go.transform.localPosition = other.transform.localPosition;
        }
    }
    public static void SetLocalPosToOther(GameObject go, Transform other)
    {
        if (go)
        {
            go.transform.localPosition = other.localPosition;
        }
    }

    public static void SetPos(Transform tran, float x, float y, float z)
    {
        if (tran)
        {
            tran.position = new Vector3(x, y, z);
        }
    }
    public static void SetPos(Transform tran, Vector3 pos)
    {
        if (tran)
        {
            tran.transform.position = pos;
        }
    }
    public static void SetPosX(Transform tran, float x)
    {
        if (tran)
        {
            SetPos(tran, x, tran.position.y, tran.position.z);
        }
    }
    public static void SetPosY(Transform tran, float y)
    {
        if (tran)
        {
            SetPos(tran, tran.position.x, y, tran.position.z);
        }
    }
    public static void SetPosZ(Transform tran, float z)
    {
        if (tran)
        {
            SetPos(tran, tran.position.x, tran.position.y, z);
        }
    }
    public static void SetPosZero(Transform tran)
    {
        if (tran)
        {
            tran.position = Vector3.zero;
        }
    }
    public static void SetPosToOther(Transform tran, GameObject other)
    {
        if (tran)
        {
            tran.position = other.transform.position;
        }
    }
    public static void SetPosToOther(Transform tran, Transform other)
    {
        if (tran)
        {
            tran.position = other.position;
        }
    }

    public static void SetLocalPos(Transform tran, float x, float y, float z)
    {
        if (tran)
        {
            tran.localPosition = new Vector3(x, y, z);
        }
    }
    public static void SetLocalPos(Transform tran, Vector3 pos)
    {
        if (tran)
        {
            tran.transform.localPosition = pos;
        }
    }
    public static void SetLocalPosX(Transform tran, float x)
    {
        if (tran)
        {
            SetLocalPos(tran, x, tran.localPosition.y, tran.localPosition.z);
        }
    }
    public static void SetLocalPosY(Transform tran, float y)
    {
        if (tran)
        {
            SetLocalPos(tran, tran.localPosition.x, y, tran.localPosition.z);
        }
    }
    public static void SetLocalPosZ(Transform tran, float z)
    {
        if (tran)
        {
            SetLocalPos(tran, tran.localPosition.x, tran.localPosition.y, z);
        }
    }
    public static void SetLocalPosZero(Transform tran)
    {
        if (tran)
        {
            tran.localPosition = Vector3.zero;
        }
    }
    public static void SetLocalPosToOther(Transform tran, GameObject other)
    {
        if (tran)
        {
            tran.localPosition = other.transform.localPosition;
        }
    }
    public static void SetLocalPosToOther(Transform tran, Transform other)
    {
        if (tran)
        {
            tran.localPosition = other.localPosition;
        }
    }

    #endregion SetPos

    #region SetScale

    public static void SetLocalScale(GameObject go, float x, float y, float z)
    {
        if (go)
        {
            go.transform.localScale = new Vector3(x, y, z);
        }
    }
    public static void SetLocalScaleX(GameObject go, float x)
    {
        if (go)
        {
            SetLocalScale(go, x, go.transform.localScale.y, go.transform.localScale.z);
        }
    }
    public static void SetLocalScaleY(GameObject go, float y)
    {
        if (go)
        {
            SetLocalScale(go, go.transform.localScale.x, y, go.transform.localScale.z);
        }
    }
    public static void SetLocalScaleZ(GameObject go, float z)
    {
        if (go)
        {
            SetLocalScale(go, go.transform.localScale.x, go.transform.localScale.y, z);
        }
    }
    public static void SetLocalScaleOne(GameObject go)
    {
        if (go)
        {
            go.transform.localScale = Vector3.one;
        }
    }
    public static void SetLocalScaleToOther(GameObject go, GameObject other)
    {
        if (go)
        {
            go.transform.localScale = other.transform.localScale;
        }
    }
    public static void SetLocalScaleToOther(GameObject go, Transform other)
    {
        if (go)
        {
            go.transform.localScale = other.localScale;
        }
    }


    public static void SetLocalScale(Transform tran, float x, float y, float z)
    {
        if (tran)
        {
            tran.localScale = new Vector3(x, y, z);
        }
    }
    public static void SetLocalScaleX(Transform tran, float x)
    {
        if (tran)
        {
            SetLocalScale(tran, x, tran.localScale.y, tran.localScale.z);
        }
    }
    public static void SetLocalScaleY(Transform tran, float y)
    {
        if (tran)
        {
            SetLocalScale(tran, tran.localScale.x, y, tran.localScale.z);
        }
    }
    public static void SetLocalScaleZ(Transform tran, float z)
    {
        if (tran)
        {
            SetLocalScale(tran, tran.localScale.x, tran.localScale.y, z);
        }
    }
    public static void SetLocalScaleOne(Transform tran)
    {
        if (tran)
        {
            tran.localScale = Vector3.one;
        }
    }
    public static void SetLocalScaleDouble(Transform tran, float times)
    {
        if (tran)
        {
            tran.localScale = tran.localScale * times;
        }
    }
    public static void SetLocalScaleToOther(Transform tran, GameObject other)
    {
        if (tran)
        {
            tran.localScale = other.transform.localScale;
        }
    }
    public static void SetLocalScaleToOther(Transform tran, Transform other)
    {
        if (tran)
        {
            tran.localScale = other.localScale;
        }
    }

    #endregion SetScale
}
