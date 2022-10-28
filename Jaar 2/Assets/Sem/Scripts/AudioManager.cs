using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public int currentlyPlaying;
    public bool curPlaying;
    public bool stop;


    public void Update()
    {
        if(stop == true)
        {
            StopCoroutine(CountDown(12));
        }
    }
    public void PlayAudio(int toPLay, float minimalPitch, float maximumPitch)
    {

        float newPitch = Random.Range(minimalPitch, maximumPitch);
        audioSources[toPLay].pitch = newPitch;
        audioSources[toPLay].Play();
        currentlyPlaying = toPLay;
    }
    public void StopAudio(int toStop)
    {
        audioSources[toStop].Stop();

    }
    public void StopAllAudio()
    {
        for(int i =0; i < audioSources.Length; i++)
        {
            audioSources[i].Stop();
        }
        

    }
    public void PlayOnce(int toPlay)
    {
        if(curPlaying == true)
        {
            CountDown(toPlay);
        }

    }
    
    public IEnumerator CountDown(int toPlay)
    {
        
        audioSources[12].Play();
        yield return new WaitForSeconds(17.943f);
        StopAudio(toPlay);
        
    }

    
}
