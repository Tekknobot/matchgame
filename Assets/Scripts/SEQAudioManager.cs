using UnityEngine;

public class SEQAudioManager : MonoBehaviour {
	public static SEQAudioManager instance;

	// Use this for initialization
	void Start () {
		instance = GetComponent<SEQAudioManager>();
    }	
}
