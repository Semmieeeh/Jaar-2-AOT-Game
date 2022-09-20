using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    public bool canvasActive;
    // Start is called before the first frame update
    void Start()
    {
        canvasActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvasActive == false)
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
        else if(canvasActive == true)
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
