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
    public GameObject cam;
    public GameObject fovSlider;
    public GameObject panels;
    public GameObject maincam;
    public GameObject shopUI;
    public Vector3 scale;
    public GameObject panelFront, panelBack;
    private bool playtrue;
    public bool escmenuon;
    public GameObject play;
    public GameObject titel;
    public GameObject slidersensitivity;
    public GameObject cantBuy;
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
        fovSlider.SetActive(false);
        panels.transform.localScale = scale;
        slidersensitivity.SetActive(false);
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
            GameObject.Find("MainCanvas").GetComponent<Esc>().ResumeTrue();
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
        GameObject.Find("MainCanvas").GetComponent<Esc>().Played();
        cam.SetActive(false);
        panels.SetActive(false);
        titel.SetActive(false);
    }
    public void Escape()
    {
        mainMenu.SetActive(true);
        currency.SetActive(false);
        waves.SetActive(false);
        imageEsc.SetActive(true);
        resume.SetActive(true);
        //cam.SetActive(true);
        panels.SetActive(true);
        titel.SetActive(true);
        slidersensitivity.SetActive(false);
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
        //cam.SetActive(false);
        panels.SetActive(false);
        titel.SetActive(false);
        slidersensitivity.SetActive(false);
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
        fovSlider.SetActive(true);
        panelFront.transform.localScale = new Vector3(0.5f, 0.8f, 1);
        titel.SetActive(false);
        slidersensitivity.SetActive(true);
    }
    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        back.SetActive(true);
        scale.x = 0.3f;
        panelFront.transform.localScale = new Vector3(0.3f, 0.8f, 1);
        titel.SetActive(false);
    }
    public void Back()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
        back.SetActive(false);
        slider.SetActive(false);
        fullscreen.SetActive(false);
        resolution.SetActive(false);
        fovSlider.SetActive(false);
        panelFront.transform.localScale = new Vector3(0.3f, 0.8f, 1);
        if (escmenuon == true)
        {
            resume.SetActive(true);
            restart.SetActive(true);
        }
        titel.SetActive(true);
        slidersensitivity.SetActive(false);
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
        panelFront.transform.localScale = new Vector3(0.3f, 0.8f, 1);
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
    public void BuyTurret()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if (eco.metal >= 10)
        {
            eco.turrets += 1;
            eco.metal -= 10;
        }
        else if(eco.metal < 10)
        {
            cantBuy.SetActive(true);
            Invoke("CantBuy", 0.2f);
        }      
    }
    public void BuyTrap()
    {
        Economy eco = GameObject.Find("Player").GetComponent<Economy>();
        if(eco.metal >= 5)
        {
            eco.traps += 1;
            eco.metal -= 5;
        }
        else if (eco.metal < 5)
        {
            cantBuy.SetActive(true);
            Invoke("CantBuy", 0.2f);
        }
    }
    public void CantBuy()
    {
        cantBuy.SetActive(false);
    }
    public void ExitShopMenu()
    {
        shopUI.SetActive(false);
    }
}
