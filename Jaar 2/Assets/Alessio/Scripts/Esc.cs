using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    public bool shopCanvasActive;
    public bool escActive;
    public bool gameOverMenu;
    public GameObject ui;
    public GameObject edges;
    public GameObject gameOver;
    public GameObject image;
    public GameObject currency;
    public GameObject resume;
    public GameObject restart,restart2;
    public bool played;
    public GameObject shop;
    public bool shopActive;
    public Transform wall;
    public Image deadImage;
    public GameObject playerUI;

    [Header("Shop")]
    public GameObject turrets;
    public GameObject traps;
    public GameObject images;
    public GameObject shopText;
    public float switchVoid;
    public float switchVoidTraps;

    [Header("Turrets")]
    public GameObject turretlvl1;
    public GameObject turretlvl2;
    public GameObject turretlvl3;

    [Header("Traps")]
    public GameObject traplvl1;
    public GameObject traplvl2;
    public GameObject traplvl3;
    

    // Start is called before the first frame update
    void Start()
    {
        escActive = false;
        shopCanvasActive = false;
        gameOverMenu = true;
        resume.SetActive(false);
        restart.SetActive(false);
        if(restart2 != null)
        {
            restart2.SetActive(true);
        }
    }
    void Update()
    {
        deadImage.GetComponent<Image>().color = new Color(255, 0, 0, 1);

        if (played == true)
        {
            if (gameOver.activeSelf == true)
            {
                currency.SetActive(false);
                gameOverMenu = false;
            }

            

            if (gameOverMenu == true)
            {
                if (escActive == false)
                {
                    if (Input.GetKeyDown(KeyCode.Escape)&&shopActive == false)
                    {
                        restart.SetActive(true);
                        escActive = true;
                        Time.timeScale = 0f;
                        FindObjectOfType<AudioManager>().StopAllAudio();
                        playerUI.SetActive(false);
                        
                        canvas.GetComponent<UIButtons>().Escape();
                        canvas.GetComponent<UIButtons>().escmenu();
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        canvasSem.SetActive(false);
                        shopActive = true;

                    }
                }
                else if (escActive == true)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0f;
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        edges.SetActive(false);
                        restart.SetActive(false);
                        ui.SetActive(false);
                        escActive = false;
                        Time.timeScale = 1f;
                        playerUI.SetActive(true);
                        canvas.GetComponent<UIButtons>().ExitEscape();
                        canvas.GetComponent<UIButtons>().EscBack();
                        canvas.GetComponent<UIButtons>().escmenuoff();
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        canvasSem.SetActive(true);
                        shopActive = true;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;

                    }

                }

                if(shopActive == false)
                {
                    if (Input.GetKeyDown(KeyCode.Tab) && escActive == false && played == true)
                    {
                        shopCanvasActive = true;
                        shop.SetActive(true);
                        shopActive = true;
                        Time.timeScale = 0f;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        turrets.SetActive(true);
                        shopText.SetActive(true);
                        canvasSem.SetActive(false);
                    }
                }
                
                else if(shopActive == true)
                {
                    if (Input.GetKeyDown(KeyCode.Tab)||Input.GetKeyDown(KeyCode.Escape))
                    {
                        shopCanvasActive = false;
                        shop.SetActive(false);
                        shopActive = false;
                        Time.timeScale = 1f;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        traps.SetActive(false);
                        canvasSem.SetActive(true); 
                    }
                }
            }
        }  
    }
    public void Played()
    {
        played = true;
    }
    public void Resume()
    {
        escActive = false;
        Time.timeScale = 1f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        GameObject.Find("MainMenu").GetComponent<UIButtons>().Play();
        canvas.GetComponent<UIButtons>().escmenuoff();
        resume.SetActive(false);
        restart.SetActive(false);
        shopCanvasActive = false;
        FindObjectOfType<AudioManager>().PlayAudio(12,1,1);
        FindObjectOfType<AudioManager>().audioSources[12].volume = 0;
    }
    public void ResumeTrue()
    {
        resume.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void BuyTurret()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 10 && eco.turrets <=4)
        {
            eco.turrets += 1;
            eco.metal -= 10;
        }
    }
    public void BuyTurretLvl2()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 10 && eco.turrets <= 4)
        {
            turretlvl2.SetActive(false);
            turretlvl3.SetActive(true);
            eco.turrets += 1;
            eco.metal -= 10;
        }
    }
    public void BuyTurretLvl3()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 10 && eco.turrets <= 4)
        {
            eco.turrets += 1;
            eco.metal -= 10;

        }
    }
    public void UpgradeTurrets()
    {
        switchVoid += 1f;

        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 100 && switchVoid <= 2)
        {
            eco.turretLvl += 1;
            eco.metal -= 100;
        }

        if (switchVoid == 0f)
        {
            turretlvl1.SetActive(true);
            turretlvl2.SetActive(false);
            turretlvl3.SetActive(false);
        }

        if (switchVoid == 1f)
        {
            turretlvl1.SetActive(false);
            turretlvl2.SetActive(true);
            turretlvl3.SetActive(false);
        }

        if (switchVoid == 2f)
        {
            turretlvl1.SetActive(false);
            turretlvl2.SetActive(false);
            turretlvl3.SetActive(true);
        }
    }
    public void UpgradeTraps()
    {
        switchVoidTraps += 1f;

        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 50)
        {
            eco.metal -= 50;
        }

        if (switchVoidTraps == 0f)
        {
            traplvl1.SetActive(true);
            traplvl2.SetActive(false);
            traplvl3.SetActive(false);
        }

        if (switchVoidTraps == 1f)
        {
            traplvl1.SetActive(false);
            traplvl2.SetActive(true);
            traplvl3.SetActive(false);
        }

        if (switchVoidTraps == 2f)
        {
            traplvl1.SetActive(false);
            traplvl2.SetActive(false);
            traplvl3.SetActive(true);
        }
    }
    public void BuyTrap()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 5)
        {
            traplvl1.SetActive(false);
            traplvl2.SetActive(true);
            eco.traps += 1;
            eco.metal -= 5;
        }
    }
    public void BuyTrapLvl2()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 5)
        {
            traplvl2.SetActive(false);
            traplvl3.SetActive(true);
            eco.traps += 1;
            eco.metal -= 5;
        }
    }
    public void BuyTrapLvl3()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 5)
        {
            eco.traps += 1;
            eco.metal -= 5;
        }
    }
    public void ExitShopMenu()
    {
        shop.SetActive(false);
    }
    public void NextPage()
    {
        turrets.SetActive(false);
        traps.SetActive(true);
    }

    public void PreviousPage()
    {
        turrets.SetActive(true);
        traps.SetActive(false);
    }
}