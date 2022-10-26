using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Economy : MonoBehaviour
{
    public float metal;
    public float turretCost;
    public float payment;
    public int turrets;
    public int traps;
    public GameObject turretUI;
    // Start is called before the first frame update
    void Start()
    {
        payment = 10;
        turrets = 1;
        traps = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            metal += 20f;
        }
        //slider. value = metal

        turretUI.GetComponent<TextMeshProUGUI>().text = "Cannons: " + turrets.ToString();
    }
}
