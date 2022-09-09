using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpPower : MonoBehaviour
{
    public Slider slider;
    public GameObject canJump, cannotJump;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementAdvanced pm = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();

        slider.value = pm.jumpForce;
        if (pm.grounded == true)
        {
            
            cannotJump.SetActive(false);
        }
        else if(pm.grounded == false)
        {
            
            cannotJump.SetActive(true);
        }

    }
}
