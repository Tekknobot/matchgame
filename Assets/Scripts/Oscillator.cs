using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oscillator : MonoBehaviour {
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;
    
    public float gain = 0.1f;
    public float volume = 0.1f;

    public float[] frequencies;

    public Button octaveDown;
    public Button octaveUp;
    public Button octaveDefault;

    public int octaveDownInt = -12;
    public int octaveUpInt = 12;

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

        Button octBtnDef = octaveDefault.GetComponent<Button>();
		octBtnDef.onClick.AddListener(OctaveDefaultOnClick); 

        Button octBtnDown = octaveDown.GetComponent<Button>();
		octBtnDown.onClick.AddListener(OctaveDownOnClick);

        Button octBtnUp = octaveUp.GetComponent<Button>();
		octBtnUp.onClick.AddListener(OctaveUpOnClick);        
    }

	void OctaveDefaultOnClick(){
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
		Debug.Log ("Octave DEFAULT!");
	}

	void OctaveDownOnClick(){
        frequencies[0] = 261.63f * Mathf.Pow(1.05946f, octaveDownInt);   // C4
        frequencies[1] = 277.18f * Mathf.Pow(1.05946f, octaveDownInt);   // C#4
        frequencies[2] = 293.66f * Mathf.Pow(1.05946f, octaveDownInt);   // D4 
        frequencies[3] = 311.13f * Mathf.Pow(1.05946f, octaveDownInt);   // D#4
        frequencies[4] = 329.63f * Mathf.Pow(1.05946f, octaveDownInt);   // E4                      
        frequencies[5] = 349.23f * Mathf.Pow(1.05946f, octaveDownInt);   // F4
        frequencies[6] = 369.99f * Mathf.Pow(1.05946f, octaveDownInt);   // F#4
        frequencies[7] = 392.00f * Mathf.Pow(1.05946f, octaveDownInt);   // G4
        frequencies[8] = 415.30f * Mathf.Pow(1.05946f, octaveDownInt);   // G#4
        frequencies[9] = 440.00f * Mathf.Pow(1.05946f, octaveDownInt);   // A4
        frequencies[10] = 466.16f * Mathf.Pow(1.05946f, octaveDownInt);  // A#4
        frequencies[11] = 493.88f * Mathf.Pow(1.05946f, octaveDownInt);  // B4
        frequencies[12] = 523.25f * Mathf.Pow(1.05946f, octaveDownInt);  // C5                        
        frequencies[13] = 554.37f * Mathf.Pow(1.05946f, octaveDownInt);  // C#5
        frequencies[14] = 587.33f * Mathf.Pow(1.05946f, octaveDownInt);  // D5
        frequencies[15] = 622.25f * Mathf.Pow(1.05946f, octaveDownInt);  // D#5         
		Debug.Log ("Octave Down!");
	}

	void OctaveUpOnClick(){
        frequencies[0] = 261.63f * Mathf.Pow(1.05946f, octaveUpInt);   // C4
        frequencies[1] = 277.18f * Mathf.Pow(1.05946f, octaveUpInt);   // C#4
        frequencies[2] = 293.66f * Mathf.Pow(1.05946f, octaveUpInt);   // D4 
        frequencies[3] = 311.13f * Mathf.Pow(1.05946f, octaveUpInt);   // D#4
        frequencies[4] = 329.63f * Mathf.Pow(1.05946f, octaveUpInt);   // E4                      
        frequencies[5] = 349.23f * Mathf.Pow(1.05946f, octaveUpInt);   // F4
        frequencies[6] = 369.99f * Mathf.Pow(1.05946f, octaveUpInt);   // F#4
        frequencies[7] = 392.00f * Mathf.Pow(1.05946f, octaveUpInt);   // G4
        frequencies[8] = 415.30f * Mathf.Pow(1.05946f, octaveUpInt);   // G#4
        frequencies[9] = 440.00f * Mathf.Pow(1.05946f, octaveUpInt);   // A4
        frequencies[10] = 466.16f * Mathf.Pow(1.05946f, octaveUpInt);  // A#4
        frequencies[11] = 493.88f * Mathf.Pow(1.05946f, octaveUpInt);  // B4
        frequencies[12] = 523.25f * Mathf.Pow(1.05946f, octaveUpInt);  // C5                        
        frequencies[13] = 554.37f * Mathf.Pow(1.05946f, octaveUpInt);  // C#5
        frequencies[14] = 587.33f * Mathf.Pow(1.05946f, octaveUpInt);  // D5
        frequencies[15] = 622.25f * Mathf.Pow(1.05946f, octaveUpInt);  // D#5         
		Debug.Log ("Octave Up!");
	}    

    void OnAudioFilterRead(float[] data, int channels) {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels) {
            phase += increment;
            
            //sine
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            //sqaure
            // if (gain * Mathf.Sin((float)phase) >= 0 * gain) {
            //     data[i] = (float)gain * 0.6f;
            // }
            // else {
            //     data[i] = (-(float)gain) * 0.6f;
            // }

            //triangle
            //data[i] = (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));


            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }
    }
}      