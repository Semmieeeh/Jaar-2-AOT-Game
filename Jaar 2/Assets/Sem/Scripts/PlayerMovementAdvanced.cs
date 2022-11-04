using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementAdvanced : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public bool groundHitParticle;
    

    public float groundDrag;
    public bool smallGroundHit;
    [Header("Jumping")]
    public float jumpForce;
    public float maxJumpForce;
    public float minJumpForce = 8;
    
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public GameObject fpsCam;
    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 moveDirection;
    
    public Rigidbody rb;

    [Header("Dash")]
    public float dashForce;
    public float dashForceMin = 3;
    public float dashForceMax = 15;
    public float dashCoolDown = 3f;
    public float dashCoolDownMin;
    public float dashCoolDownMax;
    public bool airDash;
    public bool canPlay;
    public GrapplingTest grapplingTest;
    public GameObject cannotJump;
    public MovementState state;
    public ParticleSystem groundparticle;
    public bool canPlayGroundSound;
    public bool walking;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        grapplingTest = GameObject.Find("GrapplingGun").GetComponent<GrapplingTest>();
        readyToJump = true;
        maxJumpForce = 20;
        dashForceMax = 15;
        dashForceMin = 3;
        startYScale = transform.localScale.y;
        dashCoolDownMax = 3f;
        canPlay = false;
        FindObjectOfType<AudioManager>().audioSources[12].Play(12);
        FindObjectOfType<AudioManager>().audioSources[12].volume = 0;
    }

    private void Update()
    {
        if (walking == true && grounded)
        {
            FindObjectOfType<AudioManager>().audioSources[12].volume = 1;

        }
        else if (walking == false ||grounded)
        {
            FindObjectOfType<AudioManager>().audioSources[12].volume = 0;
        }

        if(grounded == true && horizontalInput > 0 ||horizontalInput < 0 || verticalInput < 0 || verticalInput > 0)
        {
            walking = true;
        }
        else if(horizontalInput == 0 && verticalInput== 0 || !grounded)
        {
            walking = false;
        }
        if (!grounded)
        {
            FindObjectOfType<AudioManager>().audioSources[12].volume = 0;
        }



        
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput(); 
        transform.localScale = transform.localScale;
        SpeedControl();
        StateHandler();


        if(grounded == false)
        {
            canPlay = true;

            if (canPlay == true && rb.velocity.x >5 && rb.velocity.y < 5 || rb.velocity.y >5 && rb.velocity.y < 5 || rb.velocity.z > 5 && rb.velocity.z < 5)
            {
                FindObjectOfType<AudioManager>().PlayAudio(3, 0.8f, 1.2f);
            }
            

        }
        else if(grounded == true)
        {
            canPlay = false;
            FindObjectOfType<AudioManager>().StopAudio(3);
            
        }


        



        dashCoolDown -= 1 * Time.deltaTime;
        if(dashCoolDown < dashCoolDownMin)
        {
            dashCoolDown = dashCoolDownMin;
        }

        if(dashCoolDown > 3)
        {
            dashCoolDown = 3;
        }

        if(dashCoolDown == dashCoolDownMin)
        {
            
            if (Input.GetKeyUp(KeyCode.LeftAlt) && grounded || grounded == false && airDash == true && Input.GetKeyUp(KeyCode.LeftAlt))
            {

                rb.velocity = new Vector3(0f, 0f, 0f);
                
                Dash();
                dashForce = dashForceMin;
                dashCoolDown += 3f;
                airDash = false;
            }
        }
        else if(grounded == false && airDash == true && Input.GetKeyUp(KeyCode.LeftAlt))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);

            Dash();
            dashForce = dashForceMin;
            
            airDash = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            dashForce = dashForceMin;
            
        }

        
        if(airDash == false)
        {
            if(grounded == true)
            {
                airDash = true;
            }
        }

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        
        //dashforce
        if(dashForce > dashForceMax)
        {
            dashForce = dashForceMax;
        }

        if(dashForce < dashForceMin)
        {
            dashForce = dashForceMin;
        }

        if (Input.GetKey(KeyCode.LeftAlt) && grounded)
        {
            dashForce += 15f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftAlt) && grounded == false)
        {
            dashForce += 30f * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpForce = minJumpForce;
        }



        //JumpForce
        if (jumpForce > 20)
        {
            jumpForce = maxJumpForce;
        }
        if (jumpForce < 8)
        {
            jumpForce = minJumpForce;
        }

        if(rb.velocity.y > 20)
        {
            groundHitParticle = true;
        }
        
        
        if(groundHitParticle == true &&grounded == true)
        {
            groundHitParticle = false;
            groundparticle.Play();
            FindObjectOfType<AudioManager>().PlayAudio(0, 0.9f, 1.1f);

        }
        


    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        

        if (grounded)
        {
            cannotJump.SetActive(false);
        }

        // when to jump
        if (Input.GetKeyUp(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

            



            jumpForce = 8;
        }

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            jumpForce +=15 * Time.deltaTime;
        }


        
    }

    
    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(KeyCode.C))
        {
            
        }

        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey) && grapplingTest.isGrappling == false)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air;
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed *airMultiplier , ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed && grapplingTest.isGrappling == true &&Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkSpeed * 12f);
            }
            else if (flatVel.magnitude > moveSpeed && grapplingTest.isGrappling == true)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkSpeed * 7.5f);
            }
            else if (flatVel.magnitude > moveSpeed && grapplingTest.isGrappling == false)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkSpeed * 7.5f);
            }




        }


        
    }
    


    private void Dash()
    {

        Vector3 vel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        rb.AddForce(fpsCam.transform.forward * dashForce * 7.5f + vel, ForceMode.Impulse);
        rb.AddForce(fpsCam.transform.up * dashForce + vel, ForceMode.Impulse);
        FindObjectOfType<AudioManager>().PlayAudio(9, 0.8f, 1.2f);

    }

    private void Jump()
    {
        exitingSlope = true;
        
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        if (jumpForce >= 15)
        {
            FindObjectOfType<AudioManager>().PlayAudio(11, 0.8f, 1.2f);
        }
        else if (jumpForce < 15)
        {
            FindObjectOfType<AudioManager>().PlayAudio(10, 0.8f, 1.2f);
        }
        cannotJump.SetActive(true);

    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    
	

    
    




    
}





