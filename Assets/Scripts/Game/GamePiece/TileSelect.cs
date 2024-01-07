using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private GridPiece _gridPiece;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    void Start()
    {
        _gridPiece = transform.parent.gameObject.GetComponent<GridPiece>();
    }

    void Update()
    {
        if(_gridPiece.Selected)
        {
            _spriteRenderer.enabled = true;
            return;
        }
        _spriteRenderer.enabled = false;
    }

}
