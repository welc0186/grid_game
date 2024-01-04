using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMessage
{
	public string Text { get; private set; }
	public ButtonMessage(string message)
	{
		Text = message;
	}
}

public class MenuButton : MonoBehaviour, ISelectHandler
{
	public ButtonMessage Message;

    void Awake()
	{
		GetComponent<Button>().onClick.AddListener( () => {Events.onButtonPressed.Invoke(gameObject, null);});
	}

	public void OnSelect(BaseEventData eventData)
    {
        Events.onFocusEntered.Invoke(gameObject, null);
    }

}
