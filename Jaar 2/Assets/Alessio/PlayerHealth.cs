using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
  public float maxHealth = 200;
  public float curHealth = 200;
 
  public GameObject player;
  public Transform medkid;
  public GameObject medkitGameObject;
  public GameObject gameOver; 
  public bool slurpjuice;
  public float slurpjuiceTime = 5;

  public TextMeshProUGUI healthText;
  public GameObject pressE;
  public bool pressedE;
  public bool medkitPickUp;

  public float distanceToMedkid;

    private void Start()
    {
        pressedE = false;
        pressE.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = (curHealth.ToString("f0") + "/" + maxHealth);

        if(curHealth == 0)
        {
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        float dist = Vector3.Distance(medkid.position, transform.position);
        distanceToMedkid = dist;

        if (distanceToMedkid <= 2)
        {
            pressedE = true;
            if(pressedE == true)
            {
                pressE.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                medkitPickUp = true;

                slurpjuice = true;
                Invoke("SlurpjuiceBoolean", slurpjuiceTime);

                medkitGameObject.SetActive(false);
            }
        }

        if(medkitPickUp == true)
        {
            pressE.SetActive(false);
        }

        if (distanceToMedkid >= 2)
        {
            pressedE = false;
            pressE.SetActive(false);
        }

        if (slurpjuice == true)
        {
            pressE.SetActive(false);
            curHealth += 10f * Time.deltaTime;
        }

        if (curHealth > 100)
        {
            slurpjuice = false;
        }
    }
  private void SlurpjuiceBoolean()
    {
        slurpjuice = false;
    }
 
  public void AddjustCurrentHealth(float adj)
  {
    Debug.Log(adj);
    curHealth -= adj;
  
    if (curHealth < 0)
    {
      curHealth = 0;
    }
  
    if (curHealth > maxHealth)
    {
      curHealth = maxHealth;
    }
    
    if (maxHealth < 1)
    {
      maxHealth = 1;
    }

    if (curHealth > 100)
    {
      curHealth = 100;
    }
  }
}

