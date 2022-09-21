using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoint waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceTreshold = 0.1f;

    public Transform wall;

    [SerializeField] public float distanceToWall;

    private Transform currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.LookAt(currentWaypoint.transform.position);
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

        float dist = Vector3.Distance(wall.position, transform.position);
        distanceToWall = dist;

        if(distanceToWall < 5)
        {
            moveSpeed = 0f;
            transform.LookAt(wall);
            wall.GetComponent<Healthbarscript>().WallDamage(20f * Time.unscaledDeltaTime);
        }
    }
}
