using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class BallSpawner
{

    public const string FILEPATH = "Prefabs/Ball";

	public static Ball Spawn()
	{
		var ballObject = Resources.Load<GameObject>(FILEPATH);
		Ball ball = GameObject.Instantiate(ballObject, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
		return ball;
	}
}

public class Ball : MonoBehaviour
{
	public const float BALL_SPEED = 20f;
	public const float KICK_DELAY = 0.5f;

	// Max should not exceed Pi/2 radians (mirrored)
	public const float KICK_RADS_MIN = 0.7f;  //~40 degrees
	public const float KICK_RADS_MAX = 1.22f; //~70 degrees
	public const float PI = 3.14f;

	void Awake()
	{
		if(gameObject.scene.name != "MainGame")
			return;
		SimpleTimer.Create(KICK_DELAY, gameObject).Timeout += Kick;
		Events.onBallSpawned.Invoke(gameObject, null);
	}

	void OnCollisionEnter(Collision collision)
	{
		Events.onBallCollided.Invoke(gameObject, null);
	}

	void Kick()
	{
		// Random initial angle
		var kickRads = Random.Range(KICK_RADS_MIN, KICK_RADS_MAX);

		// Random left/right direction
		kickRads = Random.Range(0,1) > 0.5f? kickRads : PI - kickRads;

		var kickVector = Vector2.right.rotate(-kickRads);
		GetComponent<Rigidbody2D>().velocity = kickVector * BALL_SPEED;
		Events.onBallKicked.Invoke(gameObject, null);
	}

}
