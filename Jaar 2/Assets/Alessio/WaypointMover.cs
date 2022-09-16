using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoint waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceTreshold = 0.1f;

    private Transform currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, currentWaypoint.position) < distanceTreshold)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}
