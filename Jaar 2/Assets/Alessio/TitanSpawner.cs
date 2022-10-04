using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSpawner : MonoBehaviour
{
    public GameObject titan;
    public bool maySpawn;
    public float spawnTime;

    private void Start()
    {
        maySpawn = false;
        
    }
    void Update()
    {
        if(maySpawn == true)
        {
            //Invoke("Spawner", spawnTime);
            Instantiate(titan);
            
            maySpawn = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            maySpawn = true;
        }
    }

    
}
