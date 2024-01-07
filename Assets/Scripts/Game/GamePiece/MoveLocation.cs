using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocationManager
{
    public const string PATH = "Prefabs/MoveLocation";
    public List<GameObject> Locations { get; private set; }

    private GameObject _moveLocationPrefab;

    public MoveLocationManager()
    {
        _moveLocationPrefab = Resources.Load<GameObject>(PATH);
        Locations = new List<GameObject>();
    }

    public GameObject Spawn(Transform parent)
    {
        var moveLocation = GameObject.Instantiate(_moveLocationPrefab, parent);
        Locations.Add(moveLocation);
        return moveLocation;
    }

    public void Reset()
    {
        foreach(GameObject moveLocation in Locations)
        {
            GameObject.Destroy(moveLocation);
        }
        Locations = new List<GameObject>();
    }
    
}



public class MoveLocation : MonoBehaviour, ITileObject
{
    public GameObject Tile { get; private set; }
}
