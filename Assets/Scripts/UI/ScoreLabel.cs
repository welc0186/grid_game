using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreLabel : MonoBehaviour
{
	public const string ENEMY_GOAL = "EnemyGoal";
	private int _score;
    private TMP_Text _label;

	void Awake()
	{
		_label = GetComponent<TMP_Text>();
		Events.onNewGame.Subscribe(OnNewGame);
		UpdateScore(0);
	}

    void OnDestroy()
	{
		Events.onNewGame.Unsubscribe(OnNewGame);
	}

    private void OnNewGame(GameObject sender, object data)
    {
        UpdateScore(0);
    }

	void UpdateScore(int score)
	{
		_score = score;
		_label.text = _score.ToString();
	}

	void OnGoalEnter(GameObject goal, object data)
	{
		if(goal.name == ENEMY_GOAL)
		{
			_score++;
			UpdateScore(_score);
		}
	}
}
