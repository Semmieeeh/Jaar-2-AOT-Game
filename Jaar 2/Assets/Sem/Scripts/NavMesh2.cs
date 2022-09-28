using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMesh2 : MonoBehaviour
{
    public Vector3 dir;
    public GameObject player;
    public RaycastHit hit;
    public float angle;
    public NavMeshAgent navMeshAgent;
    public Vector3 walkDir;
    public float distance;
    public Transform[] target;
    public bool idle;
    public int currentTargetIndex;
    public float inRange;
    public LayerMask playerMask;
    public LayerMask trapMask;
    public Vector3 rotatie;
    public float xRotation = 0f;
    public string targetName;
    public float targetRange;
    public float attackRange;
    public float attackCooldown;
    public float damage;
    public GameObject trap;
    public HealthScript playerHealth;
    public bool trapped;
    
    public enum TitanState
    {
        Patrolling,
        Chasing,
        Attacking,
        Trapped,
    }
    public TitanState state = TitanState.Patrolling;
    public void Start()
    {
        player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        idle = true;
        trapped = false;
        damage = 50f;
        currentTargetIndex = 0;
        playerHealth = GameObject.Find("Player").GetComponent<HealthScript>();

        target[0] = GameObject.Find("Waypoint").transform;  
        target[1] = GameObject.Find("Waypoint1").transform;
        target[2] = GameObject.Find("Waypoint2").transform;
        target[3] = GameObject.Find("Waypoint3").transform;







    }

    public void Update()
    {
        targetName = target[currentTargetIndex].name;
        distance = Vector3.Distance(player.transform.position, transform.position);
        dir = player.transform.position - transform.position;
        angle = Vector3.Angle(dir, transform.forward);



        switch (state)
        {
            case TitanState.Patrolling:
                navMeshAgent.destination = target[currentTargetIndex].position;
                transform.LookAt(target[currentTargetIndex].position);
                if (Vector3.Distance(gameObject.transform.position, target[currentTargetIndex].position) < targetRange)
                {
                    currentTargetIndex++;
                }
                if (currentTargetIndex >= target.Length)
                {
                    currentTargetIndex = 0;
                }


                break;
            case TitanState.Chasing:
                navMeshAgent.destination = player.transform.position;
                transform.LookAt(player.transform.position);

                if(Vector3.Distance(gameObject.transform.position, player.transform.position) < attackRange && attackCooldown < 0.1)
                {
                    playerHealth.TakeDamage(damage);
                    
                    playerHealth.tookDamage = true;
                    playerHealth.healthRegeneration = false;
                    attackCooldown += 3;
                }

                break;
            case TitanState.Trapped:

                navMeshAgent.destination = trap.transform.position;
                if (Vector3.Distance(gameObject.transform.position, trap.transform.position) < attackRange && attackCooldown < 0.1)
                {

                    trap.GetComponent<Trap>().TrapDamage(damage);
                    attackCooldown += 3;

                }



                break;

        }
        attackCooldown -= 1 * Time.deltaTime;
        if (attackCooldown > 3)
        {
            attackCooldown = 3;
        }
        else if (attackCooldown < 0)
        {
            attackCooldown = 0;
        }


        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < inRange &&trapped == false)
        {
            state = TitanState.Chasing;
        }
        else if(trapped == false)
        {
            state = TitanState.Patrolling;
        }
        else if(trapped == true)
        {
            state = TitanState.Trapped;
        }

    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            
            trap = collision.gameObject;
            trapped = true;
            state = TitanState.Trapped;
        }
    }



}











