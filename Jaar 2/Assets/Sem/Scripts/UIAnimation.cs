using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public Animator anim;
    public bool canFire;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Crosshair").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("canFire", canFire);
    }
}
