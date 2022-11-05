using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashPower : MonoBehaviour
{
    public Slider slider;
    public GameObject cannotDashUI, canDash, cannotDash;
    


    public void Start()
    {
        slider = GameObject.Find("DashBar").GetComponent<Slider>();
        
    }
    public void Update()
    {
        PlayerMovementAdvanced pm = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();
        
        if (pm.airDash == true)
        {
            Slider();
            
        }
        if(pm.airDash == true)
        {
            cannotDash.SetActive(false);
            canDash.SetActive(true);
            
        }
        else if(pm.airDash == false)
        {
            canDash.SetActive(false);
            cannotDash.SetActive(true);
        }
    }


    public void Slider()
    {
        PlayerMovementAdvanced pm = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();
        slider.value = pm.dashForce;
    }
}
