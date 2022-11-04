using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Waves : MonoBehaviour
{
    public TMP_Text timerUI;
    public TMP_Text text;
    public bool time;
    public WaveSystem wave;
    
    public bool initialWave;
    float timer = 20;


    public void Start()
    {
        initialWave = true;
        wave = GameObject.Find("TitanSpawner").GetComponent<WaveSystem>();
    }
    void Update()
    {
        

        if(time == true)
        {
            timerUI.text = "Next Wave In:" + timer.ToString("00");

            timer -= 1 * Time.deltaTime;

            if (timer < 0f)
            {
                timer = 0f;
            }

            if(timer == 0f && initialWave == true)
            {
                initialWave = false;
                Debug.Log("WaveOneStarted");
                wave.one = true;
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveOne());
                text.text = "Wave: 1";
                wave.two = true;
            }

            if (timer == 0f && wave.oneFinished == true)
            {
                Debug.Log("WaveOneFinished");
                wave.one = false;
                wave.oneFinished = false;
                time = false;
                timer = 20f;
                text.text = "Wave: 2";
                wave.StartCoroutine(wave.WaveTwo());
                
                wave.two = true;
            }
            else if(timer == 0f &&wave.twoFinished == true)
            {
                wave.two = false;
                wave.twoFinished = false;
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveThree());
                text.text = "Wave: 3";
                wave.three = true;

            }
            else if(timer == 0f && wave.threeFinished == true)
            {
                wave.three = false;
                wave.threeFinished = false;
                time = false;
                timer = 20f;
                wave.StartCoroutine(wave.WaveFour());
                text.text = "Wave: 4";
                wave.four = true;
            }
            else if (timer == 0f && wave.fourFinished == true)
            {
                wave.four = false;
                wave.fourFinished = false;
                time = false;
                timer = 0f;
                wave.StartCoroutine(wave.WaveFive());
                text.text = "Wave: 5";

                wave.five = true;
            }
            else if (timer == 0f && wave.fiveFinished == true)
            {
                wave.five = false;
                wave.fiveFinished = false;
                time = false;

                wave.finished = true;
            }
        }
    }
}
