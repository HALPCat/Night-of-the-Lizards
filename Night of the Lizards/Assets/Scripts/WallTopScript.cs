﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTopScript : MonoBehaviour {
    public Sprite[] topPieces;
    GridHandler gridHandler;
    PlayerScript playerScript;

    int wallX;
    int wallY;

    bool north = false;
    bool east = false;
    bool south = false;
    bool west = false;

    void Awake() {
        gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;
        if(gridHandler != null) {
            //Debug.Log("WallTopScript found grid handler!");
        }

        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;

        wallX = (int)transform.position.x;
        wallY = (int)transform.position.y-1;
    }

    // Use this for initialization
    void Start () {
        checkSurroundings();
        updateSprite();
        
    }
	
	// Update is called once per frame
	void Update () {
		if(playerScript.PositionY < wallY+1) {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Dungeon background";
        }else {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Dungeon foreground";
        }
	}
    
    void checkSurroundings() {
        //wallY at the top row always equals dungeonHeight-1 because indexing starts at 0..
        if (wallY < gridHandler.DungeonHeight - 1) {
            if (gridHandler.getGrid(wallX, wallY + 1).name.Equals("Wall")) {
                north = true;
            }
        }
        if(wallX < gridHandler.DungeonWidth - 1) {
            if (gridHandler.getGrid(wallX+1, wallY).name.Equals("Wall")) {
                east = true;
            }
        }
        if (wallY > 0) {
            if (gridHandler.getGrid(wallX, wallY - 1).name.Equals("Wall")) {
                south = true;
            }
        }
        if (wallX > 0) {
            if (gridHandler.getGrid(wallX - 1, wallY).name.Equals("Wall")) {
                west = true;
            }
        }
    }

    void updateSprite() {
        if (north && east) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[4];
        }
        else if (east && south) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[1];
        }
        else if (south && west) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[3];
        }
        else if (north && west) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[6];
        }
        else if (north && south) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[5];
        }
        else if (east && west) {
            this.GetComponent<SpriteRenderer>().sprite = topPieces[2];
        }
    }
}
