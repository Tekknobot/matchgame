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

	public Sprite Note_16;
	public Sprite Note_17;
	public Sprite Note_18;
	public Sprite Note_19;
	public Sprite Note_20;
	public Sprite Note_21;
	public Sprite Note_22;
	public Sprite Note_23;
	public Sprite Note_24;
	public Sprite Note_25;
	public Sprite Note_26;
	public Sprite Note_27;
	public Sprite Note_28;
	public Sprite Note_29;
	public Sprite Note_30;
	public Sprite Note_31;

	public Sprite Note_32;
	public Sprite Note_33;
	public Sprite Note_34;
	public Sprite Note_35;
	public Sprite Note_36;
	public Sprite Note_37;
	public Sprite Note_38;
	public Sprite Note_39;
	public Sprite Note_40;
	public Sprite Note_41;
	public Sprite Note_42;
	public Sprite Note_43;
	public Sprite Note_44;
	public Sprite Note_45;
	public Sprite Note_46;
	public Sprite Note_47;		

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
	public SpriteRenderer[,] noteTilesDown;
	public SpriteRenderer[,] noteTilesUp;

	bool hasCoroutineStarted = false;

	float nextbeatTime;

	private MusicPlayer player;

	public GameObject SynthSource_0;
	public GameObject SynthSource_1;
	public GameObject SynthSource_2;
	public GameObject SynthSource_3;
	public GameObject SynthSource_4;
	public GameObject SynthSource_5;
	public GameObject SynthSource_6;
	public GameObject SynthSource_7;
	public GameObject SynthSource_8;
	public GameObject SynthSource_9;
	public GameObject SynthSource_10;
	public GameObject SynthSource_11;
	public GameObject SynthSource_12;
	public GameObject SynthSource_13;
	public GameObject SynthSource_14;
	public GameObject SynthSource_15;

	public GameObject SynthSource_16;
	public GameObject SynthSource_17;
	public GameObject SynthSource_18;
	public GameObject SynthSource_19;
	public GameObject SynthSource_20;
	public GameObject SynthSource_21;
	public GameObject SynthSource_22;
	public GameObject SynthSource_23;
	public GameObject SynthSource_24;
	public GameObject SynthSource_25;
	public GameObject SynthSource_26;
	public GameObject SynthSource_27;
	public GameObject SynthSource_28;
	public GameObject SynthSource_29;
	public GameObject SynthSource_30;
	public GameObject SynthSource_31;

	public GameObject SynthSource_32;
	public GameObject SynthSource_33;
	public GameObject SynthSource_34;
	public GameObject SynthSource_35;
	public GameObject SynthSource_36;
	public GameObject SynthSource_37;
	public GameObject SynthSource_38;
	public GameObject SynthSource_39;
	public GameObject SynthSource_40;
	public GameObject SynthSource_41;
	public GameObject SynthSource_42;
	public GameObject SynthSource_43;
	public GameObject SynthSource_44;
	public GameObject SynthSource_45;
	public GameObject SynthSource_46;
	public GameObject SynthSource_47;		

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

		noteTilesDown = new SpriteRenderer[NoteManagerDown.instance.xSize, NoteManagerDown.instance.ySize];
		for (int y = 0; y < NoteManagerDown.instance.ySize; y++) {
			for (int x = 0; x < NoteManagerDown.instance.xSize; x++) {
				noteTilesDown[x,y] = NoteManagerDown.instance.tiles[x, y].GetComponent<SpriteRenderer>();
			}
		}			
		
		StartCoroutine(TriggerWave());

		SynthSource_0 = GameObject.Find("SynthSource_0");
		SynthSource_1 = GameObject.Find("SynthSource_1");
		SynthSource_2 = GameObject.Find("SynthSource_2");
		SynthSource_3 = GameObject.Find("SynthSource_3");
		SynthSource_4 = GameObject.Find("SynthSource_4");	
		SynthSource_5 = GameObject.Find("SynthSource_5");
		SynthSource_6 = GameObject.Find("SynthSource_6");
		SynthSource_7 = GameObject.Find("SynthSource_7");
		SynthSource_8 = GameObject.Find("SynthSource_8");
		SynthSource_9 = GameObject.Find("SynthSource_9");
		SynthSource_10 = GameObject.Find("SynthSource_10");
		SynthSource_11 = GameObject.Find("SynthSource_11");
		SynthSource_12 = GameObject.Find("SynthSource_12");
		SynthSource_13 = GameObject.Find("SynthSource_13");
		SynthSource_14 = GameObject.Find("SynthSource_14");
		SynthSource_15 = GameObject.Find("SynthSource_15");

		SynthSource_16 = GameObject.Find("SynthSource_16");
		SynthSource_17 = GameObject.Find("SynthSource_17");
		SynthSource_18 = GameObject.Find("SynthSource_18");
		SynthSource_19 = GameObject.Find("SynthSource_19");
		SynthSource_20 = GameObject.Find("SynthSource_20");	
		SynthSource_21 = GameObject.Find("SynthSource_21");
		SynthSource_22 = GameObject.Find("SynthSource_22");
		SynthSource_23 = GameObject.Find("SynthSource_23");
		SynthSource_24 = GameObject.Find("SynthSource_24");
		SynthSource_25 = GameObject.Find("SynthSource_25");
		SynthSource_26 = GameObject.Find("SynthSource_26");
		SynthSource_27 = GameObject.Find("SynthSource_27");
		SynthSource_28 = GameObject.Find("SynthSource_28");
		SynthSource_29 = GameObject.Find("SynthSource_29");
		SynthSource_30 = GameObject.Find("SynthSource_30");
		SynthSource_31 = GameObject.Find("SynthSource_31");

		SynthSource_32 = GameObject.Find("SynthSource_32");
		SynthSource_33 = GameObject.Find("SynthSource_33");
		SynthSource_34 = GameObject.Find("SynthSource_34");
		SynthSource_35 = GameObject.Find("SynthSource_35");
		SynthSource_36 = GameObject.Find("SynthSource_36");	
		SynthSource_37 = GameObject.Find("SynthSource_37");
		SynthSource_38 = GameObject.Find("SynthSource_38");
		SynthSource_39 = GameObject.Find("SynthSource_39");
		SynthSource_40 = GameObject.Find("SynthSource_40");
		SynthSource_41 = GameObject.Find("SynthSource_41");
		SynthSource_42 = GameObject.Find("SynthSource_42");
		SynthSource_43 = GameObject.Find("SynthSource_43");
		SynthSource_44 = GameObject.Find("SynthSource_44");
		SynthSource_45 = GameObject.Find("SynthSource_45");
		SynthSource_46 = GameObject.Find("SynthSource_46");
		SynthSource_47 = GameObject.Find("SynthSource_47");				

		SynthSourcePad = GameObject.Find("SynthPads");	
			
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

		if (GameObject.Find ("SynthVol") && SynthSource_0 != null) {
			synthVolume = GameObject.Find ("SynthVol").GetComponent<Slider>().value;
			SynthSource_0.GetComponent<Oscillator>().volume = synthVolume;
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
			StartCoroutine(StopNote());						
		}

		if (render.sprite.name == "note 1") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[1];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 2") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[2];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 3") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[3];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 4") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[4];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 5") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[5];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 6") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[6];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 7") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[7];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 8") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[8];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 9") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[9];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 10") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[10];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 11") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[11];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 12") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[12];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 13") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[13];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 14") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[14];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 15") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[15];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 16") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[16];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 17") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[17];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 18") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[18];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 19") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[19];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 20") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[20];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 21") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[21];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 22") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[22];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 23") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[23];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 24") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[24];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 25") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[25];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 26") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[26];
			StartCoroutine(StopNote());
		}			

		if (render.sprite.name == "note 27") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[27];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 28") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[28];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 29") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[29];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 30") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[30];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 31") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[31];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 32") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[32];
			StartCoroutine(StopNote());
		}																															

		if (render.sprite.name == "note 33") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[33];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 34") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[34];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 35") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[35];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 36") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[36];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 37") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[37];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 38") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[38];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 39") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[39];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 40") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[40];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 41") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[41];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 42") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[42];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 43") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[43];
			StartCoroutine(StopNote());
		}		

		if (render.sprite.name == "note 44") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[44];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 45") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[45];
			StartCoroutine(StopNote());
		}

		if (render.sprite.name == "note 46") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[46];
			StartCoroutine(StopNote());
		}	

		if (render.sprite.name == "note 47") {
			render.color = selectedColor;	
			SynthSourcePad.GetComponent<Oscillator>().gain = synthVolume;
			SynthSourcePad.GetComponent<Oscillator>().frequency = SynthSourcePad.GetComponent<Oscillator>().frequencies[47];
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

		//

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 16") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[0][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 17") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[1][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 18") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[2][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 19") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[3][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 20") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[4][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 21") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[5][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 21") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[5][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 22") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[6][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}		

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 23") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[7][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 24") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[8][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 25") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[9][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 26") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[10][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}																																					

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 27") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[11][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 28") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[12][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 29") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[13][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}	

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 30") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[14][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}		

		if (render.tag == "blocks" && render.color == Color.white && render.sprite.name == "note 31") {
			if (FindIndicesOfObject(this.gameObject, out jFound, out kFound)) {
				OperatorManager.instance.noteDown[15][jFound, kFound] = false;
				render.sprite = block;
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());
				Instantiate(whiteParticles, transform.position, Quaternion.identity);
			}
		}						

		//



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

				//

				if (OperatorManager.instance.noteDown[0][x, y] == true && render.sprite.name == "note 16") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_16;
				}
				else if (OperatorManager.instance.noteDown[0][x, y] == false && render.sprite.name == "note 16") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[1][x, y] == true && render.sprite.name == "note 17") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_17;
				}
				else if (OperatorManager.instance.noteDown[1][x, y] == false && render.sprite.name == "note 17") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[2][x, y] == true && render.sprite.name == "note 18") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_18;
				}
				else if (OperatorManager.instance.noteDown[2][x, y] == false && render.sprite.name == "note 18") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[3][x, y] == true && render.sprite.name == "note 19") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_19;
				}
				else if (OperatorManager.instance.noteDown[3][x, y] == false && render.sprite.name == "note 19") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[4][x, y] == true && render.sprite.name == "note 20") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_20;
				}
				else if (OperatorManager.instance.noteDown[4][x, y] == false && render.sprite.name == "note 20") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[5][x, y] == true && render.sprite.name == "note 21") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_21;
				}
				else if (OperatorManager.instance.noteDown[5][x, y] == false && render.sprite.name == "note 21") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}				

				if (OperatorManager.instance.noteDown[6][x, y] == true && render.sprite.name == "note 22") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_22;
				}
				else if (OperatorManager.instance.noteDown[6][x, y] == false && render.sprite.name == "note 22") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.noteDown[7][x, y] == true && render.sprite.name == "note 23") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_23;
				}
				else if (OperatorManager.instance.noteDown[7][x, y] == false && render.sprite.name == "note 23") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[8][x, y] == true && render.sprite.name == "note 24") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_24;
				}
				else if (OperatorManager.instance.noteDown[8][x, y] == false && render.sprite.name == "note 24") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.noteDown[9][x, y] == true && render.sprite.name == "note 25") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_25;
				}
				else if (OperatorManager.instance.noteDown[9][x, y] == false && render.sprite.name == "note 25") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}

				if (OperatorManager.instance.noteDown[10][x, y] == true && render.sprite.name == "note 26") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_26;
				}
				else if (OperatorManager.instance.noteDown[10][x, y] == false && render.sprite.name == "note 26") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[11][x, y] == true && render.sprite.name == "note 27") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_27;
				}
				else if (OperatorManager.instance.noteDown[11][x, y] == false && render.sprite.name == "note 27") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[12][x, y] == true && render.sprite.name == "note 28") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_28;
				}
				else if (OperatorManager.instance.noteDown[12][x, y] == false && render.sprite.name == "note 28") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[13][x, y] == true && render.sprite.name == "note 29") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_29;
				}
				else if (OperatorManager.instance.noteDown[13][x, y] == false && render.sprite.name == "note 29") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}		

				if (OperatorManager.instance.noteDown[14][x, y] == true && render.sprite.name == "note 30") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_30;
				}
				else if (OperatorManager.instance.noteDown[14][x, y] == false && render.sprite.name == "note 30") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}	

				if (OperatorManager.instance.noteDown[15][x, y] == true && render.sprite.name == "note 31") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = Note_31;
				}
				else if (OperatorManager.instance.noteDown[15][x, y] == false && render.sprite.name == "note 31") {
					OperatorManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = block;
				}																						

				//




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

		//

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 16") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[0][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 16";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 17") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[1][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 17";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 18") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[2][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 18";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 19") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[3][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 19";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 20") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[4][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 20";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}				

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 21") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[5][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 21";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 22") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[6][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 22";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 23") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[7][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 23";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 24") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[8][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 24";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 25") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[9][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 25";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 26") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[10][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 26";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 27") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[11][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 27";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 28") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[12][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 28";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 29") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[13][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 29";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 30") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[14][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 30";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}	

		if (render.sprite.name == "block 0" && render2.sprite.name == "note 31") {
			if (FindIndicesOfObject(clickedPad, out jFound, out kFound)) {
				OperatorManager.instance.tiles[jFound,kFound].GetComponent<SpriteRenderer>().sprite = render2.sprite;
				OperatorManager.instance.noteDown[15][jFound,kFound] = true;
				OperatorManager.instance.notePadsDown[0][jFound,kFound] = "note 31";
				previousSelected.GetComponent<OperatorTile>().Deselect();
				Select();
			}
		}		

		//



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
							SynthSource_0.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_0.GetComponent<Oscillator>().frequency = SynthSource_0.GetComponent<Oscillator>().frequencies[0];	
							noteTiles[0,0].color = selectedColor;
						}
						else if (noteTiles[0,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[0][x, y] == false) {
							noteTiles[0,0].color = Color.white;
							SynthSource_0.GetComponent<Oscillator>().gain = 0;
						}

						if (noteTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[1][x, y] == true) {
                            SynthSource_1.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_1.GetComponent<Oscillator>().frequency = SynthSource_1.GetComponent<Oscillator>().frequencies[1];	
							noteTiles[1,0].color = selectedColor;
						}
						else if (noteTiles[1,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[1][x, y] == false) {
							noteTiles[1,0].color = Color.white;
							SynthSource_1.GetComponent<Oscillator>().gain = 0;
						}			

						if (noteTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[2][x, y] == true) {
							SynthSource_2.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_2.GetComponent<Oscillator>().frequency = SynthSource_2.GetComponent<Oscillator>().frequencies[2];
							noteTiles[2,0].color = selectedColor;
						}
						else if (noteTiles[2,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[2][x, y] == false) {
							noteTiles[2,0].color = Color.white;
							SynthSource_2.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[3][x, y] == true) {
							SynthSource_3.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_3.GetComponent<Oscillator>().frequency = SynthSource_3.GetComponent<Oscillator>().frequencies[3];	
							noteTiles[3,0].color = selectedColor;
						}
						else if (noteTiles[3,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[3][x, y] == false) {
							noteTiles[3,0].color = Color.white;
							SynthSource_3.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[4][x, y] == true) {
							SynthSource_4.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_4.GetComponent<Oscillator>().frequency = SynthSource_4.GetComponent<Oscillator>().frequencies[4];	
							noteTiles[4,0].color = selectedColor;
						}
						else if (noteTiles[4,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[4][x, y] == false) {
							noteTiles[4,0].color = Color.white;
							SynthSource_4.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[5][x, y] == true) {
							SynthSource_5.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_5.GetComponent<Oscillator>().frequency = SynthSource_5.GetComponent<Oscillator>().frequencies[5];
							noteTiles[5,0].color = selectedColor;
						}
						else if (noteTiles[5,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[5][x, y] == false) {
							noteTiles[5,0].color = Color.white;
							SynthSource_5.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[6][x, y] == true) {
							SynthSource_6.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_6.GetComponent<Oscillator>().frequency = SynthSource_6.GetComponent<Oscillator>().frequencies[6];	
							noteTiles[6,0].color = selectedColor;
						}
						else if (noteTiles[6,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[6][x, y] == false) {
							noteTiles[6,0].color = Color.white;
							SynthSource_6.GetComponent<Oscillator>().gain = 0;
						}

						if (noteTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[7][x, y] == true) {
							SynthSource_7.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_7.GetComponent<Oscillator>().frequency = SynthSource_7.GetComponent<Oscillator>().frequencies[7];	
							noteTiles[7,0].color = selectedColor;
						}
						else if (noteTiles[7,0] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[7][x, y] == false) {
							noteTiles[7,0].color = Color.white;
							SynthSource_7.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[0,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[8][x, y] == true) {
							SynthSource_8.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_8.GetComponent<Oscillator>().frequency = SynthSource_8.GetComponent<Oscillator>().frequencies[8];	
							noteTiles[0,1].color = selectedColor;
						}
						else if (noteTiles[0,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[8][x, y] == false) {
							noteTiles[0,1].color = Color.white;
							SynthSource_8.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[1,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[9][x, y] == true) {
							SynthSource_9.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_9.GetComponent<Oscillator>().frequency = SynthSource_9.GetComponent<Oscillator>().frequencies[9];	
							noteTiles[1,1].color = selectedColor;
						}
						else if (noteTiles[1,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[9][x, y] == false) {
							noteTiles[1,1].color = Color.white;
							SynthSource_9.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[2,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[10][x, y] == true) {
							SynthSource_10.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_10.GetComponent<Oscillator>().frequency = SynthSource_10.GetComponent<Oscillator>().frequencies[10];	
							noteTiles[2,1].color = selectedColor;
						}
						else if (noteTiles[2,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[10][x, y] == false) {
							noteTiles[2,1].color = Color.white;
							SynthSource_10.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[3,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[11][x, y] == true) {
							SynthSource_11.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_11.GetComponent<Oscillator>().frequency = SynthSource_11.GetComponent<Oscillator>().frequencies[11];	
							noteTiles[3,1].color = selectedColor;
						}
						else if (noteTiles[3,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[11][x, y] == false) {
							noteTiles[3,1].color = Color.white;
							SynthSource_11.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[4,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[12][x, y] == true) {
							SynthSource_12.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_12.GetComponent<Oscillator>().frequency = SynthSource_12.GetComponent<Oscillator>().frequencies[12];	
							noteTiles[4,1].color = selectedColor;
						}
						else if (noteTiles[4,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[12][x, y] == false) {
							noteTiles[4,1].color = Color.white;
							SynthSource_12.GetComponent<Oscillator>().gain = 0;
						}		

						if (noteTiles[5,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[13][x, y] == true) {
							SynthSource_13.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_13.GetComponent<Oscillator>().frequency = SynthSource_13.GetComponent<Oscillator>().frequencies[13];	
							noteTiles[5,1].color = selectedColor;
						}
						else if (noteTiles[5,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[13][x, y] == false) {
							noteTiles[5,1].color = Color.white;
							SynthSource_13.GetComponent<Oscillator>().gain = 0;
						}	

						if (noteTiles[6,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[14][x, y] == true) {
							SynthSource_14.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_14.GetComponent<Oscillator>().frequency = SynthSource_14.GetComponent<Oscillator>().frequencies[14];	
							noteTiles[6,1].color = selectedColor;
						}
						else if (noteTiles[6,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[14][x, y] == false) {
							noteTiles[6,1].color = Color.white;
							SynthSource_14.GetComponent<Oscillator>().gain = 0;
						}			

						if (noteTiles[7,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[15][x, y] == true) {
							SynthSource_15.GetComponent<Oscillator>().gain = synthVolume;
							SynthSource_15.GetComponent<Oscillator>().frequency = SynthSource_15.GetComponent<Oscillator>().frequencies[15];	
							noteTiles[7,1].color = selectedColor;
						}
						else if (noteTiles[7,1] != null && gameObject.name == OperatorManager.instance.tiles[x, y].name && OperatorManager.instance.note[15][x, y] == false) {
							noteTiles[7,1].color = Color.white;
							SynthSource_15.GetComponent<Oscillator>().gain = 0;
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
		//Debug.Log(nextbeatTime - Time.timeSinceLevelLoad);
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
