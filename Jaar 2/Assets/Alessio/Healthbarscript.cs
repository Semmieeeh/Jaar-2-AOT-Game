using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbarscript : MonoBehaviour
{
    public Image healthbarSprite;

    public float maxHeal = 5f, currentHeal = 5f;
    public void Update()
    {
        UpdateHealthBar();
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentHeal -= 1f;
        }
    }
    public void UpdateHealthBar()
    {
        float fillAmount = (float)currentHeal/(float)maxHeal;
        healthbarSprite.fillAmount = fillAmount;
    }
    public void DamageTitan(float amount)
    {
        amount = currentHeal;
    }
}
