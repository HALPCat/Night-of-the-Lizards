using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight {

    public class DungeonGenerator : MonoBehaviour {
        public GridHandler gridHandler;
        [SerializeField]
        int tunnelLength = 20;
        GameObject player;

        void Awake() {
            fillGrid(Resources.Load("Wall") as GameObject);
            randomTunneler(tunnelLength);
            PlaceStairs();
            gridHandler.CreateNodeGrid();
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
                    fillGrid(Resources.Load("Wall") as GameObject);
                    randomTunneler(tunnelLength);
                    PlaceStairs();
                    gridHandler.buildDungeon();
                }
            }
        }

        void fillGrid(GameObject tileType) {
            for (int i = 0; i < gridHandler.DungeonWidth; i++) {
                for (int j = 0; j < gridHandler.DungeonHeight; j++) {
                    gridHandler.setGrid(i, j, tileType);
                }
            }
        }

        void randomTunneler(int tunnelLength) {
            int middleX = Mathf.RoundToInt(gridHandler.DungeonWidth / 2);
            int middleY = Mathf.RoundToInt(gridHandler.DungeonHeight / 2);
            int currentX = middleX;
            int currentY = middleY;
            gridHandler.setGrid(middleX, middleY, Resources.Load("Floor") as GameObject);
            for (int i = 0; i < tunnelLength; i++) {
                bool validMove = false;
                int attempts = 0;

                while (!validMove) {
                    //0 = up, 1 = right, 2 = down, 3 = left
                    int randDirection = (int)(Random.value * 4);

                    if (randDirection == 0) {
                        currentY++;
                        if (currentY == gridHandler.DungeonHeight - 1) {
                            currentY--;
                        }
                    } else if (randDirection == 1) {
                        currentX++;
                        if (currentX == gridHandler.DungeonWidth - 1) {
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

                    if (gridHandler.getGrid(currentX, currentY) == Resources.Load("Wall")) {
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

                gridHandler.setGrid(currentX, currentY, Resources.Load("Floor") as GameObject);
            }
        }

        void PlaceStairs() {
            int randX;
            int randY;
            bool downStairPlaced = false;
            bool upStairPlaced = false;

            while (!downStairPlaced) {
                randX = Random.Range(0, gridHandler.DungeonWidth);
                randY = Random.Range(0, gridHandler.DungeonHeight);

                if (gridHandler.getGrid(randX, randY) == Resources.Load("Floor")) {
                    gridHandler.setGrid(randX, randY, Resources.Load("StairsDown") as GameObject);
                    downStairPlaced = true;
                }
            }

            while (!upStairPlaced) {
                randX = Random.Range(0, gridHandler.DungeonWidth);
                randY = Random.Range(0, gridHandler.DungeonHeight);

                if (gridHandler.getGrid(randX, randY) == Resources.Load("Floor")) {
                    gridHandler.setGrid(randX, randY, Resources.Load("StairsUp") as GameObject);
                    upStairPlaced = true;
                }
            }
        }
    }
}
