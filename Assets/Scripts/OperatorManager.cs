using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorManager : MonoBehaviour {
	public static OperatorManager instance;
	public Sprite block;
	public GameObject tile;
	public int xSize, ySize;

	int i = 1;

	public GameObject[,] tiles;
	public GameObject[][,] boards = new GameObject[8][,];

	public bool IsShifting { get; set; }

	void Start () {
		instance = GetComponent<OperatorManager>();

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y); 
	}

    private void CreateBoard (float xOffset, float yOffset) {
        tiles = new GameObject[xSize, ySize];    

        float startX = transform.position.x;    
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++) {      
            for (int y = 0; y < ySize; y++) {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
				tiles[x, y] = newTile;

				newTile.transform.parent = transform; 

				Sprite newSprite = block;
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite; 
				newTile.tag = "blocks";
				newTile.name = "Pad " + i++.ToString();
			}	
        }

		for (int x = 0; x < boards.Length; x++) { 
			boards[x] = tiles;
		}
	}
}
