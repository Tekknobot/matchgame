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

	public Sprite block;

	private AudioSource audioSource;
	public AudioClip[] samples;
	private AudioClip sampleClip;

	private AudioSource audioSourceOnTile;	

	Dictionary<string, int> spriteClip = new Dictionary<string, int>() {
		{ "blue 0", 0 },
		{ "green  0", 1 },
		{ "orange 0", 2 },
		{ "pink 0", 3 },
		{ "purple 0", 4 },
		{ "red 0", 5 },
		{ "turquoise  0", 6 },
		{ "yellow  0", 7 },		

		{ "sample 0", 8 },
		{ "sample 1", 9 },
		{ "sample 2", 10 },
		{ "sample 3", 11 },
		{ "sample 4", 12 },
		{ "sample 5", 13 },
		{ "sample 6", 14 },
		{ "sample 7", 15 },	
		{ "sample 8", 16 },
		{ "sample 9", 17 },
		{ "sample 10", 18 },
		{ "sample 11", 19 },
		{ "sample 12", 20 },
		{ "sample 13", 21 },
		{ "sample 14", 22 },
		{ "sample 15", 23 },					
	};

	void Awake() {
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
    }

	void Start() {
		audioSource = SEQAudioManager.instance.GetComponent<AudioSource>();
		audioSourceOnTile = gameObject.GetComponent<AudioSource>();
		StartCoroutine(TriggerWave());
    }

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = GetComponent<OperatorTile>();	

		if (spriteClip.ContainsKey(render.sprite.name)) {
			//SFXManager.instance.PlaySFX(spriteClip[render.sprite.name]);
			sampleClip = samples[spriteClip[render.sprite.name]];
			audioSource.clip = sampleClip;
			audioSource.Play();			
		}

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

					if (spriteClip.ContainsKey(OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name) && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						//SFXManager.instance.PlaySFX(spriteClip[OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name]);
						sampleClip = samples[spriteClip[OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite.name]];
						audioSourceOnTile.clip = sampleClip;
						audioSourceOnTile.Play();						
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
					yield return StartCoroutine(Delay());
			}
		}		
	}		

	IEnumerator Delay() {
		yield return new WaitForSeconds(0.178575f);
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