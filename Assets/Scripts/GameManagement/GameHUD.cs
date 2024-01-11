using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHUD : MonoBehaviour
{

    public const string RESET_PATH = "Sprites/ui_sprite_sheet_16x16";
    public const int    RESET_PATH_INDEX = 6;

    // Start is called before the first frame update
    void Start()
    {
        LoadResetButton();
    }

    private void LoadResetButton()
    {
        var resetSprite = Resources.LoadAll<Sprite>(RESET_PATH)[RESET_PATH_INDEX];
        var resetButton = new ButtonFactory("", new ButtonMessage("ResetBoard"), resetSprite).MakeMenuItem();
        resetButton.transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
