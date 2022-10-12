using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public int currentlyPlaying;

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

    

    
}
