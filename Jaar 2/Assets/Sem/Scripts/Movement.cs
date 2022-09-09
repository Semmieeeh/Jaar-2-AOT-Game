using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 movement;
    public float horizontal;
    public float vertical;
    public float walkSpeed;
    public float sprintSpeed;
    
    public bool canSprint;
    public bool isWalking;
    
    public Transform cam;
    
    
    public bool isGrounded;
    public Vector3 jumpPower;
    public bool isSprinting;
    

    //rotation
    
    
    

    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody3D>();
        
        walkSpeed = 5f;
        sprintSpeed = 10f;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        movement.x = horizontal;
        vertical = Input.GetAxis("Vertical");
        movement.z = vertical;
        transform.Translate(movement * walkSpeed * Time.deltaTime);

        
        

        if(canSprint == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isWalking = false;
                walkSpeed = 12f;
                isSprinting = true;
            }
            else
            {
                walkSpeed = 5f;
            }
        }
      
    }


    void Awake()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                isGrounded = false;
                GetComponent<Rigidbody>().velocity += jumpPower;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }

    }

}

