using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    public bool mayDash;
    private float mayDashTrueTimer;
    private float canDashTrueTimer;

    public float wPressed;
    public float wPressedTimer;

    private void Start()
    {
        mayDash = true;

        mayDashTrueTimer = 3f;
        canDashTrueTimer = 0.3f;

        dashSpeed = 50f;

        wPressedTimer = 2f;
    }
    private void Update()
    {
        if (mayDash == true)
        {
            if(GameObject.Find("Player").GetComponent<PlayerJump>().isGrounded) 
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GetComponent<Rigidbody>().drag = 4;

                    mayDash = false;

                    GameObject.Find("Player").GetComponent<PlayerJump>().canDashing = false;

                    GetComponent<Rigidbody>().velocity += transform.forward * dashSpeed;

                    Invoke("MayDashTrue", mayDashTrueTimer);
                    Invoke("CanDash", canDashTrueTimer);
                }
            }   
        }    
    }
    private void MayDashTrue()
    {
        mayDash = true;
    }
    private void CanDash()
    {
        GameObject.Find("Player").GetComponent<PlayerJump>().canDashing = true;

        GetComponent<Rigidbody>().drag = 1;
    }
}

