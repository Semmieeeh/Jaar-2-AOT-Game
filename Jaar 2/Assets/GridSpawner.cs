using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridSpawner : MonoBehaviour
{

    public GameObject[] spawnAbles;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset;
    public float gridSpacingOffsetTwo;
    Vector3 gridOrigin;
    public float cycle;
    public Vector3 spawnPos;
    public float distanceBetweenPrefabs;
    


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
                
                
                 spawnPos = new Vector3(x * gridSpacingOffset, 0,z * gridSpacingOffsetTwo) + gridOrigin;
                spawnPos.z += 4;
                
                PickAndSpawn(spawnPos,Quaternion.identity);

                spawnPos = new Vector3(x * gridSpacingOffset, 0, -z * gridSpacingOffsetTwo) + gridOrigin;
                spawnPos.z -= 4;
                PickAndSpawn(spawnPos, Quaternion.identity);



                cycle += 1;
                if(cycle > 110)
                {

                    

                    if(gridZ == 10)
                    {
                        gridZ -= 2;
                    }

                    if(cycle > 130)
                    {
                        if (gridZ == 8)
                        {
                            gridZ -= 2;
                        }
                    }

                    if(cycle > 140)
                    {
                        if(gridZ == 6)
                        {
                            gridZ -= 2;
                        }
                    }
                }

            }
        }
    }


    public void PickAndSpawn(Vector3 positionToSpawn,Quaternion rotationToSpawn)
    {
        
        int randomIndex = Random.Range(0,spawnAbles.Length);
        Collider[] hitCollider = Physics.OverlapSphere(spawnPos,distanceBetweenPrefabs);
        bool canSpawn = true;




        for (int j = 0; j < hitCollider.Length; j++)
        {
            if (hitCollider[j].tag != "Ground")
            {
                canSpawn = false;
                continue;
            }
        }
        if (canSpawn)
        {
            GameObject g = Instantiate(spawnAbles[randomIndex], spawnPos, Quaternion.identity);
            g.transform.parent = transform;
        }
        
    }




}