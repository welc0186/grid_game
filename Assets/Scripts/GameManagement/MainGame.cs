using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainGame : MonoBehaviour
{
	public readonly Vector2 BALL_SPAWN_LOC = Vector2.zero;

	private Ball _ball;
	// private PackedScene ballParticleScene;
	
	// Called when the node enters the scene tree for the first time.
	void Awake()
	{
		Events.onGoalEnter.Subscribe(OnGoalEnter);
		Events.onNewGame.Subscribe(SpawnBallEvent);
		Events.onMainMenuRequested.Subscribe(ToMainMenu);
	}

    void Start()
    {
		SpawnBall();
    }

    void OnDestroy()
	{
		Events.onGoalEnter.Unsubscribe(OnGoalEnter);
		Events.onNewGame.Unsubscribe(SpawnBallEvent);
		Events.onMainMenuRequested.Unsubscribe(ToMainMenu);
	}

    private void ToMainMenu(GameObject sender, object data)
    {
        Debug.Log("TO-DO: Load main menu");
    }


    private void OnGoalEnter(GameObject sender, object data)
    {
		// BallDeathParticleSpawner.Spawn(_ball.GlobalPosition, this);
		if(sender.name == ScoreLabel.ENEMY_GOAL)
		{
			SpawnBall();
			return;
		}
		Events.onGameOver.Invoke(null, null);
    }

    void SpawnBall()
	{
		if (_ball != null)
		{
            Destroy(_ball.gameObject);
		}
		_ball = BallSpawner.Spawn();
		_ball.transform.position = BALL_SPAWN_LOC;
	}

	void SpawnBallEvent(GameObject sender, object data)
	{
		SpawnBall();
	}

}
