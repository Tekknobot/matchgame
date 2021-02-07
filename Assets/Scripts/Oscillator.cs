using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;
    
    public float gain = 0.1f;
    public float volume = 0.1f;

    public float[] frequencies;
    public int thisfreq;

    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);

    public GameObject note_C;
    public GameObject note_D;
    public GameObject note_E;
    public GameObject note_F;
    public GameObject note_G;
    public GameObject note_A;
    public GameObject note_B;
    public GameObject note_C1;

    void Start() {
        gain = 0;
        
        frequencies = new float[8];
        frequencies[0] = 440;
        frequencies[1] = 494;
        frequencies[2] = 554;
        frequencies[3] = 587;
        frequencies[4] = 659;                        
        frequencies[5] = 740;
        frequencies[6] = 831;
        frequencies[7] = 880;

        note_C = GameObject.Find("Note 1");
    }

    void Update() {
        if (note_C.GetComponent<OperatorTile>().render.sprite.name == "C 0" && note_C.GetComponent<OperatorTile>().render.color == selectedColor) {
            gain = volume;
            frequency = frequencies[0];
            thisfreq = thisfreq % frequencies.Length;
        }  
        else {
            gain = 0;
        }     
    }

    void OnAudioFilterRead(float[] data, int channels) {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels) {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }
    }
}      