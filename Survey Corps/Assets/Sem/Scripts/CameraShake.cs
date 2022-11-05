using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject fpsCam;
    public Vector3 originalPos;

    public void Start()
    {
        
    }
    public void Update()
    {
        originalPos = transform.position;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
         

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;

                
        }

        fpsCam.transform.position = originalPos;
    }
}
