using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OperatorTile : MonoBehaviour {
	public static OperatorTile instance;

	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static OperatorTile previousSelected = null;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	public SpriteRenderer render;
	public GameObject whiteParticles;

	public bool isSelected = false;

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

	public Sprite Note_0;
	public Sprite Note_1;
	public Sprite Note_2;
	public Sprite Note_3;
	public Sprite Note_4;
	public Sprite Note_5;
	public Sprite Note_6;
	public Sprite Note_7;
	public Sprite Note_8;
	public Sprite Note_9;
	public Sprite Note_10;
	public Sprite Note_11;
	public Sprite Note_12;
	public Sprite Note_13;
	public Sprite Note_14;
	public Sprite Note_15;	

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
	private float synthVolume = 1f;

	public SpriteRenderer[,] blockTiles;
	public SpriteRenderer[,] padTiles;
	public SpriteRenderer[,] noteTiles;

	bool hasCoroutineStarted = false;

	float nextbeatTime;

	private MusicPlayer player;

	public GameObject SynthSource;
	public GameObject SynthSourcePad;

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
		if (GameObject.FindGameObjectsWithTag("MusicPlayer").Length > 1) Destroy(GameObject.FindGameObjectWithTag("MusicPlayer"));
		player =  GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
    }

	void Start() {
		audioSource = SEQAudioManager.instance.GetComponent<AudioSource>();
		audioSourceChops = GameObject.Find("Spectrum").GetComponent<AudioSource>();
		audioSourceOnTile = gameObject.GetComponent<AudioSource>();

		bpm = GameObject.Find("Slider").GetComponent<Slider>().value;

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

		noteTiles = new SpriteRenderer[NoteManager.instance.xSize, NoteManager.instance.ySize];
		for (int y = 0; y < NoteManager.instance.ySize; y++) {
			for (int x = 0; x < NoteManager.instance.xSize; x++) {
				noteTiles[x,y] = NoteManager.instance.tiles[x, y].GetComponent<SpriteRenderer>();
			}
		}		
		
		StartCoroutine(TriggerWave());

		SynthSource = GameObject.Find("Synth");	
		SynthSourcePad = GameObject.Find("Synth2");	
			
    }

	void Update() {
		ms = 60 / bpm / 4;
		
		if (GameObject.Find ("Slider")) {
			bpm = GameObject.Find ("Slider").GetComponent<Slider>().value;
		}
		if (GameObject.Find ("BPM")) {
			GameObject.Find ("BPM").GetComponent<Text>().text = bpm.ToString();
		}	
		//GameObject.Find ("ms").GetComponent<Text>().text = ms.ToString();

		if (GameObject.Find ("Kick")) {		
			kickVolume = GameObject.Find ("Kick").GetComponent<Slider>().value;
			audioSource0.volume = kickVolume;
		}	

		if (GameObject.Find ("Snare")) {	
			snareVolume = GameObject.Find ("Snare").GetComponent<Slider>().value;
			audioSource1.volume = snareVolume;
		}	

		if (GameObject.Find ("CHat")) {	
			cHatVolume = GameObject.Find ("CHat").GetComponent<Slider>().value;
			audioSource2.volume = cHatVolume;
		}

		if (GameObject.Find ("OHat")) {
			oHatVolume = GameObject.Find ("OHat").GetComponent<Slider>().value;
			audioSource3.volume = oHatVolume;
		}

		if (GameObject.Find ("Clap")) {
			clapVolume = GameObject.Find ("Clap").GetComponent<Slider>().value;
			audioSource4.volume = clapVolume;
		}	

		if (GameObject.Find ("Crash")) {
			crashVolume = GameObject.Find ("Crash").GetComponent<Slider>().value;
			audioSource5.volume = crashVolume;
		}

		if (GameObject.Find ("Ride")) {
			rideVolume = GameObject.Find ("Ride").GetComponent<Slider>().value;
			audioSource6.volume = rideVolume;
		}	

		if (GameObject.Find ("Rim")) {
			rimVolume = GameObject.Find ("Rim").GetComponent<Slider>().value;
			audioSource7.volume = rimVolume;
		}

		if (GameObject.Find ("SampleSlider") && audioSourceChops != null) {
			chopsVolume = GameObject.Find ("SampleSlider").GetComponent<Slider>().value;
			audioSourceChops.volume = chopsVolume;
		}	

		if (GameObject.Find ("SynthVol") && SynthSource != null) {
			synthVolume = GameObject.Find ("SynthVol").GetComponent<Slider>().value;
			SynthSource.GetComponent<Oscillator>().volume = synthVolume;
		}			
	}

	private void Select() {
		if (render.sprite.name != "block 0") {
			DisplayActiveBoard();
		}
		else {
			return;
		}

		previousSelected = GetComponent<OperatorTile>();

		if (spriteClip.ContainsKey(render.sprite.name)) {
			sampleClip = samples[spriteClip[render.sprite.name]];
			audioSource.clip = sampleClip;
			audioSource.Play();
			render.color = selectedColor;
		}								

		//Chops
		if (render.sprite.name == "sample 0" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[0];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[1]-player.chopTime[0]));
			render.color = selectedColor;					
		}

		if (render.sprite.name == "sample 1" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[1];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[2]-player.chopTime[1]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 2" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[2];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[3]-player.chopTime[2]));	
			render.color = selectedColor;				
		}		

		if (render.sprite.name == "sample 3" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[3];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[4]-player.chopTime[3]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 4" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[4];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[5]-player.chopTime[4]));
			render.color = selectedColor;					
		}

		if (render.sprite.name == "sample 5" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[5];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[6]-player.chopTime[5]));
			render.color = selectedColor;					
		}

		if (render.sprite.name == "sample 6" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[6];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[7]-player.chopTime[6]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 7" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[7];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[8]-player.chopTime[7]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 8" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[8];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[9]-player.chopTime[8]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 9" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[9];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[10]-player.chopTime[9]));	
			render.color = selectedColor;				
		}	

		if (render.sprite.name == "sample 10" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[10];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[11]-player.chopTime[10]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 11" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[11];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[12]-player.chopTime[11]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 12" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[12];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[13]-player.chopTime[12]));	
			render.color = selectedColor;				
		}	

		if (render.sprite.name == "sample 13" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[13];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[14]-player.chopTime[13]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 14" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[14];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[15]-player.chopTime[14]));
			render.color = selectedColor;					
		}	

		if (render.sprite.name == "sample 15" && player.chopTime.Count > 16) {
			audioSourceChops.clip = player.song[0];
			audioSourceChops.time = player.chopTime[15];
			audioSourceChops.Play();
			audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[16]-player.chopTime[15]));
			render.color = selectedColor;					
		}																										

		// Notes
		if (render.sprite.name == "note 0") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[0];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;
			StartCoroutine(StopNote());						
		}

		if (render.sprite.name == "note 1") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[1];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 2") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[2];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;				
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 3") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[3];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 4") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[4];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 5") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[5];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 6") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[6];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 7") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[7];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 8") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[8];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 9") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[9];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 10") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[10];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 11") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[11];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 12") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[12];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 13") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[13];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 14") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[14];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 15") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[15];
			SynthSourcePad.GetComponent<Oscillator>().thisfreq = SynthSourcePad.GetComponent<Oscillator>().thisfreq % SynthSourcePad.GetComponent<Oscillator>().frequencies.Length;					
			StartCoroutine(StopNote());
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

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 0") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[0][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 1") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[1][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 2") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[2][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 3") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[3][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 4") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[4][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 5") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[5][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 6") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[6][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 7") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[7][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}		

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 8") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[8][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 9") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[9][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 10") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[10][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 11") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[11][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}		

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 12") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[12][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}		

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 13") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[13][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 14") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[14][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 15") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.note[15][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
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

				if (OperatorManager.instance.note[0][x, y] == true && render.sprite.name == "note 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_0;
				}
				else if (OperatorManager.instance.note[0][x, y] == false && render.sprite.name == "note 0") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}
				
				if (OperatorManager.instance.note[1][x, y] == true && render.sprite.name == "note 1") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_1;
				}
				else if (OperatorManager.instance.note[1][x, y] == false && render.sprite.name == "note 1") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[2][x, y] == true && render.sprite.name == "note 2") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_2;
				}
				else if (OperatorManager.instance.note[2][x, y] == false && render.sprite.name == "note 2") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[3][x, y] == true && render.sprite.name == "note 3") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_3;
				}
				else if (OperatorManager.instance.note[3][x, y] == false && render.sprite.name == "note 3") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[4][x, y] == true && render.sprite.name == "note 4") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_4;
				}
				else if (OperatorManager.instance.note[4][x, y] == false && render.sprite.name == "note 4") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}														

				if (OperatorManager.instance.note[5][x, y] == true && render.sprite.name == "note 5") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_5;
				}
				else if (OperatorManager.instance.note[5][x, y] == false && render.sprite.name == "note 5") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[6][x, y] == true && render.sprite.name == "note 6") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_6;
				}
				else if (OperatorManager.instance.note[6][x, y] == false && render.sprite.name == "note 6") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[7][x, y] == true && render.sprite.name == "note 7") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_7;
				}
				else if (OperatorManager.instance.note[7][x, y] == false && render.sprite.name == "note 7") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[8][x, y] == true && render.sprite.name == "note 8") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_8;
				}
				else if (OperatorManager.instance.note[8][x, y] == false && render.sprite.name == "note 8") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.note[9][x, y] == true && render.sprite.name == "note 9") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_9;
				}
				else if (OperatorManager.instance.note[9][x, y] == false && render.sprite.name == "note 9") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[10][x, y] == true && render.sprite.name == "note 10") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_10;
				}
				else if (OperatorManager.instance.note[10][x, y] == false && render.sprite.name == "note 10") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[11][x, y] == true && render.sprite.name == "note 11") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_11;
				}
				else if (OperatorManager.instance.note[11][x, y] == false && render.sprite.name == "note 11") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[12][x, y] == true && render.sprite.name == "note 12") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_12;
				}
				else if (OperatorManager.instance.note[12][x, y] == false && render.sprite.name == "note 12") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[13][x, y] == true && render.sprite.name == "note 13") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_13;
				}
				else if (OperatorManager.instance.note[13][x, y] == false && render.sprite.name == "note 13") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.note[14][x, y] == true && render.sprite.name == "note 14") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_14;
				}
				else if (OperatorManager.instance.note[14][x, y] == false && render.sprite.name == "note 14") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}			

				if (OperatorManager.instance.note[15][x, y] == true && render.sprite.name == "note 15") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_15;
				}
				else if (OperatorManager.instance.note[15][x, y] == false && render.sprite.name == "note 15") {
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

		// Copy Note
		if (render.sprite.name == "block 0" && render2.sprite.name == "note 0") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[0][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 0";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 1") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[1][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 1";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}		

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 2") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[2][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 2";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 3") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[3][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 3";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 4") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[4][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 4";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 5") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[5][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 5";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 6") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[6][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 6";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 7") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[7][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 7";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 8") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[8][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 8";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 9") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[9][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 9";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 10") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[10][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 10";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 11") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[11][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 11";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}					

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 12") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[12][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 12";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 13") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[13][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 13";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}		

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 14") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[14][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 14";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 15") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.note[15][jFound,kFound] = true;
				OperatorManager.instance.notePads[0][jFound,kFound] = "note 15";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}		
	}

	public IEnumerator TriggerWave() {
		while (true) {
			if (!hasCoroutineStarted) {
				for (int y = 0; y < OperatorManager.instance.ySize; y++) {
					for (int x = 0; x < OperatorManager.instance.xSize; x++) {
						blockTiles[x,y].color = selectedColor;

						// Play drum samples
						if (padTiles[0,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == true) {
							sampleClip = samples[0];
							audioSource0.clip = sampleClip;
							audioSource0.Play();
							padTiles[0,0].color = selectedColor;
						}
						else if (padTiles[0,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[0,0].color = Color.white;
						}

						if (padTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[1][x, y] == true) {
							sampleClip = samples[1];
							audioSource1.clip = sampleClip;
							audioSource1.Play();
							padTiles[1,0].color = selectedColor;
						}
						else if (padTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[1,0].color = Color.white;
						}

						if (padTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[2][x, y] == true) {
							sampleClip = samples[2];
							audioSource2.clip = sampleClip;
							audioSource2.Play();
							padTiles[2,0].color = selectedColor;
						}
						else if (padTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[2,0].color = Color.white;
						}

						if (padTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[3][x, y] == true) {
							sampleClip = samples[3];
							audioSource3.clip = sampleClip;
							audioSource3.Play();
							padTiles[3,0].color = selectedColor;
						}
						else if (padTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[3,0].color = Color.white;
						}

						if (padTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[4][x, y] == true) {
							sampleClip = samples[4];
							audioSource4.clip = sampleClip;
							audioSource4.Play();
							padTiles[4,0].color = selectedColor;
						}
						else if (padTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[4,0].color = Color.white;
						}

						if (padTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[5][x, y] == true) {
							sampleClip = samples[5];
							audioSource5.clip = sampleClip;
							audioSource5.Play();
							padTiles[5,0].color = selectedColor;
						}
						else if (padTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[5,0].color = Color.white;
						}

						if (padTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[6][x, y] == true) {
							sampleClip = samples[6];
							audioSource6.clip = sampleClip;
							audioSource6.Play();
							padTiles[6,0].color = selectedColor;
						}
						else if (padTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[6,0].color = Color.white;
						}

						if (padTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[7][x, y] == true) {
							sampleClip = samples[7];
							audioSource7.clip = sampleClip;
							audioSource7.Play();
							padTiles[7,0].color = selectedColor;
						}
						else if (padTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.boards[0][x, y] == false) {
							padTiles[7,0].color = Color.white;
						}

						//Play chops
						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 0" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[0];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[1]-player.chopTime[0]));
							padTiles[0,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[0,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 1" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[1];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[2]-player.chopTime[1]));	
							padTiles[1,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[1,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 2" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[2];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[3]-player.chopTime[2]));
							padTiles[2,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[2,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 3" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[3];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[4]-player.chopTime[3]));
							padTiles[3,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[3,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 4" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[4];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[5]-player.chopTime[4]));
							padTiles[4,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[4,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 5" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[5];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[6]-player.chopTime[5]));
							padTiles[5,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[5,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 6" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[6];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[7]-player.chopTime[6]));
							padTiles[6,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[6,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 7" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[7];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[8]-player.chopTime[7]));
							padTiles[7,1].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[7,1].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 8" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[8];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[9]-player.chopTime[8]));
							padTiles[0,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[0,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 9" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[9];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[10]-player.chopTime[9]));
							padTiles[1,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[1,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 10" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[10];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[11]-player.chopTime[10]));
							padTiles[2,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[2,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 11" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[11];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[12]-player.chopTime[11]));
							padTiles[3,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[3,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 12" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[12];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[13]-player.chopTime[12]));
							padTiles[4,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[4,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 13" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[13];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[14]-player.chopTime[13]));
							padTiles[5,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[5,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 14" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[14];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[15]-player.chopTime[14]));
							padTiles[6,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[6,2].color = Color.white;
						}

						if (OperatorManager.instance.pad[0][jFound,kFound] == "sample 15" && OperatorManager.instance.chops[0][x, y] == true && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							audioSourceChops.clip = player.song[player.currentIndex];
							audioSourceChops.time = player.chopTime[15];
							audioSourceChops.Play();
							audioSourceChops.SetScheduledEndTime(AudioSettings.dspTime+(player.chopTime[16]-player.chopTime[15]));
							padTiles[7,2].color = selectedColor;
						}
						else if (OperatorManager.instance.chops[0][x, y] == false && gameObject.name == OperatorManager.instance.tiles[x, y].name) {
							padTiles[7,2].color = Color.white;
						}										


						// Play synth
						if (noteTiles[0,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[0][x, y] == true) {
                            SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[0];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[0,0].color = selectedColor;
						}
						else if (noteTiles[0,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[0][x, y] == false) {
							noteTiles[0,0].color = Color.white;
							SynthSource.GetComponent<Oscillator>().gain = 0;
						}

						if (noteTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[1][x, y] == true) {
                            SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[1];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[1,0].color = selectedColor;
						}
						else if (noteTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[1][x, y] == false) {
							noteTiles[1,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}			

						if (noteTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[2][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[2];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[2,0].color = selectedColor;
						}
						else if (noteTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[2][x, y] == false) {
							noteTiles[2,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[3][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[3];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[3,0].color = selectedColor;
						}
						else if (noteTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[3][x, y] == false) {
							noteTiles[3,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[4][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[4];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[4,0].color = selectedColor;
						}
						else if (noteTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[4][x, y] == false) {
							noteTiles[4,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[5][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[5];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[5,0].color = selectedColor;
						}
						else if (noteTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[5][x, y] == false) {
							noteTiles[5,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[6][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[6];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[6,0].color = selectedColor;
						}
						else if (noteTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[6][x, y] == false) {
							noteTiles[6,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}

						if (noteTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[7][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[7];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[7,0].color = selectedColor;
						}
						else if (noteTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[7][x, y] == false) {
							noteTiles[7,0].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[0,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[8][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[8];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[0,1].color = selectedColor;
						}
						else if (noteTiles[0,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[8][x, y] == false) {
							noteTiles[0,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[1,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[9][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[9];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[1,1].color = selectedColor;
						}
						else if (noteTiles[1,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[9][x, y] == false) {
							noteTiles[1,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[2,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[10][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[10];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[2,1].color = selectedColor;
						}
						else if (noteTiles[2,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[10][x, y] == false) {
							noteTiles[2,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[3,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[11][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[11];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[3,1].color = selectedColor;
						}
						else if (noteTiles[3,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[11][x, y] == false) {
							noteTiles[3,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[4,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[12][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[12];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[4,1].color = selectedColor;
						}
						else if (noteTiles[4,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[12][x, y] == false) {
							noteTiles[4,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}		

						if (noteTiles[5,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[13][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[13];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[5,1].color = selectedColor;
						}
						else if (noteTiles[5,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[13][x, y] == false) {
							noteTiles[5,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[6,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[14][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[14];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[6,1].color = selectedColor;
						}
						else if (noteTiles[6,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[14][x, y] == false) {
							noteTiles[6,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}			

						if (noteTiles[7,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[15][x, y] == true) {
							SynthSource.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource.GetComponent<Oscillator>().frequency = SynthSource.GetComponent<Oscillator>().frequencies[15];
							SynthSource.GetComponent<Oscillator>().thisfreq = SynthSource.GetComponent<Oscillator>().thisfreq % SynthSource.GetComponent<Oscillator>().frequencies.Length;	
							noteTiles[7,1].color = selectedColor;
						}
						else if (noteTiles[7,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[15][x, y] == false) {
							noteTiles[7,1].color = Color.white;
							//SynthSource.GetComponent<Oscillator>().gain = 0;
						}																																										
																																										

						yield return StartCoroutine(Delay());

						hasCoroutineStarted = true;
						UnTriggerWave();
					}
				}
			}
		hasCoroutineStarted = false;
		}
	}

	void UnTriggerWave() {
		for (int y = 0; y < OperatorManager.instance.ySize; y++) {
			for (int x = 0; x < OperatorManager.instance.xSize; x++) {
				OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	public void StartWave() {
		Debug.Log("Active? "+ gameObject.activeInHierarchy);
		StartCoroutine(TriggerWave());
	}

	public IEnumerator Delay() {
		nextbeatTime += ms;
		yield return new WaitForSeconds(nextbeatTime - Time.timeSinceLevelLoad);
	}


	IEnumerator StopShakingCamera() {
		yield return new WaitForSeconds(0.1f);
		mainCamera.GetComponent<CameraShake>().stopshakingcamera();
	}

	IEnumerator resetflipTile() {
		yield return new WaitForSeconds(1f);
		GetComponent<RotateYaxis>().resetflipTile();
	}	

	IEnumerator StopNote() {
		yield return new WaitForSeconds(1f);
		SynthSourcePad.GetComponent<Oscillator>().gain = 0;
	}		
}
