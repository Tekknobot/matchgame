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
        
        frequencies = new float[8];
        frequencies[0] = 262;
        frequencies[1] = 293;
        frequencies[2] = 330;
        frequencies[3] = 349;
        frequencies[4] = 392;                        
        frequencies[5] = 440;
        frequencies[6] = 494;
        frequencies[7] = 523;

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