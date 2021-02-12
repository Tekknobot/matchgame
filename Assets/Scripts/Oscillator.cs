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

    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);

    void Start() {
        gain = 0;
        
        frequencies = new float[16];
        frequencies[0] = 261.63f;   // C4
        frequencies[1] = 277.18f;   // C#4
        frequencies[2] = 293.66f;   // D4 
        frequencies[3] = 311.13f;   // D#4
        frequencies[4] = 329.63f;   // E4                      
        frequencies[5] = 349.23f;   // F4
        frequencies[6] = 369.99f;   // F#4
        frequencies[7] = 392.00f;   // G4
        frequencies[8] = 415.30f;   // G#4
        frequencies[9] = 440.00f;   // A4
        frequencies[10] = 466.16f;  // A#4
        frequencies[11] = 493.88f;  // B4
        frequencies[12] = 523.25f;  // C5                        
        frequencies[13] = 554.37f;  // C#5
        frequencies[14] = 587.33f;  // D5
        frequencies[15] = 622.25f;  // D#5      
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