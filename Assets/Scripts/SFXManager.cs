using UnityEngine;

public enum Clip { Music, Select, Swap, Clear4, Clear5, Clear6, Clear7, Scratch};

public class SFXManager : MonoBehaviour {
	public static SFXManager instance;

	private AudioSource[] sfx;

	// Use this for initialization
	void Start () {
		instance = GetComponent<SFXManager>();
		sfx = GetComponents<AudioSource>();
    }

	public void PlaySFX(Clip audioClip) {
		sfx[(int)audioClip].Play();
	}

	public void StopSFX(Clip audioClip) {
		sfx[(int)audioClip].Stop();
	}	
}
