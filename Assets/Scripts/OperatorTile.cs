using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OperatorTile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static OperatorTile previousSelected = null;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private SpriteRenderer render;
	public GameObject whiteParticles;

	private bool isSelected = false;

	Camera mainCamera;	

	public Sprite block;
	int jFound;
	int kFound;	

	private AudioSource audioSource;
	public AudioClip[] samples;
	private AudioClip sampleClip;

	private AudioSource audioSourceOnTile;
	
	public float bpm;
	public float ms;

	public int numBeatsPerSegment = 16;
	private static double time;
	private double nextEventTime;

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

		bpm = GameObject.Find ("Slider").GetComponent<Slider>().value;

		nextEventTime = AudioSettings.dspTime + 2.0f;

		StartCoroutine(TriggerWave());
		
    }

	void Update() {
		bpm = GameObject.Find ("Slider").GetComponent<Slider>().value;	
		GameObject.Find ("BPM").GetComponent<Text>().text = bpm.ToString();

		time = AudioSettings.dspTime;
	}	

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = GetComponent<OperatorTile>();	

		if (spriteClip.ContainsKey(render.sprite.name)) {
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

	bool FindIndicesOfObject(GameObject objectToLookFor, out int j, out int k)
	{
		for (j = 0; j < OperatorManager.instance.boards.Length; j++)
		{
			for (k = 0; k < OperatorManager.instance.boards.Length; k++)
			{
				// Is this the one?
				if (OperatorManager.instance.tiles[j,k] == objectToLookFor)
				{
					return true;
				}
			}
		}
		j = k = -1;
		return false;
	}

	public void CopySprite(SpriteRenderer render2) { 
		if (render.sprite == render2.sprite) { 
			return;
		}

		if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {	
			if (render.sprite.name == "block 0" && render2.sprite.name == "blue 0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}
			if (render.sprite.name == "block 0" && render2.sprite.name == "green  0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}			
			if (render.sprite.name == "block 0" && render2.sprite.name == "orange 0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}
			if (render.sprite.name == "block 0" && render2.sprite.name == "pink 0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}	
			if (render.sprite.name == "block 0" && render2.sprite.name == "purple 0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}	
			if (render.sprite.name == "block 0" && render2.sprite.name == "red 0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}	
			if (render.sprite.name == "block 0" && render2.sprite.name == "turquoise  0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}	
			if (render.sprite.name == "block 0" && render2.sprite.name == "yellow  0") {
				Sprite tempSprite = render2.sprite;
				OperatorManager.instance.boards[spriteClip[render2.sprite.name]][jFound,kFound].GetComponent<SpriteRenderer>().sprite = tempSprite;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select(); 
			}														
		}
	}	

	public IEnumerator TriggerWave() {
		while (true)
		{
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
					OperatorManager.instance.boards[x][x, y].GetComponent<SpriteRenderer>().color = selectedColor;

					if (spriteClip.ContainsKey(OperatorManager.instance.boards[x][x, y].GetComponent<SpriteRenderer>().sprite.name) && gameObject.name == OperatorManager.instance.boards[x][x, y].name) {
						sampleClip = samples[spriteClip[OperatorManager.instance.boards[x][x, y].GetComponent<SpriteRenderer>().sprite.name]];
						audioSourceOnTile.clip = sampleClip;
						if (time + 1.0f > nextEventTime) {
							audioSourceOnTile.PlayScheduled(nextEventTime);	
							nextEventTime += 60.0f / bpm * numBeatsPerSegment;
						}					
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

    public void SetBPM(float bpmValue)
    {
        bpm = bpmValue;
    }	

	public IEnumerator Delay() {
		ms = 60000 / bpm / 1000 / 4;
		yield return new WaitForSeconds(ms);
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