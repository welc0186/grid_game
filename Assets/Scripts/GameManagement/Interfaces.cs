using UnityEngine;
using System;

public interface IMenuItemFactory
{
	public GameObject MakeMenuItem();
}

// public interface IGameSetting
// {
// 	public SettingCategory Name { get; }
// 	public void Update(IGameSetting newSetting);
// }
