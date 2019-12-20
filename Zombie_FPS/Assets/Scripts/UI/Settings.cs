using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    //public void SetVolume(float volume)
    //{
    //   audioMixer.SetFloat("volume", volume);
    //}

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log("This is " + qualityIndex);
    }

}
