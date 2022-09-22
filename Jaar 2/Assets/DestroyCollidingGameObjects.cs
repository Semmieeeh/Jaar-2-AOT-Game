using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollidingGameObjects : MonoBehaviour
{
    public RaycastHit hit;
    public LayerMask spawnAble;

    void Start()
    {
        //StartCoroutine(CheckForCollision());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CheckForCollision()
    {
        yield return new WaitForSeconds(3);
        if (Physics.SphereCast(gameObject.transform.position, 2f, gameObject.transform.forward, out hit, 10f, spawnAble))
        {
            Destroy(gameObject);
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == spawnAble)
        {
            Destroy(gameObject);
        }
    }

}
