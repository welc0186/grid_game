using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameGridSpawner
{
    public const string PATH = "Prefabs/GameGrid";
    public static GameObject SpawnGrid(int width, int height)
    {
        var gameGridRes = Resources.Load<GameObject>(PATH);
        var gameGrid = GameObject.Instantiate(gameGridRes);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var coords = new Vector2Int(x, y);
                var gridTile = GridTileSpawner.Spawn(coords, gameGrid.transform);
                gameGrid.GetComponent<GameGrid>().Tiles.Add(coords,gridTile);
            }
        }
        return gameGrid;
    }

    public static GameObject SpawnPrefab(int width, int height)
    {
        var gameGridRes = Resources.Load<GameObject>(PATH);
        var gameGrid = PrefabUtility.InstantiatePrefab(gameGridRes) as GameObject;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var coords = new Vector2Int(x, y);
                var gridTile = GridTileSpawner.SpawnPrefab(coords, gameGrid.transform);
                // gameGrid.GetComponent<GameGrid>().Tiles.Add(coords,gridTile);
            }
        }
        return gameGrid;
    }
}

public class GameGrid : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> Tiles { get; private set; }

    void Awake()
    {
        Tiles = new Dictionary<Vector2Int, GameObject>();
    }

    void Start()
    {
        UpdateTiles();
    }

    void UpdateTiles()
    {
        foreach(Transform child in transform)
        {
            GridTile gridTile;
            child.gameObject.TryGetComponent<GridTile>(out gridTile);
            if(gridTile == null)
                continue;
            Tiles.Add(gridTile.Coords, child.gameObject);
        }
    }

}