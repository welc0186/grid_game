using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileFloorSpawner
{
    public const string PATH = "Prefabs/TileFloor";

    public static GameObject Spawn(GameObject tile)
    {
        var tileFloorRes = Resources.Load<GameObject>(PATH);
        var tileFloor = GameObject.Instantiate(tileFloorRes);
        tileFloor.GetComponent<TileFloor>().Attach(tile);
        return tileFloor;
    }
}

public class TileFloor : MonoBehaviour, ITileObject
{
    public GameObject Tile { get; private set; }

    public bool Attach(GameObject tile)
    {
        Tile = tile;
        transform.SetParent(tile.transform, false);
        return true;
    }
}
