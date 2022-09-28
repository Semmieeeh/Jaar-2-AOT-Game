using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    public float minHealth;
    public bool death;
    public GameObject fpsCam;
    public Rigidbody rb;
    public NavMesh2 navMesh;
    public NavMeshAgent navMeshAgent;
    public float knockback;
    public MeleeScript melee;
    public bool diedByPlayer;


    // Start is called before the first frame update
    void Start()
    {
        diedByPlayer = false;
        melee = FindObjectOfType<MeleeScript>().GetComponent<MeleeScript>();
        minHealth = 0f;
        maxHealth = 100f;
        enemyHealth = maxHealth;
        fpsCam = GameObject.Find("Main Camera");
        rb = gameObject.GetComponent<Rigidbody>();
        navMesh = gameObject.GetComponent<NavMesh2>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(death == true && diedByPlayer == true)
        {
            StartCoroutine(DeathEffect());
            death = false;
        }
        else if(death == true &&diedByPlayer == false)
        {
            StartCoroutine(TurretDeath());
        }
    }



    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if(enemyHealth < 1)
        {
            death = true;
        }
    }

    public IEnumerator DeathEffect()
    {
        navMesh.enabled = false;
        navMeshAgent.enabled = false;
        rb.freezeRotation = false;
        //currency+
        rb.AddForce(fpsCam.transform.forward * knockback, ForceMode.Impulse);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    public IEnumerator TurretDeath()
    {
        Vector3 kb = new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3));
        rb.AddForce(kb);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
