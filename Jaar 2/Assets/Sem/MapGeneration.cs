using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] spawnAbles;
    public int spawnAblesCount;
    public float xMax,xMin,zMax,zMin;
    public RaycastHit hit;
    public LayerMask spawnAble;
    void Start()
    {

        for(int i = 0; i < spawnAblesCount; i++)
        {
            int spawnAblesArrayIndex = Random.Range(0, spawnAbles.Length);
            int j = 0;
            Vector3 pos = new Vector3(Random.Range(gameObject.transform.position.x + xMax, gameObject.transform.position.x - xMin), 0, Random.Range(gameObject.transform.position.z + zMax, gameObject.transform.position.z - zMin));
            while (Physics.SphereCast(pos, 5, Vector3.right, out hit, 15,spawnAble))
            {
                Debug.Log("hit cube");
                if(j > 100)
                {
                    i++;
                    break;
                    
                }
                pos = new Vector3(Random.Range(gameObject.transform.position.x + xMax, gameObject.transform.position.x - xMin), Random.Range(0, 0), Random.Range(gameObject.transform.position.z + zMax, gameObject.transform.position.z - zMin));
                j++;
            }
            GameObject g = Instantiate(spawnAbles[spawnAblesArrayIndex], pos, Quaternion.identity);
            g.transform.parent = transform;

        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
