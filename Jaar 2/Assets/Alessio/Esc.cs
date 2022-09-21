using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    public bool canvasActive;

    public GameObject gameOver;
    public GameObject image;

    public Transform wall;
    // Start is called before the first frame update
    void Start()
    {
        canvasActive = false;
        gameOver.SetActive(false);
    }

    void Update()
    {
        if(wall.GetComponent<Healthbarscript>().currentHeal < 0f)
        {
            gameOver.SetActive(true);
            image.SetActive(true);
            Time.timeScale = 0f;
        }

        if (canvasActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
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