using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public static class GridTileSpawner
{
    public const string PATH = "Prefabs/GridTile";
    
    public static GameObject Spawn(Vector2Int coords, Transform parent)
    {
        var gridTileRes = Resources.Load<GameObject>(PATH);
        var gridTile = GameObject.Instantiate(gridTileRes, new Vector3(coords.x, coords.y, 0), Quaternion.identity, parent);
        gridTile.name = "GridTile" + coords.x + coords.y;
        gridTile.GetComponent<GridTile>().Coords = coords;
        return gridTile;
    }

    public static GameObject SpawnPrefab(Vector2Int coords, Transform parent)
    {
        var gridTileRes = Resources.Load<GameObject>(PATH);
        var gridTile = PrefabUtility.InstantiatePrefab(gridTileRes) as GameObject;
        gridTile.name = "GridTile" + coords.x + coords.y;
        gridTile.GetComponent<GridTile>().Coords = coords;
        gridTile.transform.SetParent(parent);
        gridTile.transform.position = new Vector3(coords.x, coords.y, 0);
        return gridTile;
    }
}

public enum TileEventType
{
    HOVER_ENTER,
    HOVER_EXIT,
    MOUSE_DOWN
}

public class TileEvent
{
    public TileEventType EventType;
}


public class GridTile : MonoBehaviour
{
    public Vector2Int Coords;

    public List<GameObject> GetTileObjects()
    {
        var tileObjects = new List<GameObject>();
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<ITileObject>() != null)
            {
                tileObjects.Add(child.gameObject);
            }
        }
        return tileObjects;
    }

    public GameObject FindTileObject<T>()
    {
        var tileObjects = GetTileObjects();
        foreach (GameObject tileObject in tileObjects)
        {
            if(tileObject.GetComponent<T>() != null)
                return tileObject;
        }
        return null;
    }
    
    void OnMouseEnter()
    {
        Events.onTileEvent.Invoke(gameObject, new TileEvent(){EventType = TileEventType.HOVER_ENTER});
    }

    void OnMouseExit()
    {
        Events.onTileEvent.Invoke(gameObject, new TileEvent(){EventType = TileEventType.HOVER_EXIT});
    }

    void OnMouseDown()
    {
        Events.onTileEvent.Invoke(gameObject, new TileEvent(){EventType = TileEventType.MOUSE_DOWN});
    }
}
