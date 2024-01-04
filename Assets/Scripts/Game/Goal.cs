using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Ball>() != null)
        {
            Events.onGoalEnter.Invoke(gameObject, null);
        }
    }
}
