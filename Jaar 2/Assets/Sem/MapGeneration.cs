using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] spawnAbles;
    public int spawnAblesCount;
    public float xMax,xMin,zMax,zMin;
    public RaycastHit hit;
    public LayerMask spawnAble;
    public int j;
    public float minDistanceBetweenSpawns;
    void Start()
    {

        for(int i = 0; i < spawnAblesCount; i++)
        {
            int spawnAblesArrayIndex = Random.Range(0, spawnAbles.Length);
            
            Vector3 pos = new Vector3(Random.Range(gameObject.transform.position.x + xMax, gameObject.transform.position.x - xMin), 0, Random.Range(gameObject.transform.position.z +zMax, gameObject.transform.position.z - zMin));
            Collider[] hitColliders = Physics.OverlapSphere(pos, minDistanceBetweenSpawns);
            bool canSpawn = true;
            for(int j = 0; j < hitColliders.Length; j++)
            {
                if (hitColliders[j].tag != "Ground")
                {
                    canSpawn = false;
                    continue;
                }
            }
            if (canSpawn)
            {
                GameObject g = Instantiate(spawnAbles[spawnAblesArrayIndex], pos, Quaternion.identity);
                g.transform.parent = transform;
            }
        } 
    }
    
}
