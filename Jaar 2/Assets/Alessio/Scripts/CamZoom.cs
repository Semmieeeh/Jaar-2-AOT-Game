using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamZoom : MonoBehaviour
{
    public float fieldOfView;
    public Slider fovSlider;
    public GameObject menuCam;
    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = 60.0f;
        fovSlider.value = fieldOfView;
        menuCam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        fieldOfView = fovSlider.value * 1f;
        menuCam.GetComponent<Camera>().fieldOfView = fieldOfView;
    }
}
