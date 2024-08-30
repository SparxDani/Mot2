using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    private int currentPointIndex = 0;

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }
}