using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMsynth : MonoBehaviour
{
    [Range(50, 1000)]
    public float frequency1;
    [Range(0, 1)]
    public float amplitude1;

    [Range(0, 30)]
    public float frequency2;
    [Range(0, .1f)]
    public float amplitude2;

    int timeIndex = 0;

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            float FMfreq = frequency1 * CreateSine(timeIndex, frequency2, 48000, amplitude2);
            data[i] = CreateSine(timeIndex, FMfreq, 48000, amplitude1);

            if (channels == 2)
                data[i + 1] = data[i];

            timeIndex++;
        }
    }

        //Creates a sinewave
        public float CreateSine(int timeIndex, float frequency, float sampleRate, float amplitude)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate) * amplitude;
    }

}
