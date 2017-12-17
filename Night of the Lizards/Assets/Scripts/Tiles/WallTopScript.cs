﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LizardNight {
    public class WallTopScript : MonoBehaviour {
        public Sprite[] topPieces;
        GridHandler gridHandler;
        //PlayerScript playerScript;

        int wallX;
        int wallY;

        bool north = false;
        /* for directional pieces
        bool east = false;
        bool south = false;
        bool west = false;
        */

        void Awake() {
            gridHandler = FindObjectOfType(typeof(GridHandler)) as GridHandler;
            if (gridHandler != null) {
                //Debug.Log("WallTopScript found grid handler!");
            }

            //playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;

            wallX = (int)transform.position.x;
            //If not a swamp, put the top tile a bit up
            if (!gridHandler.getFloorType().Equals("swamp")) {
                wallY = (int)transform.position.y - 1;
            }
            //Exactly on top if a swamp
            else {
                wallY = (int)transform.position.y;
            }
        }

        // Use this for initialization
        void Start() {
            checkSurroundings();
            updateSprite();

        }

        // Update is called once per frame
        void Update() {

        }

        void checkSurroundings() {
            //wallY at the top row always equals dungeonHeight-1 because indexing starts at 0..
            if (wallY < gridHandler.DungeonHeight - 1) {
                if (gridHandler.getTileGrid(wallX, wallY + 1).name.Equals("Wall(Clone)")) {
                    north = true;
                }
            }
            /* for directional pieces
            if (wallX < gridHandler.DungeonWidth - 1) {
                if (gridHandler.getGrid(wallX + 1, wallY).name.Equals("Wall(Clone)")) {
                    east = true;
                }
            }
            if (wallY > 0) {
                if (gridHandler.getGrid(wallX, wallY - 1).name.Equals("Wall(Clone)")) {
                    south = true;
                }
            }
            if (wallX > 0) {
                if (gridHandler.getGrid(wallX - 1, wallY).name.Equals("Wall(Clone)")) {
                    west = true;
                }
            }
            */
        }

        void updateSprite() {
            /*
            if (north & east & !south & !west) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[4]; //4
            }
            else if (east & south & !north & !west) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[1]; //1
            }
            else if (south & west & !north & !east) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[3]; //3
            }
            else if (north & west & !east & !south) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[6]; //6
            }
            else if (north & south & !east & !west) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[5]; //5
            }
            else if (east & west & !north & !south) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[2]; //2
            }
            else if (!north & !east & !south & !west) {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[0]; //0
            }
            else {
                this.GetComponent<SpriteRenderer>().sprite = topPieces[7];
            }
            */
            if (north) {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[0];
                }
                //Swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[2];
                }
                //Disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[4];
                }
            } else {
                //Bedroom
                if (gridHandler.getFloorType().Equals("bedroom")) {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[1];
                }
                //Swamp
                else if (gridHandler.getFloorType().Equals("swamp")) {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[3];
                }
                //Disco
                else {
                    this.GetComponent<SpriteRenderer>().sprite = topPieces[5];
                }
            }

        }
    }
}
