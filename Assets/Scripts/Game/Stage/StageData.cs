using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Stages
{
    public static readonly Dictionary<Vector2Int, int>[] Data = new Dictionary<Vector2Int, int>[]
    {
        // First stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 2), 5},
            {new Vector2Int(2, 2), 3},
            {new Vector2Int(4, 2), 2},
        },
        // Second stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 2), 3},
            {new Vector2Int(2, 2), 2},
            {new Vector2Int(3, 2), 3},
            {new Vector2Int(3, 1), 2},
        },
        // Third stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 3), 2},
            {new Vector2Int(2, 3), 5},
            {new Vector2Int(3, 3), 2},
            {new Vector2Int(3, 2), 6},
            {new Vector2Int(3, 1), 1},
        },

    };
}
