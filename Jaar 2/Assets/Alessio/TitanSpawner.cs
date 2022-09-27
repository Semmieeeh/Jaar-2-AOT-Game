using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSpawner : MonoBehaviour
{
    public GameObject titan;
    private bool maySpawn;
    public float spawnTime;

    private void Start()
    {
        maySpawn = true;
    }
    void Update()
    {
        if(maySpawn == true)
        {
            Instantiate(titan);
            Invoke("Spawner", spawnTime);
            maySpawn = false;
        }
    }

    void Spawner()
    {
        maySpawn = true;
    }
}
