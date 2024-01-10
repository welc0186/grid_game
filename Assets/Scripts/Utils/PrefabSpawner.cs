using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabSpawner
{

    public static GameObject Spawn(string path, Transform parent = null)
    {
        var resource = Resources.Load<GameObject>(path);
        if (resource == null)
            return null;

        GameObject newGameObject;
        if (parent != null)
        {
            newGameObject = GameObject.Instantiate(resource, parent);
        } else
        {
            newGameObject = GameObject.Instantiate(resource);
        }
        return newGameObject;
    }

}
