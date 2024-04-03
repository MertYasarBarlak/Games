using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollewer : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    public float chaseSpeed = 4f;
    public float speed = 2f;

    [SerializeField] private GameObject detectionArea;
    [NonSerialized] public Collider2D target = null;
    private DetectionArea playerTrigger;

    private void Start()
    {
        playerTrigger = detectionArea.GetComponent<DetectionArea>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTrigger.playerDetected)
        {
            if ((target.gameObject.transform.position - transform.position).x > 0) transform.localScale = new Vector3(1, 1, 1);
            if ((target.gameObject.transform.position - transform.position).x < 0) transform.localScale = new Vector3(-1, 1, 1);

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * chaseSpeed);
        }
        else
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            if ((waypoints[currentWaypointIndex].gameObject.transform.position - transform.position).x > 0) transform.localScale = new Vector3(1, 1, 1);
            if ((waypoints[currentWaypointIndex].gameObject.transform.position - transform.position).x < 0) transform.localScale = new Vector3(-1, 1, 1);

            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
