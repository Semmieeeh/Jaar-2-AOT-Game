using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbarscript : MonoBehaviour
{
    public Image healthbarSprite;
    public float maxHeal = 1000f, currentHeal = 1000f;

    public GameObject titan;
    public void Update()
    {
        UpdateHealthBar();

        if(currentHeal == 0)
        {
            Destroy(titan);
        }
    }
    public void UpdateHealthBar()
    {
        float fillAmount = (float)currentHeal/(float)maxHeal;
        healthbarSprite.fillAmount = fillAmount;
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
    }
}
