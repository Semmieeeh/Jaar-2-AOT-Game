using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    public bool shopCanvasActive;
    public bool escActive;
    public bool gameOverMenu;

    public GameObject gameOver;
    public GameObject image;
    public GameObject currency;
    public GameObject resume;
    public GameObject restart,restart2;
    public bool played;
    public GameObject shop;
    public bool shopActive;
    public GameObject cantBuy;
    public Transform wall;
    // Start is called before the first frame update
    void Start()
    {
        escActive = false;
        shopCanvasActive = false;
        gameOver.SetActive(false);
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
        





        if(played == true)
        {
            //if(wall.GetComponent<Healthbarscript>().currentHeal < 0f)
            //{
            //Cursor.lockState = CursorLockMode.None;
            // restart.SetActive(true);
            //gameOver.SetActive(true);
            //image.SetActive(true);
            //Time.timeScale = 0f;
            //}

            if (gameOver.activeSelf == true)
            {
                currency.SetActive(false);
                gameOverMenu = false;
            }


            if(escActive == true||shopActive == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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
                        
                        restart.SetActive(false);
                        escActive = false;
                        Time.timeScale = 1f;
                        canvas.GetComponent<UIButtons>().ExitEscape();
                        canvas.GetComponent<UIButtons>().EscBack();
                        canvas.GetComponent<UIButtons>().escmenuoff();
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        canvasSem.SetActive(true);
                        shopActive = true;

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
        if (eco.metal >= 10 && eco.turrets <=5)
        {
            eco.turrets += 1;
            eco.metal -= 10;
        }
        else if (eco.metal < 10)
        {
            cantBuy.SetActive(true);
            StartCoroutine(CantBuy());
        }
    }
    public void BuyTrap()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 5)
        {
            eco.traps += 1;
            eco.metal -= 5;
        }
        else if (eco.metal < 5)
        {
            cantBuy.SetActive(true);
            StartCoroutine(CantBuy());
        }
    }
    public IEnumerator CantBuy()
    {
        yield return new WaitForSeconds(0.2f);
        cantBuy.SetActive(false);
    }
    public void ExitShopMenu()
    {
        shop.SetActive(false);
    }
}