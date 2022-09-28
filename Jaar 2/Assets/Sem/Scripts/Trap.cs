using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapHealth;
    public NavMesh2 navMesh;
    public LayerMask enemy;
    public bool death;
    
    public void Start()
    {
        
    }


    public void Update()
    {
        if(trapHealth <= 0)
        {
            navMesh.state = NavMesh2.TitanState.Patrolling;
            navMesh.trapped = false;
            Destroy(gameObject);
        }

        if(death == true)
        {
            Destroy(gameObject);
        }




    }

    public void TrapDamage(float amount)
    {
        trapHealth -= amount;
        if(trapHealth <= 0)
        {
            death = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Titan")
        {

            
            navMesh = other.gameObject.GetComponent<NavMesh2>();
            
            navMesh.trap = gameObject;
            navMesh.trapped = true;
            navMesh.state = NavMesh2.TitanState.Trapped;
            
        }

    }
}
