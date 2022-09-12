using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 movement;

    public float horizontal;
    public float vertical;

    public float speed;
    public float superRunning;

    public bool isSuperRunning;
    public float superRunningFloatFalse;
    public float superRunningFloatTrue;

    public float zerospeed;
    //public AudioSource walking;

    void Start()
    {
        speed = 5f;

        superRunningFloatFalse = 2f;
        superRunningFloatTrue = 10f;

        isSuperRunning = true;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        movement.x = horizontal;
        vertical = Input.GetAxis("Vertical");
        movement.z = vertical;

        transform.Translate(movement * speed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            //walking.volume = 0.25f;
        }
        else
        {
            //walking.volume = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 7f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }

        if(isSuperRunning == true)
        {
            if (Input.GetKey(KeyCode.C))
            {
                speed = 9.5f;

                Invoke("SuperRunningFalse", superRunningFloatFalse);
            }
        }
    }
    private void SuperRunningFalse()
    {
        isSuperRunning = false;

        speed = 5f;

        Invoke("SuperRunningTrue", superRunningFloatTrue);
    }

    private void SuperRunningTrue()
    {
        isSuperRunning = true;
    }
}

