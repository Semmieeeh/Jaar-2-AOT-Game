using JetBrains.Annotations;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    public GameObject[] spawnAbles;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset;
    public float gridSpacingOffsetTwo;
    Vector3 gridOrigin;


    public void Start()
    {
        gridOrigin = transform.position;

        SpawnGrid();
    }
    public void SpawnGrid()
    {
        for(int x = 0; x< gridX; x++)
        {
            for(int z = 0; z< gridZ; z++)
            {
                //gridSpacingOffset = Random.Range(1, 4);
                //gridSpacingOffsetTwo = Random.Range(1, 4);
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffsetTwo) + gridOrigin;
                //PickAndSpawn(spawnPosition, Quaternion.Euler(0, Random.Range(-100,100),0));
                PickAndSpawn(spawnPosition,Quaternion.identity);
                
            }
        }
    }


    public void PickAndSpawn(Vector3 positionToSpawn,Quaternion rotationToSpawn)
    {
        
        int randomIndex = Random.Range(0,spawnAbles.Length);
        GameObject clone = Instantiate(spawnAbles[randomIndex],positionToSpawn, rotationToSpawn);
    }




}