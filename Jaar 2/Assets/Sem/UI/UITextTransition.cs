using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class UITextTransition : MonoBehaviour
{
    public GameObject textBox;
    
    public int grabbedEquipment, objectiveStateText, objectiveStateImage;
    public bool equipmentClicked;
    public float range;
    public RaycastHit hit;
    public bool canPickUpGas;
    public bool canPickUpSwords;
    public float skipTime;
    public GameObject fpsCam, player, grapplinghook1, grapplinghook2, grappleHolder;

    
    public GameObject jumpBar, dashbar, objectiveTextHolder, objectiveImageHolder, objectiveText;
    public LayerMask Gear;
    public int tutorialState;
    public bool stateOneBegun;
    public bool stateTwoBegun;
    public bool stateThreeBegun;
    public bool stateOneFinished;
    public bool stateTwoFinished;
    public bool stateThreeFinished;
    public bool canSkip;
    public bool skipped;
    public GameObject waveSystem;

    public void Start()
    {
        waveSystem = GameObject.Find("TitanSpawner");
        canSkip = true;
        stateOneFinished = false;
        stateTwoFinished = false;
        stateThreeFinished = false;
        range = 2f;
        tutorialState = 0;
        equipmentClicked = false;
        canPickUpGas = false;
        canPickUpSwords = false;
        skipped = false;
        StartCoroutine(TheSequence());
        
    }

    public void Update()
    {
        objectiveTextHolder.GetComponent<Animator>().SetInteger("ObjectiveState", objectiveStateText);
        objectiveImageHolder.GetComponent<Animator>().SetInteger("ObjectiveState", objectiveStateImage);
        textBox.GetComponent<Animator>().SetInteger("TutorialState", tutorialState);

        CheckForEquipment();

        if (grabbedEquipment == 1 && equipmentClicked == true )
        {
            grappleHolder.SetActive(true);
            StopAllCoroutines();
            equipmentClicked = false;
            StartCoroutine(TheSecondSequence());
            
            
        }
        if (grabbedEquipment == 2 && equipmentClicked == true && canPickUpGas == true)
        {
            player.GetComponent<TutorialMovement>().enabled = false;
            player.GetComponent<PlayerMovementAdvanced>().enabled = true;
            grapplinghook1.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook1.GetComponent<RotateGun>().enabled = true;
            grapplinghook1.GetComponent<GrappleRope>().enabled = true;
            grapplinghook1.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook2.GetComponent<RotateGun>().enabled = true;
            grapplinghook2.GetComponent<GrappleRope>().enabled = true;
            jumpBar.SetActive(true);
            dashbar.SetActive(true);
            StopAllCoroutines();
            equipmentClicked = false;
            StartCoroutine(TheThirdSequence());
            waveSystem.GetComponent<WaveSystem>().one = true;

        }
        TutorialSkip();
    }
    


    public void CheckForEquipment()
    {
        
        if(grabbedEquipment == 0 && Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E) && canPickUpSwords == true)
        {
            GameObject toDestroy = hit.transform.gameObject;
            Destroy(toDestroy);
            grabbedEquipment +=1;
            equipmentClicked=true;
            tutorialState++;
        }
        else if(grabbedEquipment == 1 && Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Gear) && Input.GetKeyDown(KeyCode.E) && canPickUpGas == true)
        {
            GameObject toDestroy = hit.transform.gameObject;
            Destroy(toDestroy);
            grabbedEquipment += 1;
            equipmentClicked = true;
            tutorialState++;
        }
    }
    public IEnumerator TheSequence()
    {
        stateOneBegun = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Hello, Welcome to the Survey Corps!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "We're so happy to have you here! lets get you introduced to your ODM Equipment.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "If you havent seen it already, Your ODM Equipment is there on the table.";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your gear!";
        objectiveStateImage++;
        objectiveStateText++;
        canPickUpSwords = true;
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        

    }
    public IEnumerator TheSecondSequence()
    {
        stateTwoBegun = true;
        objectiveStateImage--;
        objectiveStateText--;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Great! Now test out your gear! You can press 'Left Click' to fire your grappling hooks.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Oh, Wait, It's not working is it? Thats because you need your Gas Canisters!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "You need to get your Gas Canisters. They are also on the table.";
        yield return new WaitForSeconds(7);
        objectiveText.GetComponent<TextMeshProUGUI>().text = "Objective: Get your Gas Canisters!";
        canPickUpGas = true;
        objectiveStateText++;
        objectiveStateImage++;
        textBox.GetComponent<TextMeshProUGUI>().text = "";

    }

    public IEnumerator TheThirdSequence()
    {
        stateThreeBegun = true;
        objectiveStateText--;
        objectiveStateImage--;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Nice! now try it again. also, you can now jump and dash.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Jump by holding and releasing the spacebar, And dash by holding and releasing Left Alt.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Now that you have learned what your equipment does, Its time to teach you the basics of killing a Titan!";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "You can kill a titan by cutting their neck, so they can no longer heal.";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "Alot of people died tring to figure out how to kill a titan."+"                                                 (Get fucked george. No one misses you)";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }


    public IEnumerator TheEasterEgg()
    {
        tutorialState++;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }



    public void TutorialSkip()
    {
        
        
        

        

        if(skipped == false)
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
            player.GetComponent<TutorialMovement>().enabled = false;
            player.GetComponent<PlayerMovementAdvanced>().enabled = true;
            grapplinghook1.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook1.GetComponent<RotateGun>().enabled = true;
            grapplinghook1.GetComponent<GrappleRope>().enabled = true;
            grapplinghook1.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<LineRenderer>().enabled = true;
            grapplinghook2.GetComponent<GrapplingTest>().enabled = true;
            grapplinghook2.GetComponent<RotateGun>().enabled = true;
            grapplinghook2.GetComponent<GrappleRope>().enabled = true;
            jumpBar.SetActive(true);
            dashbar.SetActive(true);
            textBox.GetComponent<TextMeshProUGUI>().text = " ";
            equipmentClicked = false;
            grappleHolder.SetActive(true);
            StopAllCoroutines();
            waveSystem.GetComponent<WaveSystem>().one = true;


        }


    }

}
