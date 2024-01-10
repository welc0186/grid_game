using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    
    private int _stage;
    private List<GameObject> _gridPieces;
    private GameGrid _gameGrid;

    void Awake()
    {
        _stage = 0;
        _gridPieces = new List<GameObject>();
    }

    void Start()
    {
        FindGameGrid();
        InitStage();
    }
    
    void InitStage()
    {
        DestroyPieces();
        foreach(KeyValuePair<Vector2Int, int> entry in Stages.Data[_stage])
        {
            _gridPieces.Add(GridPieceSpawner.Spawn(_gameGrid.Tiles[entry.Key].transform, entry.Value));
        }
    }

    void DestroyPieces()
    {
        foreach(GameObject piece in _gridPieces)
        {
            if (piece != null)
                Destroy(piece);
        }
        _gridPieces = new List<GameObject>();
    }

    void Update()
    {
        if (_gameGrid == null)
            FindGameGrid();
        if (!GridPieceExists() && _stage < Stages.Data.Length - 1)
        {
            _stage++;
            InitStage();
        }
    }

    bool GridPieceExists()
    {
        var ret = false;
        foreach (GameObject piece in _gridPieces)
        {
            if (piece != null)
                ret = true;
        }
        return ret;
    }

    private void FindGameGrid()
    {
        _gameGrid = GameObject.Find("GameGrid")?.GetComponent<GameGrid>();
    }
}
