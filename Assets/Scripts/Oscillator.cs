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

    void Start() {
        //gain = 0;
        
        frequencies = new float[16];
        frequencies[0] = 130.81f;
        frequencies[1] = 138.59f;
        frequencies[2] = 146.83f;
        frequencies[3] = 155.56f;
        frequencies[4] = 164.81f;                        
        frequencies[5] = 174.61f;
        frequencies[6] = 185.00f;
        frequencies[7] = 196.00f;
        frequencies[8] = 207.65f;
        frequencies[9] = 220.00f;
        frequencies[10] = 233.08f;
        frequencies[11] = 246.94f;
        frequencies[12] = 261.63f;                        
        frequencies[13] = 277.18f;
        frequencies[14] = 293.66f;
        frequencies[15] = 311.13f;        

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