using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using UnityEngine.UI;

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
    
    public Economy eco;
    public bool playerKilled;
    public TagAttribute def;
    public GameObject gettingShotBy;
    HealthScript playerHp;
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.transform.GetChild(0).GetComponent<Slider>();

        
        
        playerHp = GameObject.Find("Player").GetComponent<HealthScript>();
        eco = GameObject.Find("Player").GetComponent<Economy>();
        
        
        minHealth = 0f;
        maxHealth = 200;
        slider.maxValue = maxHealth;
        enemyHealth = maxHealth;
        fpsCam = GameObject.Find("Main Camera");
        rb = gameObject.GetComponent<Rigidbody>();
        navMesh = gameObject.GetComponent<NavMesh2>();
        
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemyHealth;
        if (death == true && playerKilled == true)
        {
            StartCoroutine(DeathEffect());
            death = false;
            
        }
        else if(death == true && playerKilled == false)
        {
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
        rb.AddForce(fpsCam.transform.forward * knockback*2f, ForceMode.Impulse);
        if (gettingShotBy != null)
        {
            Turrets turret = gettingShotBy.GetComponent<Turrets>();
            turret.titan = null;
            GrapplingTest gt1 = GameObject.Find("GrapplingGun").GetComponent<GrapplingTest>();
            GrapplingTest gt2 = GameObject.Find("GrapplingGun2").GetComponent<GrapplingTest>();

            gt1.StopGrapple();
            gt2.StopGrapple();
        }
        gameObject.tag = "Dead";
        playerHp.titansKilled += 1;
        yield return new WaitForSeconds(10);
        
        Destroy(gameObject);
    }

    public IEnumerator DeathEffectNotCausedByPlayer()
    {
        navMesh.enabled = false;
        navMeshAgent.enabled = false;
        rb.freezeRotation = false;
        rb.AddForce(gettingShotBy.transform.forward * knockback*2f, ForceMode.Impulse);
        eco.metal += eco.payment;
        if (gettingShotBy != null)
        {
            Turrets turret = gettingShotBy.GetComponent<Turrets>();
            turret.titan = null;
            GrapplingTest gt1 = GameObject.Find("GrapplingGun").GetComponent<GrapplingTest>();
            GrapplingTest gt2 = GameObject.Find("GrapplingGun2").GetComponent<GrapplingTest>();

            gt1.StopGrapple();
            gt2.StopGrapple();
        }
        gameObject.tag = "Dead";
        playerHp.titansKilled += 1;
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
