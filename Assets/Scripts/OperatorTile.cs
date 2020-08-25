using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorTile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static OperatorTile previousSelected = null;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private SpriteRenderer render;
	public GameObject whiteParticles;

	private bool isSelected = false;

	Camera mainCamera;

	private AudioSource audioSource;
	public AudioClip[] samples;
	private AudioClip sampleClips;	

	public Sprite block;

	//public List<GameObject> padManagers = new List<GameObject>();

	void Awake() {
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
    }

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();
		StartCoroutine(TriggerWave());
    }	

	void Update() {
		
	}

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = GetComponent<OperatorTile>();

		Debug.Log(render.sprite.name);
		if (render.sprite.name == "blue 0") {
			SFXManager.instance.PlaySFX(Clip.Select);
		}
		if (render.sprite.name == "green  0") {
			SFXManager.instance.PlaySFX(Clip.Swap);
		}	
		if (render.sprite.name == "orange 0") {
			SFXManager.instance.PlaySFX(Clip.HatC);
		}
		if (render.sprite.name == "pink 0") {
			SFXManager.instance.PlaySFX(Clip.HatO);
		}
		if (render.sprite.name == "purple 0") {
			SFXManager.instance.PlaySFX(Clip.Clap);
		}	
		if (render.sprite.name == "red 0") {
			SFXManager.instance.PlaySFX(Clip.Crash);
		}
		if (render.sprite.name == "turquoise  0") {
			SFXManager.instance.PlaySFX(Clip.Ride);
		}	
		if (render.sprite.name == "yellow  0") {
			SFXManager.instance.PlaySFX(Clip.Rim);
		}										

		//SFXManager.instance.PlaySFX(Clip.Select);
		mainCamera.GetComponent<CameraShake>().shakecamera();
		StartCoroutine(StopShakingCamera());
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown() {
		if (render.tag == "blocks" && render.color == Color.white) {
			render.sprite = block;
		}

		if (render.sprite == null) {
		 	return;
		}

		if (isSelected) { 
			Deselect();
		} else {
			if (previousSelected == null) {
				Select();
			} else {
				if (render.tag == "pads") {
					previousSelected.GetComponent<OperatorTile>().Deselect();
					Select();
				}

				if (render.tag == "blocks") {
					CopySprite(previousSelected.render);
					GetComponent<RotateYaxis>().flipTile();
					StartCoroutine(resetflipTile());
					Instantiate(whiteParticles, transform.position, Quaternion.identity);
					//Select();
				}							
			}
		}
	}

	public void CopySprite(SpriteRenderer render2) { 
		if (render.sprite == render2.sprite) { 
			return;
		}

		if (render.sprite.name == "block 0") {
			Sprite tempSprite = render2.sprite;
			render.sprite = tempSprite;
			//SFXManager.instance.PlaySFX(Clip.Swap2);
			previousSelected.GetComponent<OperatorTile>().Deselect();
			Select(); 
		}	
	}	

	public IEnumerator TriggerWave() {
		while (true)
		{
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = selectedColor;

					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "blue 0") {
						SFXManager.instance.PlaySFX(Clip.Select);
					}
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "green  0") {
						SFXManager.instance.PlaySFX(Clip.Swap);
					}	
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "orange 0") {
						SFXManager.instance.PlaySFX(Clip.HatC);
					}
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "pink 0") {
						SFXManager.instance.PlaySFX(Clip.HatO);
					}
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "purple 0") {
						SFXManager.instance.PlaySFX(Clip.Clap);
					}	
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "red 0") {
						SFXManager.instance.PlaySFX(Clip.Crash);
					}
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "turquoise  0") {
						SFXManager.instance.PlaySFX(Clip.Ride);
					}	
					if (OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name == "yellow  0") {
						SFXManager.instance.PlaySFX(Clip.Rim);
					}

					yield return StartCoroutine(Delay());
					StartCoroutine(UnTriggerWave());
			}
		}
		yield return new WaitForSeconds(0f);
		}		
	}		

	public IEnumerator UnTriggerWave() {
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
					//Instantiate(whiteParticles, BoardManager.instance.tiles[x, y].transform.position, Quaternion.identity);
					yield return StartCoroutine(Delay());
			}
		}		
	}		

	IEnumerator Delay() {
		yield return new WaitForSeconds(0.125f);
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