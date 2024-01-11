using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ButtonFactory : IMenuItemFactory
{
    
	GameObject _buttonPrefab;
	string _text;
	ButtonMessage _message;
	Sprite _logo;

	public ButtonFactory(string text, ButtonMessage m, Sprite logo = null)
	{
		_buttonPrefab = Resources.Load<GameObject>("UI/MenuButton");
		_text = text;
		_message = m;
		_logo = logo;
	}
	
	public GameObject MakeMenuItem()
    {
        var button = GameObject.Instantiate(_buttonPrefab);
		button.GetComponentInChildren<TMP_Text>().text = _text;
		button.GetComponent<MenuButton>().Message = _message;
		if(_logo == null)
		{
			GameObject.Destroy(button.transform.FindRecursive("Logo").gameObject);
		} else
		{
			button.transform.FindRecursive("Logo").gameObject.GetComponent<Image>().sprite = _logo;
		}
		return button;
    }

}
