using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public int numberOfObjects;
    public float radius;
    public float minDistanceBetweenSpawns;
    public bool pressedSpace;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedSpace = true;
            
        }
        if(pressedSpace == true && radius > 0)
        {
            //StartCoroutine(PressSpace());\
            for (int i = 0; i < numberOfObjects; i++)
            {

                float angle = i * Mathf.PI * 2 / numberOfObjects;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                int spawnAblesNumber = Random.Range(0, prefabs.Length);
                Vector3 pos = transform.position + new Vector3(x, 0, z);

                float angleDegrees = -angle * Mathf.Rad2Deg;
                //Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                Collider[] hitCollider = Physics.OverlapSphere(pos, minDistanceBetweenSpawns);
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
                    GameObject g = Instantiate(prefabs[spawnAblesNumber], pos, Quaternion.identity);
                    g.transform.parent = transform;
                }

            }

            radius -= Random.Range(1,2);
            numberOfObjects += Random.Range(-2,2);
        }


        if(radius <= 0)
        {
            gameObject.GetComponent<CircleGenerator>().enabled = false;
        }

    }
    
   // public IEnumerator PressSpace()
  //  {
        
  //  }


}
