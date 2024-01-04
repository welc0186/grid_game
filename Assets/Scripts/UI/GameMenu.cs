using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class GameMenu : MonoBehaviour
{
	
	bool _paused;
	GameObject _menuParent;
	IMenuItemFactory[] _titleMenu;
	IMenuItemFactory[] _settingsMenu;
	IMenuItemFactory[] _pauseMenu;
	IMenuItemFactory[] _gameOverMenu;

	void Awake()
	{

		// ** Title Menu **
		_titleMenu = new IMenuItemFactory[] {
			new LabelFactory("PONG"),
			new ButtonFactory("Play",     new ButtonMessage("New Game")),
			new ButtonFactory("Settings", new ButtonMessage("Settings Menu")),
			new ButtonFactory("Quit",     new ButtonMessage("Quit Game"))
		};

		// TO-DO: Add Settings
		// ** Settings Menu **
		_settingsMenu = new IMenuItemFactory[] {
			new LabelFactory("Settings"),
			// new SettingsCheckboxFactory("Sound", new GameSetting<bool>(SettingCategory.SFXON, true)),
			// new SettingsSliderFactory(new GameSetting<int>(SettingCategory.SFXVOLUMEDB, 8)),
			// new LabelFactory("Music"),
			// new SettingsSliderFactory(new GameSetting<int>(SettingCategory.MUSICVOLUMEDB, 8)),
			new ButtonFactory("Back", new ButtonMessage("Main Menu")),
		};

		// ** Pause Menu **
		_pauseMenu = new IMenuItemFactory[] {
			new LabelFactory("Game Paused"),
			new ButtonFactory("New",       new ButtonMessage("New Game")),
			new ButtonFactory("Resume",    new ButtonMessage("Resume Game")),
			new ButtonFactory("Main Menu", new ButtonMessage("Main Menu")),
			new ButtonFactory("Quit",      new ButtonMessage("Quit Game"))
		};

		// ** Game Over Menu **
		_gameOverMenu = new IMenuItemFactory[] {
			new LabelFactory("Game Over"),
			new ButtonFactory("New",       new ButtonMessage("New Game")),
			new ButtonFactory("Main Menu", new ButtonMessage("Main Menu")),
			new ButtonFactory("Quit",      new ButtonMessage("Quit Game"))
		};

		_menuParent = GameObject.Find("MenuParent");

		if(SceneManager.GetActiveScene().name == "MainGame")
		{
			Unpause();
		}
		if(SceneManager.GetActiveScene().name == "MainMenu")
		{
			Pause();
			CreateMenu(_titleMenu);
		}
		Events.onButtonPressed.Subscribe(OnButtonPressed);
		Events.onGameOver.Subscribe(OnGameOver);
	}

    private void Unpause()
    {
        _paused = false;
		Time.timeScale = 1f;
		_menuParent.SetActive(false);
    }

	private void Pause()
	{
		_paused = true;
		Time.timeScale = 0;
		_menuParent.SetActive(true);
	}

    void OnDestroy()
	{
		Events.onButtonPressed.Unsubscribe(OnButtonPressed);
		Events.onGameOver.Unsubscribe(OnGameOver);
	}

    private void OnGameOver(GameObject sender, object data)
    {
        Pause();
		CreateMenu(_gameOverMenu);
    }

    private void CreateMenu(IMenuItemFactory[] menuItems)
    {
		foreach(Transform child in _menuParent.transform)
		{
			if(child.gameObject != _menuParent.gameObject)
				Destroy(child.gameObject);
		}

		bool firstFocus = true;
		foreach(IMenuItemFactory factory in menuItems)
		{
			var menuItem = (GameObject) factory.MakeMenuItem();
			menuItem.transform.SetParent(_menuParent.transform);
			if(firstFocus && menuItem.GetComponentInChildren<Selectable>() != null) 
			{
				firstFocus = false;
				menuItem.GetComponentInChildren<Selectable>().Select();
			}
		}
    }

    private void OnButtonPressed(GameObject sender, object data)
    {
		if(sender.GetComponent<MenuButton>() == null)
		{
			return;
		}
		var button = sender.GetComponent<MenuButton>();
		switch(button.Message.Text)
		{
			case "New Game":
				Events.onNewGame.Invoke(gameObject, null);
				Unpause();
				break;
			case "Resume Game":
				Unpause();
				break;
			case "Main Menu":
				Events.onMainMenuRequested.Invoke(gameObject, null);
				Pause();
				CreateMenu(_titleMenu);
				break;
			case "Settings Menu":
				CreateMenu(_settingsMenu);
				break;
			case "Quit Game":
				Application.Quit();
				break;
			default:
				break;
		}
    }

    void Update()
	{	
		if (Input.GetButton("Cancel") && SceneManager.GetActiveScene().name == "MainGame")
		{
			if(!_paused)
			{
				Pause();
				CreateMenu(_pauseMenu);
				return;
			}
			Unpause();
		}
	}

    private void PressPause()
    {
		_paused = !_paused;
		Time.timeScale = _paused ? 0 : 1;
		gameObject.SetActive(!_paused);
    }

}
