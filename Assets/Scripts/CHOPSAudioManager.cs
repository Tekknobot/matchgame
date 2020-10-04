using UnityEngine;

public class CHOPSAudioManager : MonoBehaviour {
	public static CHOPSAudioManager instance;
	
	// Use this for initialization
	void Start () {
		instance = GetComponent<CHOPSAudioManager>();
    }	
}
