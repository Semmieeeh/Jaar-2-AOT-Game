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
    public Vector3 rotatie;
    public float xRotation = 0f;
    public string targetName;
    public float targetRange;
    public float attackRange;
    public float attackCooldown;
    public float damage;
    public HealthScript playerHealth;
    
    public enum TitanState
    {
        Patrolling,
        Chasing,
        Attacking,

    }
    public TitanState state = TitanState.Patrolling;
    public void Start()
    {
        player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        idle = true;
        damage = 50f;
        currentTargetIndex = 0;
        playerHealth = GameObject.Find("Player").GetComponent<HealthScript>();
        
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


        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < inRange)
        {
            state = TitanState.Chasing;
        }
        else
        {
            state = TitanState.Patrolling;
        }



        

        









    }
}











