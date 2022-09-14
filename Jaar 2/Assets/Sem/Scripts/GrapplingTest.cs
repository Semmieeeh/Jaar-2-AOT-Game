using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;


public class GrapplingTest : MonoBehaviour
{

    public bool canFire = true;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, playerCam, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public bool isGrappling = false;
    public float grappleStrength;
    public float idleStrength;
    public bool stillGrappling;
    public float damperStrength;
    public float minDamperStrength;
    public float maxDamperStrength;
    public GameObject grapplePointChild;
    public GameObject grapplePointParent;
    public float grappleTime;

    public RaycastHit hit;
    public GameObject childOfPoint;
    public bool fallEffect;
    
   


    public void Start()
    {
        
        idleStrength = 4;
        
        minDamperStrength = 1f;
        
    }

    

    void Update()
    {

        
        PlayerMovementAdvanced pma = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();

        if (isGrappling)
        {
            pma.groundDrag = 1f;
        }
        else
        {
            pma.groundDrag = 4f;
        }
        
        Check();

        if (Input.GetMouseButtonDown(0))
        {
            damperStrength = minDamperStrength;
            grappleStrength = idleStrength;
        }
        




        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            
            
            

        }
        
        else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftAlt))
        {
            
            isGrappling = false;
            
            pma.airMultiplier = 2f;
            StopGrapple();
            if (childOfPoint == null)
            {
                return;
            }
            Debug.Log("Destroy");
            grapplePoint = childOfPoint.transform.position;
            Destroy(childOfPoint.gameObject);


        }
        
        if(isGrappling == false)
        {
            return;
        }


       

        if (isGrappling == true && Input.GetMouseButton(1))
        {
            
            joint.maxDistance -= 15f * Time.deltaTime;
            
        }
        else if(isGrappling == true && Input.GetKey(KeyCode.Q))
        {
            
            joint.maxDistance += 3 * Time.deltaTime;
        }

        





        if (childOfPoint == null)
        {
            return;
        }
        childOfPoint.transform.parent = grapplePointParent.transform;
        grapplePoint = childOfPoint.transform.position;
        joint.connectedAnchor = grapplePoint;


        

    }


    public void RopeControls()
    {
        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);


        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;


      
    }

    

    //Called after Update
    

    
    void StartGrapple()
    {
        PlayerMovementAdvanced pm = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();
        
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            isGrappling = true;
            pm.airDash = true;
            pm.airMultiplier = 10f;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            pm.airMultiplier = 15f;
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            stillGrappling = true;
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0f;


            joint.spring = grappleStrength;
            joint.damper = damperStrength;
            joint.massScale = 4.5f;

            grapplePointParent = hit.transform.gameObject;
            
            FindObjectOfType<EquipmentSounds>().Play("GrapplingFire");
            childOfPoint = Instantiate(grapplePointChild, grapplePoint, Quaternion.identity) as GameObject;

            
        }
    }


    
    void StopGrapple()
    {
        
        Destroy(joint);
        FindObjectOfType<EquipmentSounds>().GetComponent<AudioSource>().Stop();
        grapplePointParent = null;

    }


    public void JumpEffect()
    {

    }




    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public void Check()
    {
        if (GameObject.Find("Crosshair") == null)
        {
            return;
        }
        Animator anim = GameObject.Find("Crosshair").GetComponent<Animator>();
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, maxDistance, whatIsGrappleable) || isGrappling == true)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
        anim.SetBool("canFire", canFire);
    }


}