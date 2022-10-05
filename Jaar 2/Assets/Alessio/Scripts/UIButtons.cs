using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject back;
    public GameObject image;
    public GameObject imageEsc;
    public GameObject slider;
    public GameObject music;
    public GameObject fullscreen, resolution;
    public GameObject canvasSem;
    public GameObject currency;
    public GameObject creditsbutton;
    public GameObject waves;
    public GameObject resume;
    public GameObject restart;

    private bool playtrue;
    public bool escmenuon;
    public GameObject play;
    public void Start()
    {
        imageEsc.SetActive(false);
        credits.SetActive(false);
        back.SetActive(false);
        slider.SetActive(false);
        fullscreen.SetActive(false);
        resolution.SetActive(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canvasSem.SetActive(false);
        currency.SetActive(false);
        playtrue = false;
        waves.SetActive(false);
    }
    public void Update()
    {
        if(playtrue == false)
        {
            play.SetActive(true);
        }

        else if(playtrue == true)
        {
            creditsbutton.SetActive(false);
            play.SetActive(false);
            GameObject.Find("Canvas").GetComponent<Esc>().ResumeTrue();
        }
    }
    public void Play()
    {
        waves.SetActive(true);
        playtrue = true;
        mainMenu.SetActive(false);
        credits.SetActive(false);
        image.SetActive(false);
        music.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canvasSem.SetActive(true);
        currency.SetActive(true);
        imageEsc.SetActive(false);
        GameObject.Find("Canvas").GetComponent<Esc>().Played();
    }
    public void Escape()
    {
        mainMenu.SetActive(true);
        currency.SetActive(false);
        waves.SetActive(false);
        imageEsc.SetActive(true);
        resume.SetActive(true);
    }

    public void Waves()
    {
        waves.SetActive(false);
    }
    public void ExitEscape()
    {
        mainMenu.SetActive(false);
        image.SetActive(false);
        currency.SetActive(true);
        waves.SetActive(true);
        imageEsc.SetActive(false);
        resume.SetActive(false);
    }

    public void Options()
    {
        back.SetActive(true);
        mainMenu.SetActive(false);
        slider.SetActive(true);
        fullscreen.SetActive(true);
        resolution.SetActive(true);
        resume.SetActive(false);
        restart.SetActive(false);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        back.SetActive(true);
    }
    public void Back()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
        back.SetActive(false);
        slider.SetActive(false);
        fullscreen.SetActive(false);
        resolution.SetActive(false);

        if(escmenuon == true)
        {
            resume.SetActive(true);
            restart.SetActive(true);
        }  
    }
    public void escmenu()
    {
        escmenuon = true;
    }

    public void escmenuoff()
    {
        escmenuon = false;
    }
    public void EscBack()
    {
        credits.SetActive(false);
        mainMenu.SetActive(false);
        back.SetActive(false);
        slider.SetActive(false);
        fullscreen.SetActive(false);
        resolution.SetActive(false);
    }

    List<int> widhts = new List<int>() { 568, 960, 1280, 1920 };
    List<int> heights = new List<int>() { 320, 540, 720, 1080 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widhts[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
