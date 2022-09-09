using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    
    public Transform gunTip, cam, player;
    public bool aboutToGrapple;
    public bool isGrappling;
    private RaycastHit hit;
    public float grappleRange;
    public Vector3 grapplePoint;
    private SpringJoint joint;
    public float grappleJump;
    public float maxDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        grappleRange = 50f;
        maxDistance = 60f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementAdvanced pma = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();
        //punt van grapple definieren met raycast
        //force applyen op het object naar de richting van het gedefinieerde punt
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, grappleRange))
            {
                aboutToGrapple = true;
                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
                if (aboutToGrapple == true)
                {
                    
                    grapplePoint = hit.point;
                    joint = player.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grapplePoint;

                    


                    joint.maxDistance = distanceFromPoint * 0.8f;
                    joint.minDistance = distanceFromPoint * 0.25f;


                    joint.spring = 4.5f;
                    joint.damper = 7f;
                    joint.massScale = 4.5f;

                    Debug.Log("holding");
                    

                }             

            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            if (Physics.Raycast(transform.position, transform.forward, out hit, grappleRange))
            {
                aboutToGrapple = true;
                
                if (aboutToGrapple == true)
                {
                    grapplePoint = hit.point;
                    joint = player.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grapplePoint;




                    joint.maxDistance = distanceFromPoint * 0f;
                    joint.minDistance = distanceFromPoint * 0f;


                    joint.spring = 4.5f;
                    joint.damper = 7f;
                    joint.massScale = 4.5f;

                    Debug.Log("grappling");
                    
                }
               
            }
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            aboutToGrapple = false;
            isGrappling = false;
            Destroy(joint);

        }

        if (grappleJump > 0)
        {
            grappleJump -= 1 * Time.deltaTime;
        }
    }

    
}
