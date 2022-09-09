using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthScript : MonoBehaviour
{
    [Header("User Interface")]
    public Slider healthSlider;
    public GameObject getToCover;
    public GameObject deathUI;

    [Header("Health")]
    public float health;
    public float maxHealth = 1000;
    public float minHealth = 0;
    public bool healthRegeneration;
    public float healthRegenDelay;
    public float maxDelay = 3f;
    public float minDelay = 0f;
    public bool death;
    



    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthRegenDelay = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        if(health < 250f)
        {
            getToCover.SetActive(true);
        }
        else if(health > 250)
        {
            getToCover.SetActive(false);
        }

        healthRegenDelay -= 0.5f * Time.deltaTime;
        if(healthRegenDelay < minDelay)
        {
            healthRegenDelay = minDelay;

        }
        if(healthRegenDelay == minDelay)
        {
            healthRegeneration = true;
            
        }
        if(healthRegeneration == true)
        {
            health += 100 * Time.deltaTime;
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

            Time.timeScale = 0f;
            deathUI.SetActive(true);

        }




    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == ("Wall"))
        {
            healthRegenDelay = maxDelay;

        }
    }



}
