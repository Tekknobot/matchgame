using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadTile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static PadTile previousSelected = null;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private SpriteRenderer render;
	public GameObject whiteParticles;

	private bool isSelected = false;

	Camera mainCamera;

	private AudioSource audioSource;
	public AudioClip[] samples;
	private AudioClip sampleClips;	

	void Awake() {
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
    }

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();
    }	

	void Update() {
		
	}

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = GetComponent<PadTile>();

		Debug.Log(render.sprite.name);
		if (render.sprite.name == "blue 0") {
			//Instantiate(padManagers[0], spawnPoint, Quaternion.identity);
			Debug.Log("Fired!");
		}


		SFXManager.instance.PlaySFX(Clip.Select);
		mainCamera.GetComponent<CameraShake>().shakecamera();
		StartCoroutine(StopShakingCamera());
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown() {
		if (render.sprite == null) {
		 	return;
		}

		if (isSelected) { 
			Deselect();
		} else {
			if (previousSelected == null) {
				Select();
			} else {
				CopySprite(previousSelected.render);
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				previousSelected.GetComponent<PadTile>().Deselect();				
			}
		}
	}

	public void CopySprite(SpriteRenderer render2) { // 1
		if (render.sprite == render2.sprite) { // 2
			return;
		}

		Sprite tempSprite = render2.sprite; // 3
		//render2.sprite = render.sprite; // 4
		render.sprite = tempSprite; // 5
		SFXManager.instance.PlaySFX(Clip.Swap); // 6
	}		

	IEnumerator StopShakingCamera() {
		yield return new WaitForSeconds(0.1f);
		mainCamera.GetComponent<CameraShake>().stopshakingcamera();
	}

	IEnumerator resetflipTile() {
		yield return new WaitForSeconds(1f);
		GetComponent<RotateYaxis>().resetflipTile();
	}
}