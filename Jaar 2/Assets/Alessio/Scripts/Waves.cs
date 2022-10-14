using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Waves : MonoBehaviour
{
    public TMP_Text timerUI;
    public bool time;
    public WaveSystem wave;

    float timer = 20;


    public void Start()
    {
        wave = GameObject.Find("TitanSpawner").GetComponent<WaveSystem>();
    }
    void Update()
    {
        timerUI.text = "" + "";

        if(time == true)
        {
            timerUI.text = "Timer:" + timer.ToString("00");

            timer -= 1 * Time.deltaTime;

            if (timer < 0f)
            {
                timer = 0f;
            }

            if (timer == 0f && wave.one == true)
            {
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveTwo());
                
                wave.two = true;
            }
            else if(timer == 0f &&wave.two == true)
            {
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveThree());
                
                wave.three = true;

            }
            else if(timer == 0f && wave.three == true)
            {
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveFour());

                wave.four = true;
            }
            else if (timer == 0f && wave.four == true)
            {
                time = false;
                timer = 0f;
                wave.StartCoroutine(wave.WaveFive());

                wave.five = true;
            }
            else if (timer == 0f && wave.five == true)
            {
                time = false;

                wave.finished = true;
            }
        }
    }
}
