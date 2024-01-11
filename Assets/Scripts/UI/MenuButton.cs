using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Alf.UI;
using TMPro;

public class ButtonMessage
{
	public string Text { get; private set; }
	public ButtonMessage(string message)
	{
		Text = message;
	}
}

public class MenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
	public ButtonMessage Message;

    void Awake()
	{
		GetComponent<Button>().onClick.AddListener( () => {Events.onButtonPressed.Invoke(gameObject, Message);});
		SetTextColor(true);
	}

	public void OnSelect(BaseEventData eventData)
    {
        Events.onFocusEntered.Invoke(gameObject, null);
		SetTextColor(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
		SetTextColor(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTextColor(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTextColor(true);
    }

	void SetTextColor(bool primaryColor)
	{
		if(primaryColor)
		{
			GetComponentInChildren<TMP_Text>().color = Colors.Value("PRIMARY");
			return;
		}
		GetComponentInChildren<TMP_Text>().color = Colors.Value("SECONDARY");
	}

}
