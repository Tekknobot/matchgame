using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour {

    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 10;

    Camera _camera;
    void Start() {
        _camera = GetComponent<Camera>();
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update() {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        //GetComponent<Camera>().orthographicSize = desiredHalfHeight;

        //9:19 aspect ratio
        if (Camera.main.aspect >= 0.4736) 
            Camera.main.orthographicSize = 9.221814f;

        // 10:16 aspect ratio
        if (Camera.main.aspect >= 0.62)
            Camera.main.orthographicSize = 7.1f; 

        // 9:16 aspect ratio
        if (Camera.main.aspect >= 0.5625)
            Camera.main.orthographicSize = 9.333333f;                   
    }
}
