using UnityEngine;
using System;

public static class SimpleTimer
{

	public static Timer Create(float seconds = 1, GameObject parent = null, bool pausable = false)
	{
		var timer = new GameObject();
        timer.AddComponent<Timer>();
		timer.GetComponent<Timer>().Seconds = seconds;
		if(parent != null)
			timer.transform.parent = parent.transform;
		return timer.GetComponent<Timer>();
	}

}

public class Timer : MonoBehaviour
{
    public float Seconds;
    public Action Timeout;

    private float _remaining;

    void Start()
    {
        _remaining = Seconds;
    }

    void Update()
    {
        _remaining = _remaining - Time.deltaTime;
        if(_remaining <= 0)
        {
            Timeout?.Invoke();
            Destroy(gameObject);
        }
    }

}
