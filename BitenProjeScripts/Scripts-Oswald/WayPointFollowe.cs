using UnityEngine;

public class WayPointFollowe : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;
    public bool isActive = true;
    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {

        if (isActive)
        {
            if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
            {
                currentWayPointIndex++;
                if (currentWayPointIndex >= waypoints.Length)
                {
                    currentWayPointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}