using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSamples : MonoBehaviour
{
    public void clearSamplesOnClick() {
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().chopTime.Clear();
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().song.Clear();               
    }  
}
