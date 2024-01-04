using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleScreen : MonoBehaviour
{

	void Awake()
	{
		Events.onNewGame.Subscribe(OnNewGame);
	}

	void OnDestroy()
	{
		Events.onNewGame.Unsubscribe(OnNewGame);
	}

    private void OnNewGame(GameObject sender, object data)
    {
        SceneManager.LoadScene("MainGame");
    }

}
