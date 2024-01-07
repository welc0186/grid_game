using UnityEngine;
using System;


public static class Events
{
    // ** UI **
    public static readonly CustomEvent onButtonPressed = new CustomEvent();
    public static readonly CustomEvent onFocusEntered  = new CustomEvent();

    // ** Main Game **
    public static readonly CustomEvent onTileEvent = new CustomEvent();

    // ** Game State **
    public static readonly CustomEvent onNewGame           = new CustomEvent();
    public static readonly CustomEvent onGameOver          = new CustomEvent();
    public static readonly CustomEvent onMainMenuRequested = new CustomEvent();

    // ** Config **
    public static readonly CustomEvent onSettingsChanged = new CustomEvent();
}

public class CustomEvent
{
    private event Action<GameObject, object> _event;

    public void Invoke(GameObject sender, object data)
    {
        _event?.Invoke(sender, data);
    }

    public void Subscribe(Action<GameObject, object> listener)
    {
        _event += listener;
    }

    public void Unsubscribe(Action<GameObject, object> listener)
    {
        _event -= listener;
    }
}

