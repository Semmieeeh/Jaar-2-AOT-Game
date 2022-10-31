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
    public bool allSpawnedone;
    public bool allSpawnedtwo;
    public bool allSpawnedthree;
    public bool allSpawnedfour;
    public bool allSpawnedfive;
    public GameObject winUI;
    public GameObject mainCanvas;
    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    public IEnumerator WaveOne()
    {
        one = true;
        for(int one =0; one<1; one++)
        {
            
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        yield return new WaitForSeconds(5);
        allSpawnedone = true;
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveTwo()
    {
        two = true;
        for (int one = 0; one < 2; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        yield return new WaitForSeconds(5);
        allSpawnedtwo = true;
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveThree()
    {
        three = true;
        for (int one = 0; one < 4; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }   
        yield return new WaitForSeconds(5);
        allSpawnedthree = true;
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
        
    }

    public IEnumerator WaveFour()
    {
        four = true;
        for (int one = 0; one < 8; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        yield return new WaitForSeconds(5);
        allSpawnedfour = true;
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;
    }

    public IEnumerator WaveFive()
    {
        five = true;
        for (int one = 0; one < 12; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
            continue;
        }
        yield return new WaitForSeconds(5);
        allSpawnedfive = true;
        fiveFinished = true;
        GameObject.Find("TitanSpawner").GetComponent<Waves>().time = true;


    }
}
