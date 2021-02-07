using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour {
	public static PadManager instance;
	public List<Sprite> pad = new List<Sprite>();
	public GameObject tile;
	public int xSize, ySize;
	int element;

	public GameObject[,] tiles;

	public bool IsShifting { get; set; }

	int i = 1;

	void Start () {
		instance = GetComponent<PadManager>();

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y); 
	}

    private void CreateBoard (float xOffset, float yOffset) {
        tiles = new GameObject[xSize, ySize];    

        float startX = transform.position.x;    
        float startY = transform.position.y;

        for (int y = 0; y < ySize; y++) {      
            for (int x = 0; x < xSize; x++) {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
				tiles[x, y] = newTile;

				newTile.transform.parent = transform; 

				Sprite newSprite = pad[element++];
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite;

				newTile.tag = "pads"; 
				newTile.name = "Note " + i++.ToString();
            }
        }
	}
}
