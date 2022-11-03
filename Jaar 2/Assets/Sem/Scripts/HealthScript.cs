using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthScript : MonoBehaviour
{
    [Header("User Interface")]
    public Slider healthSlider;
    public GameObject getToCover;
    public GameObject deathUI,winUI;

    [Header("Health")]
    public float health;
    public float maxHealth = 1000;
    public float minHealth = 0;
    public bool healthRegeneration;
    public float healthRegenDelay;
    public float maxDelay = 10f;
    public float minDelay = 0f;
    public bool death;
    public bool tookDamage;
    public bool damageDelay;
    public float titansKilled;
    public GameObject music,music2;
    



    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthRegenDelay = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        healthRegenDelay -= 0.5f*Time.deltaTime;
        if(healthRegenDelay < minDelay)
        {
            healthRegenDelay = minDelay;

        }
        if(healthRegenDelay == minDelay)
        {
            healthRegeneration = true;
            
        }
        else if(healthRegenDelay > minDelay)
        {
            healthRegeneration = false;
        }



        if (healthRegeneration == true)
        {
            health += 100 * Time.deltaTime;
        }
        else
        {
            health += 0 * Time.deltaTime;
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health == minHealth || health < minHealth)
        {
            death = true;
        }

        if(death == true)
        {

            
            deathUI.SetActive(true);
            music.SetActive(false);
            music2.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            death = false;

        }
        if(tookDamage == true)
        {
            
            healthRegenDelay = maxDelay;
            healthRegeneration = false;
            tookDamage = false;
        }


        WaveSystem wave = FindObjectOfType<WaveSystem>();
        if (wave.fiveFinished == true)
        {
            winUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

    }
    

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

}
