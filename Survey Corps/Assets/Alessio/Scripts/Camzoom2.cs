using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camzoom2 : MonoBehaviour
{
    public float fieldOfView;
    public GameObject menuCam;
    private bool fovmax;
    private bool may;
    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = 60.0f;
        menuCam = GameObject.Find("CameraMenu");
        fovmax = false;
    }

    // Update is called once per frame
    void Update()
    {
        menuCam.GetComponent<Camera>().fieldOfView = fieldOfView;

        if(may == true)
        {
            if (fovmax == false)
            {
                fieldOfView -= 1 * Time.unscaledDeltaTime;
            }
        }
        
        else if(fovmax == true)
        {
            fieldOfView += 1 * Time.unscaledDeltaTime;
        }

        if(fieldOfView < 45)
        {
            may = false;
            fovmax = true;  
        }

        if(fieldOfView >= 60)
        {
            may = true;
            fovmax = false;
        }
    }
}
