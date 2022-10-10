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
        maySpawn = true;
    }
    void Update()
    {
        if(maySpawn == true)
        {
            Invoke("Spawner", spawnTime);
            Instantiate(titan);
            
            maySpawn = false;
        }
    }
    void Spawner()
    {
        maySpawn = true;
    }
}
