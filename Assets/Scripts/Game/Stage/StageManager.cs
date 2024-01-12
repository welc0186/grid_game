using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public const float TRANSITION_SECONDS = 1f;

    private int _stage;
    private List<GameObject> _gridPieces;
    private GameGrid _gameGrid;
    private bool _transitioning;

    void Awake()
    {
        _stage = 0;
        _gridPieces = new List<GameObject>();
        Events.onButtonPressed.Subscribe(HandleButtonPress);
    }

    void Start()
    {
        FindGameGrid();
        InitStage();
    }

    void OnDestroy()
    {
        Events.onButtonPressed.Unsubscribe(HandleButtonPress);
    }

    private void HandleButtonPress(GameObject sender, object data)
    {
        if (data is not ButtonMessage) return;
        var buttonMessage = (ButtonMessage) data;
        switch (buttonMessage.Text)
        {
            case "ResetBoard":
                InitStage();
                break;
            case "New Game":
                _stage = 0;
                InitStage();
                break;
            default:
                break;
        }
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
        var pieceExists = GridPieceExists();
        if (_gameGrid == null)
            FindGameGrid();
        if (pieceExists)
            _transitioning = false;
        if (!pieceExists && !_transitioning)
        {
            _transitioning = true;
            StartCoroutine(ClearStage());
        }
    }

    IEnumerator ClearStage()
    {
        yield return new WaitForSeconds(TRANSITION_SECONDS);
        _stage++;
        if(_stage == Stages.Data.Length)
            _stage = 0;
        InitStage();
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
