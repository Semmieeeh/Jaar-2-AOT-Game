using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    public bool canvasActive;
    public bool gameOverMenu;

    public GameObject gameOver;
    public GameObject image;
    public GameObject currency;
    public GameObject resume;
    public GameObject restart,restart2;
    public bool played;

    public Transform wall;
    // Start is called before the first frame update
    void Start()
    {
        canvasActive = false;
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

            if (gameOverMenu == true)
            {
                if (canvasActive == false)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        restart.SetActive(true);
                        canvasActive = true;
                        Time.timeScale = 0f;
                        canvas.GetComponent<UIButtons>().Escape();
                        canvas.GetComponent<UIButtons>().escmenu();
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        canvasSem.SetActive(false);

                    }
                }
                else if (canvasActive == true)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        restart.SetActive(false);
                        canvasActive = false;
                        Time.timeScale = 1f;
                        canvas.GetComponent<UIButtons>().ExitEscape();
                        canvas.GetComponent<UIButtons>().EscBack();
                        canvas.GetComponent<UIButtons>().escmenuoff();
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
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
        GameObject.Find("MainMenu").GetComponent<UIButtons>().Play();
        canvas.GetComponent<UIButtons>().escmenuoff();
        resume.SetActive(false);
        restart.SetActive(false);
        canvasActive = false;
    }

    public void ResumeTrue()
    {
        resume.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}