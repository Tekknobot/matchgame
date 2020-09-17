using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAudioTimeScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
 
    public void ChangeAudioTime()
    {
        audioSource.time = audioSource.clip.length * slider.value;
    }
 
    public void Update()
    {
        Debug.Log(audioSource);
        slider.value = audioSource.time / audioSource.clip.length;
    }
}
