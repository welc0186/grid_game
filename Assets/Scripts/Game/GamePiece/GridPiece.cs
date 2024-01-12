using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class GridPieceSpawner
{
    public const string PATH = "Prefabs/GridPiece";
    public static GameObject Spawn(Transform parent, int value = 1)
    {
        var gridPieceRes = Resources.Load<GameObject>(PATH);
        var gridPiece = GameObject.Instantiate(gridPieceRes, parent);
        gridPiece.GetComponent<GridPiece>().UpdateValue(value);
        return gridPiece;
    }
}

public class GridPiece : MonoBehaviour, ITileObject
{
    public const int MIN_VALUE = 0;
    public const int MAX_VALUE = 6;
    
    public GameObject Tile { get; private set; }
    public int Value { get; private set; }
    public bool Selected { get; private set; }

    private SpriteMB _spriteMB;
    private GameGrid _gameGrid;
    private MoveLocationManager _moveLocations;

    void Awake()
    {
        Selected = false;
        _moveLocations = new MoveLocationManager();
        _spriteMB = GetComponentInChildren<SpriteMB>();
        Events.onTileEvent.Subscribe(HandleTileEvent);
    }

    void OnDestroy()
    {
        Events.onGridPieceDestroy.Invoke(gameObject, null);
        Events.onTileEvent.Unsubscribe(HandleTileEvent);
        if(_moveLocations != null)
            _moveLocations.Reset();
    }

    private void HandleTileEvent(GameObject sender, object data)
    {
        if(data is not TileEvent)
            return;
        var tileEvent = (TileEvent) data;
        if (tileEvent.EventType == TileEventType.MOUSE_DOWN && sender.GetComponent<GridTile>() != null)
        {
            HandleMouseDown(sender);
        }
        
    }

    private void HandleMouseDown(GameObject tileGameObject)
    {
        GridTile tile;
        if (!tileGameObject.TryGetComponent<GridTile>(out tile))
            return;
        
        // Clicked on own tile
        if(tile.gameObject == Tile)
        {
            Select();
            return;
        }

        // Selected and clicked on valid move location (jump a piece)
        if(Selected && tile.FindTileObject<MoveLocation>() != null)
        {
            var direction = (tile.Coords - Tile.GetComponent<GridTile>().Coords).sign();
            var jumpedPieceCoords = Tile.GetComponent<GridTile>().Coords + direction;
            var jumpedPiece = _gameGrid.Tiles[jumpedPieceCoords].GetComponent<GridTile>().FindTileObject<GridPiece>();
            _moveLocations.Reset();
            HandleTileMove(tileGameObject);
            SimpleTimer.Create(0.5f).Timeout += () => JumpPiece(jumpedPiece);
            return;
        }

        // Clicked on anything else
        _moveLocations.Reset();
        Selected = false;
    }

    private void JumpPiece(GameObject jumpedPieceObj)
    {
        GridPiece jumpedPiece;
        jumpedPieceObj.TryGetComponent<GridPiece>(out jumpedPiece);
        if (jumpedPiece == null)
            return;
        var jumpedValue = jumpedPiece.Value;
        Destroy(jumpedPieceObj);
        UpdateValue(Mathf.Abs(jumpedValue - Value));
    }

    public void UpdateValue(int value)
    {
        if(value < MIN_VALUE || value > MAX_VALUE)
        {
            Debug.Log("Can't update value - out of range");
            return;
        }
        
        Value = value;
        if(Value <= 0)
        {
            Destroy(gameObject);
            return;
        }
        _spriteMB.SetSprite(Value - 1);
    }

    public void Select()
    {
        if(Selected)
            return;
        Selected = true;

        // Spawn valid move location icons
        var checkDirections = new Vector2Int[] {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
        };
        foreach(Vector2Int checkDirection in checkDirections)
        {
            var origin = Tile.GetComponent<GridTile>().Coords;
            if(!IsValidMoveDirection(origin, checkDirection))
                continue;
            var parentTile = _gameGrid.Tiles[origin + checkDirection + checkDirection];
            _moveLocations.Spawn(parentTile.transform);
        }
        Events.onGridPieceSelect.Invoke(gameObject, null);
    }

    bool IsValidMoveDirection(Vector2Int origin, Vector2Int direction)
    {
        var checkGamePiece = origin + direction;
        var checkEmpty = checkGamePiece + direction;

        //Both check locations exist
        if(!_gameGrid.Tiles.ContainsKey(checkGamePiece) || !_gameGrid.Tiles.ContainsKey(checkEmpty))
            return false;

        //Adjacent location contains a grid piece
        if(!_gameGrid.Tiles[checkGamePiece].GetComponent<GridTile>().FindTileObject<GridPiece>())
            return false;

        //Adjacent to adjacent location is empty
        if(_gameGrid.Tiles[checkEmpty].GetComponent<GridTile>().FindTileObject<GridPiece>())
            return false;
        
        return true;
    }

    private void HandleTileMove(GameObject tileGameObject)
    {
        if (tileGameObject.GetComponent<GridTile>() == null)
            return;
        
        Tile = tileGameObject;
        transform.SetParent(tileGameObject.transform);
        transform.localPosition = Vector3.zero;
        Selected = false;
        Events.onGridPieceJump.Invoke(gameObject, null);
    }

    void FindParentTile()
    {
        var colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<GridTile>() != null)
            {
                Tile = collider.gameObject;
                transform.SetParent(collider.transform);
                transform.localPosition = Vector3.zero;
                return;
            }
        }

        return;
    }

    private void FindGameGrid()
    {
        GameGrid gameGrid;
        transform.root.gameObject.TryGetComponent<GameGrid>(out gameGrid);
        if (gameGrid != null)
        {
            _gameGrid = gameGrid;
        }
    }

    // TO-DO: Find way to avoid missingreferenceexception entirely
    void OnValidate()
    {
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.delayCall += () => 
        {
            try
            {
                if(gameObject == null) return;
                var spriteMB = GetComponentInChildren<SpriteMB>();
                if(spriteMB != null)
                spriteMB.SetSprite(Value - 1);
            } catch (MissingReferenceException e)
            {
                Debug.Log("Missing reference: " + e.Message);
            }
        };
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            FindParentTile();
        }
        if(Tile == null)
        {
            Tile = transform.parent.gameObject;
        }
        if(_gameGrid == null)
        {
            FindGameGrid();
        }
    }


}
