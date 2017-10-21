using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight {

    public class DungeonGenerator : MonoBehaviour {
        public GridHandler gridHandler;
        [SerializeField]
        public int tunnelLength = 20;
        GameObject player;

        void Awake() {

        }

        void OnEnable() {

        }

        void Start() {
            player = GameObject.Find("Player");
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.teleportToBeginning();
        }

        public void BuildDungeon(GameObject[,] grid) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);
            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    //Create a tile in the grid in corresponding coordinates
                    grid[i, j] = (GameObject)Instantiate(grid[i, j], new Vector3(transform.position.x + i, transform.position.y + j), Quaternion.identity);
                }
            }
        }

        public void DestroyDungeon(GameObject[,] grid) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);
            GameObject currentTile;
            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    currentTile = grid[i, j];
                    Destroy(currentTile);
                }
            }
        }

        public void FillGrid(GameObject[,] grid, GameObject tileType) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    grid[i, j] = tileType;
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

        public void RemoveCorners(GameObject[,] grid) {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            bool nWest = false;
            bool nEast = false;
            bool sEast = false;
            bool sWest = false;

            bool cornersExist = true;
            int loops = 0;

            while (cornersExist) {
                int corners = 0; //Amount of found corners

                for (int i = 1; i < dungeonWidth - 1; i++) {
                    for (int j = 1; j < dungeonHeight - 1; j++) {
                        //Check surroundings
                        if (grid[i, j] == (GameObject)Resources.Load("Wall")) {
                            sWest = true;
                        }
                        if (grid[i + 1, j] == (GameObject)Resources.Load("Wall")) {
                            sEast = true;
                        }
                        if (grid[i, j + 1] == (GameObject)Resources.Load("Wall")) {
                            nWest = true;
                        }
                        if (grid[i + 1, j + 1] == (GameObject)Resources.Load("Wall")) {
                            nEast = true;
                        }

                        if (nEast && sWest && !nWest && !sEast) {
                            Debug.Log("Case found at " + i + "," + j + "!");
                            grid[i, j] = Resources.Load("Floor") as GameObject;
                            corners++;
                        } else if (nWest && sEast && !nEast && !sWest) {
                            Debug.Log("Case found at " + i + "," + j + "!");
                            grid[i + 1, j] = Resources.Load("Floor") as GameObject;
                            corners++;
                        }

                        sWest = false;
                        sEast = false;
                        nWest = false;
                        nEast = false;
                    }
                }

                loops++;
                if(corners == 0) {
                    cornersExist = false;
                }
            }

            Debug.Log("RemoveCorners() ran through " + loops + " loops before completing.");
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

        /* obsolete trash from GridHandler, look into this later
        void fillGrid() {
            //For every slot in width
            for (int i = 0; i < dungeonWidth; i++) {
                //For every slot in height
                for (int j = 0; j < dungeonHeight; j++) {
                    //If current tile is a border tile
                    if (i == 0 || j == 0 || i == dungeonWidth - 1 || j == dungeonHeight - 1) {
                        //Occupy this grid slot with a wall
                        grid[i, j] = Resources.Load("Wall") as GameObject;
                    } else {
                        //Occupy this grid slot with a floor
                        grid[i, j] = Resources.Load("Floor") as GameObject;
                    }
                }
            }
        }
        */
    }
}
