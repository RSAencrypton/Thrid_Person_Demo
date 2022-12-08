using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindObjectMethod
{

    public static Transform FindExcuateObject(this Transform parent, string targetName) {
        Transform tmp = null;
        foreach (Transform item in parent)
        {
            if (item.name == targetName)
            {
                return item;
            }
            else {
                tmp = FindExcuateObject(item, targetName);
                if (tmp != null) {
                    return tmp;
                }
            }
        }

        return null;
    }
}
