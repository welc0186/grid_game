using UnityEngine;
using System;

public static class Vector2Extensions
{
    public static Vector2 rotate(this Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}

public static class Vector2IntExtensions
{
    public static Vector2Int sign(this Vector2Int v)
    {
        var ret = Vector2Int.zero;
        if(v.x > 0)
            ret.x = 1;
        if(v.x < 0)
            ret.x = -1;
        if(v.y > 0)
            ret.y = 1;
        if(v.y < 0)
            ret.y = -1;
        return ret;
    }
}

public static class TransformExtensions
{
    public static Transform FindRecursive(this Transform self, string exactName) => self.FindRecursive(child => child.name == exactName);

    public static Transform FindRecursive(this Transform self, Func<Transform, bool> selector)
    {
        foreach (Transform child in self)
        {
            if (selector(child))
            {
                return child;
            }

            var finding = child.FindRecursive(selector);

            if (finding != null)
            {
                return finding;
            }
        }

        return null;
    }
}