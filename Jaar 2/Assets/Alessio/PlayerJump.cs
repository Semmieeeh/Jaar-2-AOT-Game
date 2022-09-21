using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public bool canDashing;

    public float jumpedFloat;

    public GameObject player;
    public Vector3 jumpPower;

    public void Start()
    {
        canDashing = true;
        player = GameObject.Find("Player");
        jumpPower.y = 9f;
    }
    void Update()
    {
        if(isGrounded && canDashing == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                player.GetComponent<Dash>().mayDash = false;

                GetComponent<Rigidbody>().velocity += jumpPower;
                isGrounded = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            player.GetComponent<Dash>().mayDash = true;

            jumpPower.y = 9f;

            isGrounded = true;
        }
    }
}
