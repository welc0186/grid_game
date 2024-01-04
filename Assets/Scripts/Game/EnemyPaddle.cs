using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPaddle : MonoBehaviour
{
    public const float PADDLE_SPEED      = 50f;
	public const float REACT_SECONDS     = 1f;
	public const float COURT_WIDTH       = 18f;

	private Ball _ball;
	private Vector2 _lastBallPos;
	private Vector2 _targetPos;
	private Vector2? _wayPoint;
    private Rigidbody2D _rigidbody;
	
	// Called when the node enters the scene tree for the first time.
	void Awake()
	{
		Events.onBallSpawned.Subscribe(OnBallSpawned);
        _rigidbody = GetComponent<Rigidbody2D>();
	}

    void OnDestroy()
    {
        Events.onBallSpawned.Unsubscribe(OnBallSpawned);
    }

    private void OnBallSpawned(GameObject sender, object data)
    {
        if(sender.GetComponent<Ball>() != null)
		{
			_ball = sender.GetComponent<Ball>();
			_lastBallPos = _ball.transform.position;
			_wayPoint = null;
		}
    }

    void FixedUpdate()
	{
		ProcessWaypoint();
		MoveToBall();
	}

    private void ProcessWaypoint()
    {
		if(_ball == null)
		{
			_wayPoint = null;
			return;
		}

		if(_wayPoint == null && _ball.transform.position.y > _lastBallPos.y)
		{
			SimpleTimer.Create(REACT_SECONDS, gameObject, true).Timeout += () => {
				_wayPoint = null;
			};
			_wayPoint = CalculateIntercept(_lastBallPos, _ball.transform.position);
		}
		_lastBallPos = _ball.transform.position;
		
    }

    private Vector2? CalculateIntercept(Vector2 lastBallPos, Vector2 currBallPos)
    {

			float xIntercept;
			var intercepted = Math.XIntercept(out xIntercept, lastBallPos, currBallPos, transform.position.y);

			if(!intercepted)
			{
				return null;
			}

			int attempts = 0;
			while(xIntercept < -COURT_WIDTH / 2 || xIntercept > COURT_WIDTH / 2)
			{
				if(xIntercept < -COURT_WIDTH / 2)
				{
					xIntercept = xIntercept - COURT_WIDTH;
				}
				if(xIntercept > COURT_WIDTH / 2)
				{
					xIntercept = COURT_WIDTH - xIntercept;
				}
				attempts++;
				if(attempts > 100)
					break;
			}

			return new Vector2(xIntercept, transform.position.y);
    }

    private void MoveToBall()
    {
        if (_ball == null || _wayPoint == null)
		{
			_rigidbody.velocity = Vector2.zero;
			return;
		}
		var waypointDistance = Vector2.Distance(transform.position, _wayPoint.Value);
		if(waypointDistance > 2f)
		{
			float direction = Mathf.Sign(_wayPoint.Value.x - transform.position.x);
			_rigidbody.velocity = new Vector2(direction, 0) * PADDLE_SPEED;
			return;
		}
		_rigidbody.velocity = Vector2.zero;
    }

	void OnDrawGizmos()
	{
		if(_wayPoint != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(_wayPoint.Value, 0.4f);
		}
	}

}
