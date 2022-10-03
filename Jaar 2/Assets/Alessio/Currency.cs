using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Currency : MonoBehaviour
{
    public TMP_Text metalUi;
    public Economy eco;
    // Start is called before the first frame update
    void Start()
    {
        eco = GameObject.Find("Player").GetComponent<Economy>();
    }

    // Update is called once per frame
    void Update()
    {
        metalUi.text = "Metal:" + eco.metal.ToString("00");
    }
}
