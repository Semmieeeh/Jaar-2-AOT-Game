using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Economy eco;
    public bool playerKilled;
    public TagAttribute def;
    public GameObject gettingShotBy;


    // Start is called before the first frame update
    void Start()
    {
        eco = GameObject.Find("Player").GetComponent<Economy>();
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
        if(death == true && playerKilled == true)
        {
            StartCoroutine(DeathEffect());
            
            death = false;
            
        }
        else if(death == true && playerKilled == false)
        {
            eco.metal += eco.payment;
            StartCoroutine(DeathEffectNotCausedByPlayer());
            death = false;
            
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
        eco.metal += eco.payment;
        rb.AddForce(fpsCam.transform.forward * knockback, ForceMode.Impulse);
        if (gettingShotBy != null)
        {
            Turrets turret = gettingShotBy.GetComponent<Turrets>();
            turret.titan = null;
        }
        gameObject.tag = "Dead";
        yield return new WaitForSeconds(10);
        
        Destroy(gameObject);
    }

    public IEnumerator DeathEffectNotCausedByPlayer()
    {
        navMesh.enabled = false;
        navMeshAgent.enabled = false;
        rb.freezeRotation = false;
        eco.metal += eco.payment;
        if(gettingShotBy != null)
        {
            Turrets turret = gettingShotBy.GetComponent<Turrets>();
            turret.titan = null;
        }
        gameObject.tag = "Dead";
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
