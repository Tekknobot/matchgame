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

	public GameObject clickedPad;

	public Sprite block;
	public Sprite blue;
	public Sprite green;
	public Sprite orange;
	public Sprite pink;
	public Sprite purple;
	public Sprite red;
	public Sprite turquoise;
	public Sprite yellow;

	public Sprite sample0;
	public Sprite sample1;
	public Sprite sample2;
	public Sprite sample3;
	public Sprite sample4;
	public Sprite sample5;
	public Sprite sample6;
	public Sprite sample7;
	public Sprite sample8;
	public Sprite sample9;
	public Sprite sample10;
	public Sprite sample11;
	public Sprite sample12;
	public Sprite sample13;
	public Sprite sample14;
	public Sprite sample15;

	int jFound;
	int kFound;

	private AudioSource audioSource;
	private AudioSource audioSourceChops;
	public AudioClip[] samples;
	public AudioClip[] chops;
	private AudioClip sampleClip;

	private AudioSource audioSourceOnTile;

	public AudioSource audioSource0;
	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public AudioSource audioSource3;
	public AudioSource audioSource4;
	public AudioSource audioSource5;
	public AudioSource audioSource6;
	public AudioSource audioSource7;

	public AudioSource chopSource;

	public float bpm;
	public float ms;

	private float kickVolume = 1f;
	private float snareVolume = 1f;
	private float cHatVolume = 1f;
	private float oHatVolume = 1f;
	private float clapVolume = 1f;
	private float crashVolume = 1f;
	private float rideVolume = 1f;
	private float rimVolume = 1f;
	private float chopsVolume = 1f;

	public SpriteRenderer[,] blockTiles;
	public SpriteRenderer[,] padTiles;

	bool hasCoroutineStarted = false;

	float lastbeatTime;
	float nextbeatTime;

	Dictionary<string, int> spriteClip = new Dictionary<string, int>() {
		{ "blue 0", 0 },
		{ "green  0", 1 },
		{ "orange 0", 2 },
		{ "pink 0", 3 },
		{ "purple 0", 4 },
		{ "red 0", 5 },
		{ "turquoise  0", 6 },
		{ "yellow  0", 7 },
	};

	Dictionary<string, int> chopClip = new Dictionary<string, int>() {
		{ "sample 0", 0 },
		{ "sample 1", 1 },
		{ "sample 2", 2 },
		{ "sample 3", 3 },
		{ "sample 4", 4 },
		{ "sample 5", 5 },
		{ "sample 6", 6 },
		{ "sample 7", 7 },
		{ "sample 8", 8 },
		{ "sample 9", 9 },
		{ "sample 10", 10 },
		{ "sample 11", 11 },
		{ "sample 12", 12 },
		{ "sample 13", 13 },
		{ "sample 14", 14 },
		{ "sample 15", 15 },
	};

	void Awake() {
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
    }

	void Start() {
		audioSource = SEQAudioManager.instance.GetComponent<AudioSource>();
		audioSourceChops = CHOPSAudioManager.instance.GetComponent<AudioSource>();
		audioSourceOnTile = gameObject.GetComponent<AudioSource>();

		bpm = GameObject.Find ("Slider").GetComponent<Slider>().value;

		blockTiles = new SpriteRenderer[OperatorManager.instance.xSize, OperatorManager.instance.ySize];
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
				blockTiles[x,y] = OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>();
			}
		}

		padTiles = new SpriteRenderer[PadManager.instance.xSize, PadManager.instance.ySize];
		for (int y = 0; y < PadManager.instance.ySize; y++) {
			for (int x = 0; x < PadManager.instance.xSize; x++) {
				padTiles[x,y] = PadManager.instance.tiles[x, y].GetComponent<SpriteRenderer>();
			}
		}

		StartCoroutine(TriggerWave());
    }

	void FixedUpdate() {
		bpm = GameObject.Find ("Slider").GetComponent<Slider>().value;
		GameObject.Find ("BPM").GetComponent<Text>().text = bpm.ToString();
		//GameObject.Find ("ms").GetComponent<Text>().text = ms.ToString();

		kickVolume = GameObject.Find ("Kick").GetComponent<Slider>().value;
		audioSource0.volume = kickVolume;

		snareVolume = GameObject.Find ("Snare").GetComponent<Slider>().value;
		audioSource1.volume = snareVolume;

		cHatVolume = GameObject.Find ("CHat").GetComponent<Slider>().value;
		audioSource2.volume = cHatVolume;

		oHatVolume = GameObject.Find ("OHat").GetComponent<Slider>().value;
		audioSource3.volume = oHatVolume;

		clapVolume = GameObject.Find ("Clap").GetComponent<Slider>().value;
		audioSource4.volume = clapVolume;

		crashVolume = GameObject.Find ("Crash").GetComponent<Slider>().value;
		audioSource5.volume = crashVolume;

		rideVolume = GameObject.Find ("Ride").GetComponent<Slider>().value;
		audioSource6.volume = rideVolume;

		rimVolume = GameObject.Find ("Rim").GetComponent<Slider>().value;
		audioSource7.volume = rimVolume;

		chopsVolume = GameObject.Find ("SampleSlider").GetComponent<Slider>().value;
		audioSourceChops.volume = chopsVolume;
	}

	private void Select() {
		if (render.sprite.name != "block 0") {
			DisplayActiveBoard();
		}
		else {
			return;
		}

		//isSelected = true;
		//render.color = selectedColor;
		previousSelected = GetComponent<OperatorTile>();

		if (spriteClip.ContainsKey(render.sprite.name)) {
			sampleClip = samples[spriteClip[render.sprite.name]];
			audioSource.clip = sampleClip;
			audioSource.Play();
		}

		if (chopClip.ContainsKey(render.sprite.name)) {
			sampleClip = chops[chopClip[render.sprite.name]];
			audioSourceChops.clip = sampleClip;
			audioSourceChops.Play();
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
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
            //Debug.Log (hitInfo.collider.gameObject.name);
			clickedPad = GameObject.Find(hitInfo.collider.gameObject.name);
        }

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "blue 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[0][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "green  0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[1][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "orange 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[2][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "pink 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[3][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "purple 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[4][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "red 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[5][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "turquoise  0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[6][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "yellow  0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.boards[7][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		/////

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 1") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 2") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 3") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 4") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 5") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 6") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 7") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 8") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 9") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 10") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 11") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 12") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 13") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 14") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "sample 15") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.pad[0][jFound, kFound] = null;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		////

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

	public void DisplayActiveBoard() {
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
				if (OperatorManager.instance.boards[0][x, y] == true && render.sprite.name == "blue 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = blue;
				}
				else if (OperatorManager.instance.boards[0][x, y] == false && render.sprite.name == "blue 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[1][x, y] == true && render.sprite.name == "green  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = green;
				}
				else if (OperatorManager.instance.boards[1][x, y] == false && render.sprite.name == "green  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[2][x, y] == true && render.sprite.name == "orange 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = orange;
				}
				else if (OperatorManager.instance.boards[2][x, y] == false && render.sprite.name == "orange 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[3][x, y] == true && render.sprite.name == "pink 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = pink;
				}
				else if (OperatorManager.instance.boards[3][x, y] == false && render.sprite.name == "pink 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[4][x, y] == true && render.sprite.name == "purple 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = purple;
				}
				else if (OperatorManager.instance.boards[4][x, y] == false && render.sprite.name == "purple 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[5][x, y] == true && render.sprite.name == "red 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = red;
				}
				else if (OperatorManager.instance.boards[5][x, y] == false && render.sprite.name == "red 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[6][x, y] == true && render.sprite.name == "turquoise  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = turquoise;
				}
				else if (OperatorManager.instance.boards[6][x, y] == false && render.sprite.name == "turquoise  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.boards[7][x, y] == true && render.sprite.name == "yellow  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = yellow;
				}
				else if (OperatorManager.instance.boards[7][x, y] == false && render.sprite.name == "yellow  0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				////

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 0" && OperatorManager.instance.pad[0][x,y] == "sample 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample0;
				}
				else if (render.sprite.name == "sample 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 1" && OperatorManager.instance.pad[0][x,y] == "sample 1") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample1;
				}
				else if (render.sprite.name == "sample 1") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 2" && OperatorManager.instance.pad[0][x,y] == "sample 2") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample2;
				}
				else if (render.sprite.name == "sample 2") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 3" && OperatorManager.instance.pad[0][x,y] == "sample 3") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample3;
				}
				else if (render.sprite.name == "sample 3") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 4" && OperatorManager.instance.pad[0][x,y] == "sample 4") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample4;
				}
				else if (render.sprite.name == "sample 4") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 5" && OperatorManager.instance.pad[0][x,y] == "sample 5") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample5;
				}
				else if (render.sprite.name == "sample 5") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 6" && OperatorManager.instance.pad[0][x,y] == "sample 6") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample6;
				}
				else if (render.sprite.name == "sample 6") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 7" && OperatorManager.instance.pad[0][x,y] == "sample 7") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample7;
				}
				else if (render.sprite.name == "sample 7") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 8" && OperatorManager.instance.pad[0][x,y] == "sample 8") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample8;
				}
				else if (render.sprite.name == "sample 8") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 9" && OperatorManager.instance.pad[0][x,y] == "sample 9") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample9;
				}
				else if (render.sprite.name == "sample 9") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 10" && OperatorManager.instance.pad[0][x,y] == "sample 10") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample10;
				}
				else if (render.sprite.name == "sample 10") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 11" && OperatorManager.instance.pad[0][x,y] == "sample 11") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample11;
				}
				else if (render.sprite.name == "sample 11") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 12" && OperatorManager.instance.pad[0][x,y] == "sample 12") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample12;
				}
				else if (render.sprite.name == "sample 12") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 13" && OperatorManager.instance.pad[0][x,y] == "sample 13") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample13;
				}
				else if (render.sprite.name == "sample 13") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 14" && OperatorManager.instance.pad[0][x,y] == "sample 14") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample14;
				}
				else if (render.sprite.name == "sample 14") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.chops[0][x, y] == true && render.sprite.name == "sample 15" && OperatorManager.instance.pad[0][x,y] == "sample 15") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = sample15;
				}
				else if (render.sprite.name == "sample 15") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}
			}
		}
	}

	public void CopySprite(SpriteRenderer render2) {
		if (render.sprite == render2.sprite) {
			return;
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "blue 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[0][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "green  0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[1][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "orange 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[2][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "pink 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[3][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "purple 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[4][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "red 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[5][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "turquoise  0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[6][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
		if (render.sprite.name == "block 0" && render2.sprite.name == "yellow  0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.boards[7][jFound,kFound] = true;
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		//Copy chop
		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 0";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 1") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 1";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 2") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 2";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 3") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 3";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 4") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 4";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 5") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 5";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 6") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 6";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 7") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 7";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 8") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 8";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 9") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 9";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 10") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 10";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 11") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 11";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 12") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 12";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 13") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 13";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 14") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 14";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "sample 15") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.chops[0][jFound,kFound] = true;
				OperatorManager.instance.pad[0][jFound,kFound] = "sample 15";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}
	}

	public IEnumerator TriggerWave() {
		while (true) {
			for (int y = 0; y < OperatorManager.instance.ySize; y++) {
				for (int x = 0; x < OperatorManager.instance.xSize; x++) {
					blockTiles[x,y].color = selectedColor;

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == true) {
						sampleClip = samples[0];
						audioSource0.clip = sampleClip;
						audioSource0.Play();
						padTiles[0,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[0,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[1][x, y] == true) {
						sampleClip = samples[1];
						audioSource1.clip = sampleClip;
						audioSource1.Play();
						padTiles[1,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[1,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[2][x, y] == true) {
						sampleClip = samples[2];
						audioSource2.clip = sampleClip;
						audioSource2.Play();
						padTiles[2,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[2,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[3][x, y] == true) {
						sampleClip = samples[3];
						audioSource3.clip = sampleClip;
						audioSource3.Play();
						padTiles[3,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[3,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[4][x, y] == true) {
						sampleClip = samples[4];
						audioSource4.clip = sampleClip;
						audioSource4.Play();
						padTiles[4,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[4,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[5][x, y] == true) {
						sampleClip = samples[5];
						audioSource5.clip = sampleClip;
						audioSource5.Play();
						padTiles[5,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[5,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[6][x, y] == true) {
						sampleClip = samples[6];
						audioSource6.clip = sampleClip;
						audioSource6.Play();
						padTiles[6,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[6,0].color = Color.white;
					}

					if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[7][x, y] == true) {
						sampleClip = samples[7];
						audioSource7.clip = sampleClip;
						audioSource7.Play();
						padTiles[7,0].color = selectedColor;
					}
					else if (gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
						padTiles[7,0].color = Color.white;
					}

					//Play chops
					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 0" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[0];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[0,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[0,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 1" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[1];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[1,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[1,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 2" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[2];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[2,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[2,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 3" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[3];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[3,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[3,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 4" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[4];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[4,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[4,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 5" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[5];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[5,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[5,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 6" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[6];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[6,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[6,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 7" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[7];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[7,1].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[7,1].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 8" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[8];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[0,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[0,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 9" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[9];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[1,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[1,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 10" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[10];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[2,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[2,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 11" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[11];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[3,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[3,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 12" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[12];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[4,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[4,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 13" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[13];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[5,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[5,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 14" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[14];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[6,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[6,2].color = Color.white;
					}

					if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 15" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						sampleClip = chops[15];
						audioSourceChops.clip = sampleClip;
						audioSourceChops.Play();
						padTiles[7,2].color = selectedColor;
					}
					else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
						padTiles[7,2].color = Color.white;
					}

					yield return StartCoroutine(Delay());

					UnTriggerWave();
				}
			}
		}
	}

	void UnTriggerWave() {
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
				OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	public IEnumerator Delay() {
		ms = 60 / bpm / 4;
		nextbeatTime += ms;
		yield return new WaitForSeconds(nextbeatTime - Time.time);
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
