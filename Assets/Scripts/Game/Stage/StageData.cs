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
        // Fourth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(2, 0), 4},
            {new Vector2Int(2, 1), 5},
            {new Vector2Int(3, 2), 5},
            {new Vector2Int(4, 2), 1},
            {new Vector2Int(2, 3), 3},
            {new Vector2Int(2, 4), 6},
        },
        // Fifth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 1), 4},
            {new Vector2Int(2, 1), 6},
            {new Vector2Int(3, 1), 1},
            {new Vector2Int(1, 2), 3},
            {new Vector2Int(2, 2), 1},
            {new Vector2Int(3, 2), 6},
            {new Vector2Int(3, 3), 3},
        },
        // Sixth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(0, 1), 3},
            {new Vector2Int(1, 1), 2},
            {new Vector2Int(3, 1), 1},
            {new Vector2Int(4, 1), 4},
            {new Vector2Int(0, 2), 3},
            {new Vector2Int(1, 2), 4},
            {new Vector2Int(3, 2), 1},
            {new Vector2Int(4, 2), 6},
        },
        // Seventh stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 0), 2},
            {new Vector2Int(1, 1), 5},
            {new Vector2Int(2, 1), 3},
            {new Vector2Int(2, 2), 5},
            {new Vector2Int(1, 3), 6},
            {new Vector2Int(2, 3), 5},
        },
        // Eighth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 1), 5},
            {new Vector2Int(1, 2), 4},
            {new Vector2Int(2, 2), 4},
            {new Vector2Int(3, 2), 2},
            {new Vector2Int(0, 3), 6},
            {new Vector2Int(3, 3), 3},
        },
        // Ninth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 1), 4},
            {new Vector2Int(4, 1), 6},
            {new Vector2Int(1, 2), 5},
            {new Vector2Int(2, 2), 6},
            {new Vector2Int(3, 2), 5},
            {new Vector2Int(3, 3), 6},
        },
        // Tenth stage
        new Dictionary<Vector2Int, int>
        {
            {new Vector2Int(1, 1), 1},
            {new Vector2Int(4, 1), 3},
            {new Vector2Int(1, 2), 5},
            {new Vector2Int(2, 2), 6},
            {new Vector2Int(3, 2), 5},
            {new Vector2Int(3, 3), 6},
        },

    };
}
