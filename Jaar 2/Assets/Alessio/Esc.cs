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
    public GameObject restart;

    public Transform wall;
    // Start is called before the first frame update
    void Start()
    {
        canvasActive = false;
        gameOver.SetActive(false);
        gameOverMenu = true;
        resume.SetActive(false);
        restart.SetActive(false);
    }

    void Update()
    {
        if(wall.GetComponent<Healthbarscript>().currentHeal < 0f)
        {
            Cursor.lockState = CursorLockMode.None;
            restart.SetActive(true);
            gameOver.SetActive(true);
            image.SetActive(true);
            Time.timeScale = 0f;
        }

        if(gameOver.activeSelf == true)
        {
            currency.SetActive(false);
            gameOverMenu = false;
        }

        if(gameOverMenu == true)
        {
            if (canvasActive == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    restart.SetActive(true);
                    canvasActive = true;
                    Time.timeScale = 0f;
                    canvas.GetComponent<UIButtons>().Escape();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //canvasSem.SetActive(true);

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
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    //canvasSem.SetActive(false);
                }

            }
        }   
    }
    public void Resume()
    {
        GameObject.Find("MainMenu").GetComponent<UIButtons>().Play();
        resume.SetActive(false);
        restart.SetActive(false);
    }

    public void ResumeTrue()
    {
        resume.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Servey Corps");
    }
}