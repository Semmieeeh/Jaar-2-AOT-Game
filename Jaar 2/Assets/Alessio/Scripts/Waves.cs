using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Waves : MonoBehaviour
{
    public TMP_Text timerUI;
    public bool time;

    float timer = 20;
    void Update()
    {
        timerUI.text = "Timer:" + "/";

        if(time == true)
        {
            timerUI.text = "Timer:" + timer.ToString("00");

            timer -= 1 * Time.deltaTime;

            if (timer < 0f)
            {
                timer = 0f;
            }

            if (timer == 0f)
            {
                timer = 20f;
            }
        }
    }
}
