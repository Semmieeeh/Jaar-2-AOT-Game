using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Waves : MonoBehaviour
{
    public TMP_Text waveUi;
    public TMP_Text timerUI;

    float wave = 0;
    float timer = 90;
    void Update()
    {
        waveUi.text = "Wave:" + wave.ToString("00");

        timerUI.text = "Timer:" + timer.ToString("00");

        timer -= 1 * Time.deltaTime;
        if (timer < 0f)
        {
            timer = 0f;
        }

        if (timer == 0f)
        {
            wave += 1f;
            timer = 90f;
            GameObject.Find("TitanSpawner").GetComponent<TitanSpawner>().spawnTime -= 1f;
        }
    }
}
