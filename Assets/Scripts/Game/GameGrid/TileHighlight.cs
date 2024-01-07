using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour, ITileObject
{
    public GameObject Tile { get; private set; }
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        Tile = transform.parent.gameObject;
        Events.onTileEvent.Subscribe(OnTileEvent);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    void OnDestroy()
    {
        Events.onTileEvent.Unsubscribe(OnTileEvent);
    }

    private void OnTileEvent(GameObject sender, object data)
    {
        var clickedTile = sender.GetComponent<GridTile>();        
        if(data is not TileEvent || clickedTile == null || sender != Tile)
            return;
        
        var tileEvent = (TileEvent) data;

        switch (tileEvent.EventType)
        {
            case TileEventType.HOVER_ENTER:
                _spriteRenderer.enabled = true;
                break;
            case TileEventType.HOVER_EXIT:
                _spriteRenderer.enabled = false;
                break;
        }

    }
}
