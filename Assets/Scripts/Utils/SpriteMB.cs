using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMB : MonoBehaviour
{

    [SerializeField] private Sprite[] _sprites; 

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(int spriteIndex)
    {
        #if UNITY_EDITOR
        _spriteRenderer = GetComponent<SpriteRenderer>();
        #endif
        if(spriteIndex >= 0 && spriteIndex < _sprites.Length)
        {
            _spriteRenderer.sprite = _sprites[spriteIndex];
        }
    }

}
