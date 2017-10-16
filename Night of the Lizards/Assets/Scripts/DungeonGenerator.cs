﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight {

    public class DungeonGenerator : MonoBehaviour {
        public GridHandler gridHandler;
        [SerializeField]
        public int tunnelLength = 20;
        GameObject player;

        void Awake() {
            //FillGrid(Resources.Load("Wall") as GameObject);
            //RandomTunneler(tunnelLength);
            //PlaceStairs();
            //gridHandler.CreateNodeGrid();
        }


        //OnEnable is called after Awake but before Start
        //fillDungeon is called in Awake and buildDungeon is called in Start
        void OnEnable() {

        }

        void Start() {
            player = GameObject.Find("Player");
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.teleportToBeginning();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (gridHandler.getGrid((int)player.transform.position.x, (int)player.transform.position.y).name.Equals("StairsDown(Clone)")) {
                    gridHandler.destroyDungeon();
                    //FillGrid(Resources.Load("Wall") as GameObject);
                    //RandomTunneler(tunnelLength);
                    //PlaceStairs();
                    //gridHandler.buildDungeon();
                }
            }
        }

        public void FillGrid(GameObject[,] grid, GameObject tileType) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    gridHandler.setGrid(i, j, tileType);
                }
            }
        }

        public void RandomTunneler(GameObject[,] grid, int tunnelLength) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            int middleX = Mathf.RoundToInt(dungeonWidth / 2);
            int middleY = Mathf.RoundToInt(dungeonHeight / 2);
            int currentX = middleX;
            int currentY = middleY;
            grid[middleX, middleY] = Resources.Load("Floor") as GameObject;
            for (int i = 0; i < tunnelLength; i++) {
                bool validMove = false;
                int attempts = 0;

                while (!validMove) {
                    //0 = up, 1 = right, 2 = down, 3 = left
                    int randDirection = (int)(Random.value * 4);

                    if (randDirection == 0) {
                        currentY++;
                        if (currentY == dungeonHeight - 1) {
                            currentY--;
                        }
                    } else if (randDirection == 1) {
                        currentX++;
                        if (currentX == dungeonWidth - 1) {
                            currentX--;
                        }
                    } else if (randDirection == 2) {
                        currentY--;
                        if (currentY == 0) {
                            currentY++;
                        }
                    } else if (randDirection == 3) {
                        currentX--;
                        if (currentX == 0) {
                            currentX++;
                        }
                    }

                    if (grid[currentX, currentY] == Resources.Load("Wall")) {
                        validMove = true;
                        attempts = 0;
                    } else {
                        attempts++;
                    }
                    if (attempts > tunnelLength * 10) {
                        Debug.LogError("Stopped generation after " + tunnelLength * 10 + " failed attempts.");
                        validMove = true;
                        i = tunnelLength;
                    }

                }
                grid[currentX, currentY] = Resources.Load("Floor") as GameObject;
            }
        }

        public void PlaceStairs(GameObject[,] grid) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            int randX;
            int randY;
            bool downStairPlaced = false;
            bool upStairPlaced = false;

            while (!downStairPlaced) {
                randX = Random.Range(0, dungeonWidth);
                randY = Random.Range(0, dungeonHeight);

                if (grid[randX, randY] == Resources.Load("Floor")) {
                    grid[randX, randY] = Resources.Load("StairsDown") as GameObject;
                    downStairPlaced = true;
                }
            }

            while (!upStairPlaced) {
                randX = Random.Range(0, dungeonWidth);
                randY = Random.Range(0, dungeonHeight);

                if (grid[randX, randY] == Resources.Load("Floor")) {
                    grid[randX, randY] = Resources.Load("StairsUp") as GameObject;
                    upStairPlaced = true;
                }
            }
        }
    }
}
