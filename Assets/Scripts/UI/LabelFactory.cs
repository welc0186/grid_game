using UnityEngine;
using System;
using TMPro;

public class LabelFactory : IMenuItemFactory
{
    
	GameObject _labelPrefab;
	string _text;

	public LabelFactory(string text)
	{
		_labelPrefab = Resources.Load<GameObject>("UI/MenuLabel");
		_text = text;
	}
	
	public GameObject MakeMenuItem()
    {
        var label = GameObject.Instantiate(_labelPrefab);
		label.GetComponent<TMP_Text>().text = _text;
		return label;
    }

}
