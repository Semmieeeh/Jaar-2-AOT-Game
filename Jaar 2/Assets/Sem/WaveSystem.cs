using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TutorialFinished tut;
    public bool canSpawnWaveOne;
    public GameObject[] titan;
    public GameObject[] enemiesInScene;
    public GameObject ui;
    
    public float waveTime;
    public bool one;
    public bool oneFinished;
    public bool two;
    public bool twoFinished;
    public bool three;
    public bool threeFinished;
    public bool four;
    public bool fourFinished;
    public bool five;
    public bool fiveFinished;
    public bool finished;
    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ui.GetComponent<TextMeshProUGUI>().text = null;
        enemiesInScene = GameObject.FindGameObjectsWithTag("Titan");
        if(titan == null)
        {
            titan = GameObject.FindGameObjectsWithTag("Titan");
        }


        if (tut.tutorialFinished == true)
        {
            canSpawnWaveOne = true;
            
        }

        if (tut.tutorialFinished == true &&canSpawnWaveOne == true)
        {
            
            canSpawnWaveOne = false;
            tut.tutorialFinished = false;
            StartCoroutine(WaveOne());
            
        }
        
        if (one == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 1";
            if(enemiesInScene == null)
            {
                oneFinished = true;
            }
        }

        if (two == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 2";
            one = false;
            if (enemiesInScene == null)
            {
                twoFinished = true;
            }
        }
        
        if (three == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 3";
            two = false;
            if (enemiesInScene == null)
            {
                threeFinished = true;
            }
        }

        if (four == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 4";
            three = false;
            if (enemiesInScene == null)
            {
                fourFinished = true;
            }
        }

        if (five == true)
        {          
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 5";
            four = false;
            if (enemiesInScene == null)
            {
                fiveFinished = true;
            }
        }

        if(finished == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: Finished";
            five = false;

        }
    }
    public IEnumerator WaveOne()
    {
        one = true;
        for(int one =0; one<3; one++)
        {
            
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveTwo()
    {
        two = true;
        for (int one = 0; one < 3; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveThree()
    {
        three = true;
        for (int one = 0; one < 3; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveFour()
    {
        four = true;
        for (int one = 0; one < 3; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
    }

    public IEnumerator WaveFive()
    {
        five = true;
        for (int one = 0; one < 3; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
    }
}
