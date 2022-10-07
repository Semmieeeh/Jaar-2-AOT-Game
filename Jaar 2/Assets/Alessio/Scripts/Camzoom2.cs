using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camzoom2 : MonoBehaviour
{
    public float fieldOfView;
    public GameObject menuCam;
    public bool fovmax;
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
        if(fovmax == false)
        {
            fieldOfView -= 1 * Time.unscaledDeltaTime;
        }

        if(fieldOfView < 45)
        {
            fovmax = true;
            fieldOfView += 1 * Time.unscaledDeltaTime;
        }

        if (fieldOfView > 50)
        {
            fovmax = false;
            fieldOfView -= 1 * Time.unscaledDeltaTime;
        }

        menuCam.GetComponent<Camera>().fieldOfView = fieldOfView;
    }
}
