using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static NavMesh2;

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
    public GameObject titanHolder,wall;
    public bool gameStart;
    public bool closeToWall;
    public bool wallBroken;
    public bool stateTwo;
    public float distanceToNextPoint;
    public Animator animator;
    public int titanState;
    public bool canPatrol;
    public enum TitanState
    {
        Patrolling,
        Chasing,
        Attacking,
        Trapped,
        AttackingWall,
    }
    public TitanState state = TitanState.Patrolling;
    public void Start()
    {
        animator = GetComponent<Animator>();
        titanState = 1;
        player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        idle = true;
        trapped = false;
        damage = 100f;
        currentTargetIndex = 0;
        targetRange = 5;
        playerHealth = GameObject.Find("Player").GetComponent<HealthScript>();
        titanHolder = gameObject.transform.GetChild(1).gameObject;
        target[0] = GameObject.Find("Waypoint1").transform;
        target[1] = GameObject.Find("Waypoint2").transform;
        target[2] = GameObject.Find("Waypoint3").transform;
        target[3] = GameObject.Find("Waypoint4").transform;
        target[4] = GameObject.Find("Waypoint5").transform;
        target[5] = GameObject.Find("Waypoint6").transform;
        target[6] = GameObject.Find("Waypoint7").transform;
        target[7] = GameObject.Find("Waypoint8").transform;
        target[8] = GameObject.Find("Waypoint9").transform;
        target[9] = GameObject.Find("Waypoint10").transform;
        target[10] = GameObject.Find("Waypoint11").transform;
        target[11] = GameObject.Find("Waypoint12").transform;
        target[12] = GameObject.Find("Waypoint13").transform;
        target[13] = GameObject.Find("Waypoint14").transform;
        target[14] = GameObject.Find("Waypoint15").transform;
        target[15] = GameObject.Find("Waypoint16").transform;
        target[16] = GameObject.Find("Waypoint17").transform;
        target[17] = GameObject.Find("Waypoint18").transform;
        target[18] = GameObject.Find("Waypoint19").transform;
        target[19] = GameObject.Find("Waypoint20").transform;
        target[20] = GameObject.Find("Waypoint21").transform;
        target[21] = GameObject.Find("Waypoint22").transform;
        target[22] = GameObject.Find("Waypoint23").transform;
        wall = GameObject.Find("Shighanshina Gate");
        canPatrol = true;
        attackRange = 10;
    }

    public void Update()
    {
        animator.SetInteger("TitanState", titanState);
        distanceToNextPoint = Vector3.Distance(titanHolder.transform.position, target[currentTargetIndex].transform.position);

        if(gameStart == false)
        {
            targetName = target[currentTargetIndex].name;
        }
        if(titanHolder != null)
        {
            distance = Vector3.Distance(player.transform.position, titanHolder.transform.position);
        }
        dir = player.transform.position - transform.position;
        angle = Vector3.Angle(dir, transform.forward);



        Vector3 pos = target[currentTargetIndex].position;
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);


        EnemyHealth enemyhealth = gameObject.GetComponent<EnemyHealth>();
        if(titanHolder != null&& canPatrol == true)
        {
            switch (state)
            {
                case TitanState.Patrolling:
                    if(enemyhealth.enemyHealth > 0)
                    {
                        titanState = 0;
                    }
                    navMeshAgent.destination = target[currentTargetIndex].position;
                    

                    //Vector3 rot = Vector3.RotateTowards(transform.forward, target[currentTargetIndex].position - transform.position, 10, 0.0f);
                    //transform.rotation = Quaternion.LookRotation(rot);


                    //Debug.Log(target[currentTargetIndex].name+distanceToNextPoint);
                    if (Vector3.Distance(titanHolder.transform.position, target[currentTargetIndex].position) < targetRange)
                    {
                        if (currentTargetIndex < 22)
                        {
                            currentTargetIndex++;
                        }
                    }
                    if (currentTargetIndex >= target.Length)
                    {
                        currentTargetIndex = 0;
                    }


                    break;
                case TitanState.Chasing:
                    navMeshAgent.destination = player.transform.position;
                    //transform.LookAt(playerPos);

                    if (Vector3.Distance(titanHolder.transform.position, player.transform.position) < attackRange && attackCooldown < 0.1)
                    {
                        playerHealth.TakeDamage(damage);
                        playerHealth.tookDamage = true;

                        playerHealth.healthRegeneration = false;
                        attackCooldown += 3;
                    }

                    break;
                case TitanState.Trapped:

                    navMeshAgent.destination = trap.transform.position;
                    if (Vector3.Distance(titanHolder.transform.position, trap.transform.position) < attackRange && attackCooldown < 0.1)
                    {

                        trap.GetComponent<Trap>().TrapDamage(damage);
                        attackCooldown += 3;

                    }



                    break;
                case TitanState.AttackingWall:
                    titanState = 1;
                    titanState = 0;
                    if(attackCooldown < 0.1)
                    {
                        wall.transform.GetChild(0).GetComponent<Healthbarscript>().WallDamage(damage);

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


            if (Vector3.Distance(titanHolder.transform.position, player.transform.position) < inRange && trapped == false)
            {
                //state = TitanState.Chasing;
            }
            else if (trapped == false && Vector3.Distance(gameObject.transform.position, player.transform.position) >inRange && closeToWall == false && enemyhealth.death == false)
            {
                state = TitanState.Patrolling;
            }
            else if (trapped == true)
            {
                state = TitanState.Trapped;
            }
            if(wall != null)
            {
                if (Vector3.Distance(titanHolder.transform.position, wall.transform.position) < 20 && closeToWall == true && wallBroken == false)
                {
                    state = TitanState.AttackingWall;
                }
            }
            
            




            if(wall != null)
            {
                if (Vector3.Distance(titanHolder.transform.position, wall.transform.position) < 20)
                {
                    closeToWall = true;
                }
                else if (Vector3.Distance(titanHolder.transform.position, wall.transform.position) > 20)
                {
                    closeToWall = false;
                }
            }
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
