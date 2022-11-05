using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class TutorialFinished : MonoBehaviour
{
    public GameObject textBox;

    public int grabbedEquipment, objectiveStateText, objectiveStateImage;
    public bool equipmentClicked;
    public float range;
    public RaycastHit hit;
    public bool canPickUpGas;
    public bool canPickUpSwords;
    public float skipTime;
    public GameObject fpsCam, player, grapplinghook1, grapplinghook2, swordHolder,grappleHolder;

    public enum TutorialState
    {
        State1,
        State2,
        State3,
        State4,

    }


    public TutorialState state;
    public GameObject jumpBar, dashbar, objectiveTextHolder, objectiveImageHolder, objectiveText;
    public LayerMask Gear;
    public int tutorialState;
    
    //pak een gear tijdens tutorialstart
    public bool pickedSwordsDuringTutorialStart;
    public bool pickedCanistersDuringTutorialStart;

    //pak een gear na tutorialstart
    public bool pickedSwordsAfterTutorialStart;
    public bool pickedCanistersAfterTutorialStart;

    public bool stateTwo;
    //pak iets tijdens state two
    public bool pickedSwordsDuringStateTwo;
    public bool pickedCanistersDuringStateTwo;

    //pak iets na state two
    public bool pickedSwordsAfterStateTwo;
    public bool pickedCanistersAfterStateTwo;

    public bool tutorialStartFinished;
    public bool secondTutorialFinished;
    public bool canSkip;
    public bool skipped;

    public bool swordsPicked;
    public bool gasPicked;

    public bool tutorialFinished;
    
    
    public void Start()
    {
        //swords
        pickedSwordsDuringTutorialStart = false;
        pickedSwordsAfterTutorialStart = false;
        pickedSwordsDuringStateTwo = false;
        pickedSwordsAfterStateTwo = false;
        //canisters
        pickedCanistersAfterStateTwo = false;
        pickedCanistersDuringStateTwo = false;
        pickedCanistersDuringTutorialStart = false;
        pickedCanistersAfterTutorialStart = false;

        tutorialStartFinished = false;
        secondTutorialFinished = false;
        stateTwo = false;


        gasPicked = false;
        swordsPicked = false;


        canSkip = true;
        range = 5f;
        tutorialState = 0;
        equipmentClicked = false;
        canPickUpGas = false;
        canPickUpSwords = false;
        skipped = false;
        StartCoroutine(TutorialStart());

    }

    public void Update()
    {
        objectiveTextHolder.GetComponent<Animator>().SetInteger("ObjectiveState", objectiveStateText);
        objectiveImageHolder.GetComponent<Animator>().SetInteger("ObjectiveState", objectiveStateImage);
        textBox.GetComponent<Animator>().SetInteger("TutorialState", tutorialState);

        
        TutorialSkip();
        
        

        

        if (gasPicked && swordsPicked)
        {
            StopAllCoroutines();
            
            swordsPicked = false;
            secondTutorialFinished=false;
            tutorialFinished=true;

            pickedSwordsAfterStateTwo = false;

            pickedSwordsAfterTutorialStart=false;

            pickedSwordsDuringStateTwo = false;

            pickedSwordsDuringTutorialStart = false;

            pickedCanistersAfterStateTwo = false;

            pickedCanistersDuringStateTwo = false;

            pickedCanistersDuringTutorialStart = false;

            pickedCanistersAfterTutorialStart = false;
            gasPicked = false;
            StartCoroutine(PickedBothAfter());

        }


        

        if(swordsPicked == true && gasPicked == false || swordsPicked == false && gasPicked == true || swordsPicked == false && gasPicked == false)
        {
            TutorialStates();
        }
        CheckForAction();
        TutorialCompleted();
    }



    public void CheckForAction()
    {
        //state one
        if(stateTwo == false)
        {
            if (tutorialStartFinished == true)
            {
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("GearInSight & tutorialfinished = true");
                    if (hit.transform.gameObject.tag == "Swords")
                    {
                        Debug.Log("Picked Swords");
                        Destroy(hit.transform.gameObject);
                        pickedSwordsAfterTutorialStart = true;
                        
                        swordsPicked = true;
                        tutorialState = 0;
                    }
                    else if (hit.transform.gameObject.tag == "Canisters")
                    {
                        Debug.Log("Picked Canisters");
                        Destroy(hit.transform.gameObject);
                        pickedCanistersAfterTutorialStart = true;
                        
                        gasPicked = true;
                        tutorialState = 0;
                    }

                    
                }

            }
            else if (tutorialFinished == false)
            {
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("GearInSight & tutorialfinished = false");
                    if (hit.transform.gameObject.tag == "Swords")
                    {
                        
                        Debug.Log("Picked Swords");
                        Destroy(hit.transform.gameObject);
                        pickedSwordsDuringTutorialStart = true;
                        swordsPicked = true;
                        tutorialState = 0;
                    }
                    else if (hit.transform.gameObject.tag == "Canisters")
                    {
                        Debug.Log("Picked Canisters");
                        Destroy(hit.transform.gameObject);
                        pickedCanistersDuringTutorialStart = true;
                        gasPicked = true;
                        tutorialState = 0;
                    }


                    
                }
            }
        }



        //state two
        if(stateTwo == true)
        {
            if (secondTutorialFinished == false)
            {
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.transform.gameObject.tag == "Swords")
                    {
                        Debug.Log("Picked Swords");
                        Destroy(hit.transform.gameObject);
                        pickedSwordsDuringStateTwo = true;
                        swordsPicked = true;
                        tutorialState = 0;
                    }
                    else if (hit.transform.gameObject.tag == "Canisters")
                    {
                        Debug.Log("Picked Canisters dickhead");
                        Destroy(hit.transform.gameObject);
                        pickedCanistersDuringStateTwo = true;
                        gasPicked = true;
                        tutorialState = 0;
                    }
                }



              
            }
            else if(secondTutorialFinished == true)
            {
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.transform.gameObject.tag == "Swords")
                    {
                        Debug.Log("Picked Canisters");
                        Destroy(hit.transform.gameObject);
                        pickedSwordsAfterStateTwo = true;
                        swordsPicked = true;
                        tutorialState = 0;
                    }
                    else if (hit.transform.gameObject.tag == "Canisters")
                    {
                        Debug.Log("Picked Canisters");
                        Destroy(hit.transform.gameObject);
                        pickedCanistersAfterStateTwo = true;
                        gasPicked = true;
                        tutorialState = 0;
                    }
                }
            }
        }


    }

    //state one
    //state one
    //state one
    //state one
    //state one
    public IEnumerator TutorialStart()
    {
        tutorialState = 1;
        objectiveStateImage = 0;
        objectiveStateText = 0;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Hello, Welcome to the Survey Corps!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "We're so happy to have you here! lets get you introduced to your ODM Equipment.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "If you havent seen it already, Your ODM Equipment is there on the table.";
        objectiveStateImage++;
        objectiveStateText++;
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your gear!";
        tutorialStartFinished = true;
        yield return new WaitForSeconds(7);
        
        
                
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        tutorialState = 0;



    }



    public IEnumerator PickedSwordsDuringTutorial()
    {
        Debug.Log("Picked Swords During Tutorial");
        tutorialState = 0;
        stateTwo = true;
        
        objectiveStateImage = 0;
        objectiveStateText = 0;

        swordHolder.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Huh? you already grabbed your Swords?";
        yield return new WaitForSeconds(7f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Great job!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Now you still need your Gas Canisters! When you have them, I'll enable your ODM Gear";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your Gas Canisters";
        tutorialStartFinished = true;
        objectiveStateImage++;
        objectiveStateText++;
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        secondTutorialFinished = true;
        
        tutorialState = 0;

    }

    public IEnumerator PickedSwordsAfterTutorial()
    {

        objectiveStateImage = 0;
        objectiveStateText = 0;
        tutorialState = 1;
        swordHolder.SetActive(true);
        stateTwo = true;
        
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh so you grabbed the swords first? Great job!";
        yield return new WaitForSeconds(7f);
        textBox.GetComponent<TextMeshProUGUI>().text = "With these swords you can kill the titans.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Now you need to get your Gas Canisters! When you have them, I'll enable your ODM gear!";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your Gas Canisters!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;
        secondTutorialFinished = true;
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }

    public IEnumerator PickedCanistersDuringTutorial()
    {
        Debug.Log("Picked Canisters During Tutorial");
        tutorialState = 0;
        objectiveStateImage = 0;
        objectiveStateText = 0;
        stateTwo = true;
        gasPicked = true;
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Huh? You already grabbed your Gas Canisters?";
        yield return new WaitForSeconds(7f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Great job!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Now you still need your Swords! When you have them, I'll enable your ODM Gear!";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your Swords!";
        tutorialStartFinished = true;
        objectiveStateImage++;
        objectiveStateText++;
        
        secondTutorialFinished = true;
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        tutorialState = 1;
    }

    public IEnumerator PickedCanistersAfterTutorial()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;
        tutorialState = 1;
        stateTwo = true;
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh great! I see you've found the Gas Canisters!";
        yield return new WaitForSeconds(7f);
        textBox.GetComponent<TextMeshProUGUI>().text = "With these Gas Canisters you can maneuver while airborne.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "After you get your Swords, I will enable your ODM gear. So go get your Swords!";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your Swords!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;
        stateTwo = true;
        secondTutorialFinished = true;
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }




    //state two
    //state two
    //state two
    //state two
    //state two
    //state two
    public IEnumerator PickedSwordsDuringStateTwo()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh great, I see you've found the Swords";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Since you now have everything you need, I will enable your ODM Gear!";
        
        yield return new WaitForSeconds(7);

        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;

        textBox.GetComponent<TextMeshProUGUI>().text = " ";
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }

    public IEnumerator PickedSwordsAfterStateTwo()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh great, I see you've found the Swords.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Since you now have everything you need, I will enable your ODM Gear!";
        
        yield return new WaitForSeconds(7);

        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;

        textBox.GetComponent<TextMeshProUGUI>().text = " ";
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }

    public IEnumerator PickedCanistersDuringStateTwo()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;

        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh great, I see you've found the Gas Canisters.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Since you now have everything you need, I will enable your ODM Gear!";
       
        yield return new WaitForSeconds(7);

        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;

        textBox.GetComponent<TextMeshProUGUI>().text = " ";
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }

    public IEnumerator PickedCanistersAfterStateTwo()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;
        
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Ahh great, I see you've found the Gas Canisters.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Since you now have everything you need, I will enable your ODM Gear!";
        
        yield return new WaitForSeconds(7);

        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;

        textBox.GetComponent<TextMeshProUGUI>().text = " ";
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }




    public IEnumerator PickedBothDuring()
    {
        
        objectiveStateImage = 0;
        objectiveStateText = 0;
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "You already have all your gear? Wow! You're a fast learner!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Use your equipment to maneuver around the city, and kill all titans.";
        yield return new WaitForSeconds(7);
        
        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;
        
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }

    public IEnumerator PickedBothAfter()
    {
        objectiveStateImage = 0;
        objectiveStateText = 0;
        tutorialState = 1;
        yield return new WaitForSeconds(1.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "I see you have all your gear. Great job!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Use your equipment to maneuver around the city, and kill all titans.";
        yield return new WaitForSeconds(7);
        
        textBox.GetComponent<TextMeshProUGUI>().text = "Hold 'Spacebar' to charge a Jump, and hold 'Left Alt' to charge a Dash!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press 'Left Click' to start a grapple, Hold 'Right Click' to retract your cable, And Q to extend it.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thats all you need to know! Good luck out there!";
        tutorialState++;
        objectiveStateImage++;
        objectiveStateText++;
        stateTwo = true;
        
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Go check out the city!";
    }






    public void TutorialCompleted()
    {
        if(tutorialFinished == true)
        {
            swordHolder.SetActive(true);
            grappleHolder.SetActive(true);

            grapplinghook1.GetComponent<GrapplingTest>().enabled = true;
            //grapplinghook1.GetComponent<RotateGun>().enabled = true;
            grapplinghook1.GetComponent<GrappleRope>().enabled = true;
            grapplinghook1.GetComponent<LineRenderer>().enabled = true;

            grapplinghook2.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<GrapplingTest>().enabled = true;
           // grapplinghook2.GetComponent<RotateGun>().enabled = true;
            grapplinghook2.GetComponent<GrappleRope>().enabled = true;

            player.GetComponent<TutorialMovement>().enabled = false;
            player.GetComponent<PlayerMovementAdvanced>().enabled = true;



            jumpBar.SetActive(true);
            dashbar.SetActive(true);
            textBox.GetComponent<TextMeshProUGUI>().text = " ";
            equipmentClicked = false;

            canSkip = false;
            StopAllCoroutines();

        }
    }



    


    public void TutorialStates()
    {
        //state one
        if (pickedSwordsDuringTutorialStart == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedSwordsDuringTutorial());
            pickedSwordsDuringTutorialStart = false;
        }

        if (pickedSwordsAfterTutorialStart == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedSwordsAfterTutorial());
            pickedSwordsAfterTutorialStart = false;
        }

        if (pickedCanistersDuringTutorialStart == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedCanistersDuringTutorial());
            pickedCanistersDuringTutorialStart = false;
        }

        if (pickedCanistersAfterTutorialStart == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedCanistersAfterTutorial());
            pickedCanistersAfterTutorialStart = false;
        }

        






        //state two
        if (pickedSwordsDuringStateTwo == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedSwordsDuringStateTwo());
            pickedSwordsDuringStateTwo = false;
        }

        if (pickedSwordsAfterStateTwo == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedSwordsAfterStateTwo());
            pickedSwordsAfterStateTwo = false;
        }

        if (pickedCanistersDuringStateTwo == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedCanistersDuringStateTwo());
            pickedCanistersDuringStateTwo = false;
        }

        if (pickedCanistersAfterStateTwo == true)
        {
            StopAllCoroutines();
            StartCoroutine(PickedCanistersAfterStateTwo());
            pickedCanistersAfterStateTwo = false;
        }
    }
    public void TutorialSkip()
    {
        if (skipped == false)
        {
            if (canSkip == true)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    skipTime += 1 * Time.deltaTime;
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    skipTime = 0f;
                }
            }
        }

        if (skipTime > 3)
        {
            skipTime = 3;
        }

        if (skipTime == 3)
        {
            swordHolder.SetActive(true);
            grappleHolder.SetActive(true);
            
            grapplinghook1.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook1.GetComponent<RotateGun>().enabled = true;
            grapplinghook1.GetComponent<GrappleRope>().enabled = true;
            grapplinghook1.GetComponent<LineRenderer>().enabled = true;

            grapplinghook2.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook2.GetComponent<RotateGun>().enabled = true;
            grapplinghook2.GetComponent<GrappleRope>().enabled = true;

            player.GetComponent<TutorialMovement>().enabled = false;
            player.GetComponent<PlayerMovementAdvanced>().enabled = true;
            
            

            jumpBar.SetActive(true);
            dashbar.SetActive(true);
            textBox.GetComponent<TextMeshProUGUI>().text = " ";
            equipmentClicked = false;
            
            canSkip = false;
            StopAllCoroutines();

        }

    }

}
