using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public CapsuleCollider playerCollider;
    public CapsuleCollider enemyCollider;
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<CapsuleCollider>();
        enemyCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        Physics.IgnoreCollision(playerCollider, enemyCollider,true);
    }
}
