using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public bool canDashing;

    public float jumpedFloat;
    private bool maxJump;
    private float maxJumpTimer;
    public GameObject player;
    public Vector3 jumpPower;

    public void Start()
    {
        canDashing = true;
        player = GameObject.Find("Player");
        jumpPower.y = 9f;

        maxJump = true;
        maxJumpTimer = 10f;
    }
    void Update()
    {
        if(isGrounded && canDashing == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                {
                    player.GetComponent<Dash>().mayDash = false;

                    GetComponent<Rigidbody>().velocity += jumpPower;

                    maxJump = false;
                    Invoke("MaxJump", maxJumpTimer);

                    jumpedFloat += 1;

                    if (jumpedFloat == 1 || maxJump == true)
                    {
                        jumpPower.y = 80;        
                    }

                    if (jumpedFloat == 2 || maxJump == true)
                    {
                        jumpedFloat = 2;

                        isGrounded = false;

                        GetComponent<Rigidbody>().drag = 2;
                    }
                }
                
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            GetComponent<Rigidbody>().drag = 1;

            player.GetComponent<Dash>().mayDash = true;

            jumpPower.y = 9f;

            isGrounded = true;
            jumpedFloat = 0;
        }
    }
    private void MaxJump()
    {
        maxJump = true;
    }
}
