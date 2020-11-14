using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPanel : MonoBehaviour
{
    void Awake () {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }  
    }   
}
