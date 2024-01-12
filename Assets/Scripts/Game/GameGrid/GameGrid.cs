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

#if UNITY_EDITOR
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
#endif

}

public class GameGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> _tiles;
    
    public Dictionary<Vector2Int, GameObject> Tiles
        { 
            get
            { 
                if (_tiles == null)
                    UpdateTiles();
                return _tiles;
            } 
            set {}
        }

    void Start()
    {
        UpdateTiles();
    }

    void UpdateTiles()
    {
        _tiles = new Dictionary<Vector2Int, GameObject>();
        foreach(Transform child in transform)
        {
            GridTile gridTile;
            child.gameObject.TryGetComponent<GridTile>(out gridTile);
            if(gridTile == null)
                continue;
            _tiles.Add(gridTile.Coords, child.gameObject);
        }
    }

}