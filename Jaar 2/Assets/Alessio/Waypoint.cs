using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointsize = 1f;
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(t.position, waypointsize);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWayPoint(Transform CurrentWaypoint)
    {
        if(CurrentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if(CurrentWaypoint.GetSiblingIndex() < transform.childCount -1)
        {
            return transform.GetChild(CurrentWaypoint.GetSiblingIndex() + 1);
        }

        else
        {
            return transform.GetChild(0);
        }
    }
}
