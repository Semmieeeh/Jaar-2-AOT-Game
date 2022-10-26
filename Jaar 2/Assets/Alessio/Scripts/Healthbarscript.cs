using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbarscript : MonoBehaviour
{
    public Image healthbarSprite;
    public Turrets turrets;
    public GameObject deathUI,wall;
    public float maxHeal = 1000f, currentHeal = 1000f;

    public GameObject titan;
    public bool death;
    public bool reset;
    public Slider slider;
    public NavMesh2 navMesh;

    public void Start()
    {
        reset = false;
    }
    public void Update()
    {
        UpdateHealthBar();
        slider.value = currentHeal;
        if(death == true)
        {
            
            
            reset = true;
            
            Death();

        }
        if(currentHeal <= 0)
        {
            death = true;
        }
    }
    public void UpdateHealthBar()
    {
        float fillAmount = (float)currentHeal/(float)maxHeal;
    }
    public void WallDamage(float amount)
    {
        currentHeal -= amount;

        if (currentHeal < 0f)
        {
            currentHeal = 0f;
        }
    }

    public void TitanDamage(float amount)
    {
        currentHeal -= amount;

        if(currentHeal < 0f)
        {
            currentHeal = 0f;
        }

        if(currentHeal == 0f)
        {
            GameObject.Find("Canvas").GetComponent<Currency>().eco.metal += 1f;
        }
    }
    public void Death()
    {
        deathUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Destroy(wall);
    }
}
