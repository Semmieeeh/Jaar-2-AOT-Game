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
    public GameObject waves;
    public float waveTime;
    public bool one;
    public bool two;
    public bool three;
    public bool four;
    public bool five;

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
            StartCoroutine(WaveOne());
            canSpawnWaveOne = false;
            tut.tutorialFinished = false;
        }

        





        if(one == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 1";
        }

        if (two == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 2";
        }
        
        if (three == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 3";
        }

        if (four == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 4";
        }

        if (five == true)
        {
            ui.GetComponent<TextMeshProUGUI>().text = "Wave: 5";
        }


    }
    public IEnumerator WaveOne()
    {
        one = true;
        for(int one =0; one<3; one++)
        {
            Instantiate(titan[Random.Range(0, titan.Length)]);
            yield return new WaitForSeconds(waveTime);
        }
    }

    

}
