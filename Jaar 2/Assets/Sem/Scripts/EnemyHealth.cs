using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    public float minHealth;
    public bool death;
    // Start is called before the first frame update
    void Start()
    {
        minHealth = 0f;
        maxHealth = 100f;
        enemyHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(death == true)
        {
            Destroy(gameObject); 
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
}
