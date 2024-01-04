using UnityEngine;
using System;
using TMPro;

public class ButtonFactory : IMenuItemFactory
{
    
	GameObject _buttonPrefab;
	string _text;
	ButtonMessage _message;

	public ButtonFactory(string text, ButtonMessage m)
	{
		_buttonPrefab = Resources.Load<GameObject>("UI/MenuButton");
		_text = text;
		_message = m;
	}
	
	public GameObject MakeMenuItem()
    {
        var button = GameObject.Instantiate(_buttonPrefab);
		button.GetComponentInChildren<TMP_Text>().text = _text;
		button.GetComponent<MenuButton>().Message = _message;
		return button;
    }

}
