using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
    // Start is called before the first frame update
    public bool obstructed;
    public GameObject cannon;
    
    void Start()
    {
        obstructed = false;
        
    }
    public void Update()
    {
        if(cannon != null)
        {
            obstructed = true;
        }
        else
        {
            obstructed = false;
        }
    }


}
