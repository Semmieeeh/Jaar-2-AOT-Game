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
    public GameObject slider;
    public GameObject music;
    public GameObject fullscreen, resolution;
    public GameObject canvasSem;
    public GameObject currency;
    public void Start()
    {
        credits.SetActive(false);
        back.SetActive(false);
        slider.SetActive(false);
        fullscreen.SetActive(false);
        resolution.SetActive(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //canvasSem.SetActive(false);
        currency.SetActive(false);
    }
    public void Play()
    {
        mainMenu.SetActive(false);
        credits.SetActive(false);
        image.SetActive(false);
        music.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //canvasSem.SetActive(true);
        currency.SetActive(true);
    }
    public void Escape()
    {
        mainMenu.SetActive(true);
        image.SetActive(true);
        currency.SetActive(false);
    }
    public void ExitEscape()
    {
        mainMenu.SetActive(false);
        image.SetActive(false);
        currency.SetActive(true);

    }

    public void Options()
    {
        back.SetActive(true);
        mainMenu.SetActive(false);
        slider.SetActive(true);
        fullscreen.SetActive(true);
        resolution.SetActive(true);
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
