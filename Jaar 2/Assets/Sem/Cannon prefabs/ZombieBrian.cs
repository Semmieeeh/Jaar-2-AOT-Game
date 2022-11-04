using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBrian : MonoBehaviour
{
    public GameObject zombie;
    public NavMeshAgent agent;
    public float speed;
    public GameObject[] target;
    public int currentTarget;
    void Start()
    {
        speed = 69;
        zombie = gameObject;
        agent = zombie.GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = speed;
    }

    
    void Update()
    {
        

        if (Vector3.Distance(agent.transform.position, target[currentTarget].transform.position) < 1f)
        {
            currentTarget += 1;
        }
        if (currentTarget == target.Length)
        {
            currentTarget = 0;
        }

        agent.destination = target[currentTarget].transform.position;
    }




}
