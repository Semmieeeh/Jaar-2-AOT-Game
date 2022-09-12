using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasSem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            canvas.GetComponent<UIButtons>().Escape();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            canvasSem.SetActive(false);
        }
    }
}
