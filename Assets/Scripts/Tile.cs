using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private SpriteRenderer render;
	//public GameObject boardManager;
	public GameObject whiteParticles;
	
	private bool matchFound = false;
	private bool isSelected = false;

	Camera mainCamera;

	void Awake() {
		render = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		Deselect();
		//SumScore.ClearHighScore();
    }

	void Update() {
		CheckHighScore();
	}

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = GetComponent<Tile>();
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
			} else { //if (GetAllAdjacentTiles().Contains(previousSelected.gameObject)){
				SwapSprite(previousSelected.render);
				GetComponent<RotateYaxis>().flipTile();
				StartCoroutine(resetflipTile());

				previousSelected.ClearAllMatches();
				previousSelected.GetComponent<Tile>().Deselect();
				ClearAllMatches();				
			}
		}
	}	

	public void SwapSprite(SpriteRenderer render2) {
		if (render.GetComponent<SpriteRenderer>().sprite == render2.sprite) { 
			return;
		}

		Sprite tempSprite = render2.sprite;
		render2.sprite = render.GetComponent<SpriteRenderer>().sprite;
		render.GetComponent<SpriteRenderer>().sprite = tempSprite;		

		SFXManager.instance.PlaySFX(Clip.Swap);
		mainCamera.GetComponent<CameraShake>().shakecamera();
		StartCoroutine(StopShakingCamera());
	}

	private GameObject GetAdjacent(Vector2 castDir) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		if (hit.collider != null) {
			return hit.collider.gameObject;
		}
		return null;
	}	

	private List<GameObject> GetAllAdjacentTiles() {
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++) {
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		return adjacentTiles;
	}	

	private List<GameObject> FindMatch(Vector2 castDir) { // 1
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // 3
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite) { // 4
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);	
		}
		return matchingTiles; // 5
	}

	private void ClearMatch(Vector2[] paths) // 1
	{
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		for (int i = 0; i < paths.Length; i++) // 3
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 3) // 4
		{
			for (int i = 0; i < matchingTiles.Count; i++) // 5
			{
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
				if (matchingTiles.Count == 3) {
					SumScore.Add(matchingTiles.Count);
					SFXManager.instance.PlaySFX(Clip.Clear4);
					Instantiate(whiteParticles, transform.position, Quaternion.identity);
				}				
				if (matchingTiles.Count == 4) {
					SumScore.Add(matchingTiles.Count);
					SFXManager.instance.PlaySFX(Clip.Clear5);
					Instantiate(whiteParticles, transform.position, Quaternion.identity);
				}
				if (matchingTiles.Count == 5) {
					SumScore.Add(matchingTiles.Count);
					BoardManager.instance.GetComponent<AudioSource>().volume = 0;
					SFXManager.instance.PlaySFX(Clip.Clear6);
					
					StartCoroutine(ShadowWave());
					Instantiate(whiteParticles, transform.position, Quaternion.identity);
					StartCoroutine(HasNasSampleStoppedPlaying());
				}						
				if (matchingTiles.Count >= 6) {
					SumScore.Add(matchingTiles.Count);
					BoardManager.instance.GetComponent<AudioSource>().volume = 0;
					SFXManager.instance.PlaySFX(Clip.Clear7);
										
					StartCoroutine(TriggerWave(matchingTiles.Count));
					StartCoroutine(ShadowWave());
					StartCoroutine(HasSoulSampleStoppedPlaying());
				}					
			}
			matchFound = true; // 6
		}
	}	

	public void ClearAllMatches() {
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound) {
			render.sprite = null;
			matchFound = false;

			StopCoroutine(BoardManager.instance.FindNullTiles());
			StartCoroutine(BoardManager.instance.FindNullTiles());
		}
	}

	public IEnumerator TriggerWave(int clearedCount) {
		for (int x = 0; x < BoardManager.instance.xSize; x++) {
			for (int y = 0; y < BoardManager.instance.ySize; y++) {
				if (BoardManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite == render.sprite) {
					BoardManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
					Instantiate(whiteParticles, BoardManager.instance.tiles[x, y].transform.position, Quaternion.identity);
					StartCoroutine(BoardManager.instance.FindNullTiles());
					SFXManager.instance.PlaySFX(Clip.Scratch);
					SumScore.Add(clearedCount);
					yield return StartCoroutine(WaveDelay());
				}
			}
		}
		StopCoroutine(BoardManager.instance.FindNullTiles());
		StartCoroutine(BoardManager.instance.FindNullTiles());		
	}

	public IEnumerator ShadowWave() {
		for (int x = 0; x < BoardManager.instance.xSize; x++) {
			for (int y = 0; y < BoardManager.instance.ySize; y++) {
				BoardManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = selectedColor;
				//Instantiate(whiteParticles, BoardManager.instance.tiles[x, y].transform.position, Quaternion.identity);
				yield return StartCoroutine(ShadowWaveDelay());
			}
		}
		StartCoroutine(UnShadowWave());		
	}	

	public IEnumerator UnShadowWave() {
		for (int x = 0; x < BoardManager.instance.xSize; x++) {
			for (int y = 0; y < BoardManager.instance.ySize; y++) {
				BoardManager.instance.tiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
				//Instantiate(whiteParticles, BoardManager.instance.tiles[x, y].transform.position, Quaternion.identity);
				yield return null;
			}
		}		
	}		

	IEnumerator WaveDelay() {
		yield return new WaitForSeconds(0.5f);
	}

	IEnumerator ShadowWaveDelay() {
		yield return new WaitForSeconds(0.01f);
	}	

	IEnumerator StopShakingCamera() {
		yield return new WaitForSeconds(0.1f);
		mainCamera.GetComponent<CameraShake>().stopshakingcamera();
	}

	IEnumerator resetflipTile() {
		yield return new WaitForSeconds(1f);
		GetComponent<RotateYaxis>().resetflipTile();
	}

	IEnumerator HasNasSampleStoppedPlaying() {
		yield return new WaitForSeconds(5.237f);
		BoardManager.instance.GetComponent<AudioSource>().volume = 1;	
		StartCoroutine(BoardManager.instance.FindNullTiles());	
	}

	IEnumerator HasSoulSampleStoppedPlaying() {
		yield return new WaitForSeconds(19.412f);
		BoardManager.instance.GetComponent<AudioSource>().volume = 1;
		StartCoroutine(BoardManager.instance.FindNullTiles());		
	}	

    public void CheckHighScore () {
        if (SumScore.Score > SumScore.HighScore)
            SumScore.SaveHighScore();
    }	
}