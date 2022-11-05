using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
