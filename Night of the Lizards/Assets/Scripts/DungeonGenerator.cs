﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{

    public class DungeonGenerator : MonoBehaviour
    {

        [SerializeField]
        public int tunnelLength = 20;
        [SerializeField]
        GridHandler gridhandler;

        List<Vector2> freeFloorPositions = new List<Vector2>();
        Vector2[] potentialFloorPositions = new Vector2[16 * 16];

        public void UpdateFreeFloors(GameObject[,] grid)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            freeFloorPositions.Clear();

            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    if (grid[i, j] == Resources.Load("Floor")) {
                        freeFloorPositions.Add(new Vector2(i, j));
                    }
                }
            }
        }

        public void BuildDungeon(GameObject[,] grid)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);
            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    //Create a tile in the grid in corresponding coordinates
                    grid[i, j] = Instantiate(grid[i, j], new Vector3(transform.position.x + i, transform.position.y + j), Quaternion.identity);
                }
            }
        }

        public void DestroyDungeon(GameObject[,] grid)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);
            GameObject currentTile;
            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    currentTile = grid[i, j];
                    //Destroy(currentTile);
                    Destroy(grid[i, j]);
                }
            }
        }

        public void FillGrid(GameObject[,] grid, GameObject tileType)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    grid[i, j] = tileType;
                }
            }
        }

        public void RandomTunneler(GameObject[,] grid, int tunnelLength)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            //Middle of the dungeon coordinates
            int middleX = Mathf.RoundToInt(dungeonWidth / 2);
            int middleY = Mathf.RoundToInt(dungeonHeight / 2);

            //Current coordinates of tunneler
            int currentX = middleX;
            int currentY = middleY;

            //The tile in the middle of the dungeon is changed to a floor
            grid[middleX, middleY] = Resources.Load("Floor") as GameObject;

            //Loop as long as tunnel length is achieved
            for (int i = 0; i < tunnelLength; i++) {
                //validMove is false as long as tunneler lands on floor, turns true when landing on wall
                bool validMove = false;
                //Attempts increase when more valid moves are made
                int attempts = 0;

                while (!validMove) {
                    //Generate a random direction to go to
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

                    //If landed tile was a wall, it was a valid move. Reset attempts
                    if (grid[currentX, currentY] == Resources.Load("Wall")) {
                        validMove = true;
                        attempts = 0;
                    } else {
                        //Else increase attempts
                        //Optimally this should reset curent position to a wall that can be tunneled into.

                        attempts++;
                    }

                    //Exit loop if failed attempts are greater than given tunnel length times ten
                    if (attempts > tunnelLength * 10) {
                        Debug.LogError("Stopped generation after " + tunnelLength * 10 + " failed attempts.");
                        validMove = true;
                        i = tunnelLength;
                    }

                }
                grid[currentX, currentY] = Resources.Load("Floor") as GameObject;
            }
        }

        /* Lots of shit on new type of generation, didn't go well
        public void RandomListTunneler(GameObject[,] grid, int tunnelLength)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);

            //Middle of the dungeon coordinates
            int middleX = Mathf.RoundToInt(dungeonWidth / 2);
            int middleY = Mathf.RoundToInt(dungeonHeight / 2);

            //Current coordinates of tunneler
            int currentX = middleX;
            int currentY = middleY;

            //The tile in the middle of the dungeon is changed to a floor
            grid[middleX, middleY] = Resources.Load("Floor") as GameObject;

            //Loop as long as tunnel length is achieved
            for (int i = 0; i < tunnelLength; i++) {
                UpdatePotentialFloors(grid);
                Vector2 randomPotential = GetRandomPotentialFloor();
                grid[(int)randomPotential.x, (int)randomPotential.y] = Resources.Load("Floor") as GameObject;
            }
        }

        public Vector2 GetRandomPotentialFloor()
        {
            int rIndex = Random.Range(0, potentialFloorPositions.Length);
            return potentialFloorPositions[rIndex];
        }

        public void UpdatePotentialFloors(GameObject[,] grid)
        {
            int dungeonWidth = grid.GetLength(0);
            int dungeonHeight = grid.GetLength(1);
            
            //Clear array
            for (int i = 0; i < potentialFloorPositions.Length; i++) {
                potentialFloorPositions[0] = Vector2.zero;
            }

            int currentIndex = 0;

            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    //If we're at any corner of the dungeon
                    if(i == 0 || j == 0 || i == dungeonWidth-1 || j == dungeonHeight-1) {
                        //dont do shit
                    }else {
                        //Otherwise check if this is a wall
                        if (grid[i, j] == Resources.Load("Wall")) {
                            //If it's a wall check it's surroundings

                            bool north = false;
                            bool east = false;
                            bool south = false;
                            bool west = false;

                            //If tile above is a floor
                            //Debug.Log("Checking around " + i +", "+ j);
                            if (grid[i, j + 1] == Resources.Load("Floor")) {
                                
                                north = true;
                            }
                            //If tile to the right is a floor
                            if (grid[i+1,j] == Resources.Load("Floor")) {
                                east = true;
                            }
                            //If tile below is a floor
                            if (grid[i, j - 1] == Resources.Load("Floor")) {
                                south = true;
                            }
                            //If tile to the left is a floor
                            if (grid[i - 1, j] == Resources.Load("Floor")) {
                                west = true;
                            }
                            
                            //If any position contains floor
                            if(north || east || south || west) {
                                Debug.Log("currentIndex is " + currentIndex);
                                potentialFloorPositions[currentIndex] = new Vector2(i, j);
                            }
                        }
                    }
                    currentIndex++;
                }
            }
        }
        */

        /* Tile surround check, I'll think of this later
        public bool CheckTileNorth(GameObject[,] grid, int tileX, int tileY, GameObject comparisonTarget)
        {
            if (grid[tileX, tileY+1] == comparisonTarget) {
                return true;
            }else {
                return false;
            }
        }
        */

        public void RemoveCorners(GameObject[,] grid)
        {
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
                if (corners == 0) {
                    cornersExist = false;
                }
            }

            Debug.Log("RemoveCorners() ran through " + loops + " loops before completing.");
        }

        public void PlaceRandomStairs(GameObject[,] grid)
        {
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

        public void SpawnEnemy(GameObject[,] grid, GameObject enemyPrefab, Vector2 position, int level)
        {
            GameObject enemy;
            enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

            BasicEnemy enemySheet = enemy.GetComponent<BasicEnemy>();

            if (enemySheet != null) {
                enemySheet.InitializeAttributes(level);
            }
        }

        public void SpawnEnemies(GameObject[,] grid, GameObject enemyPrefab, int amount)
        {
            SpawnEnemies(grid, enemyPrefab, amount, 1);
        }

        public void SpawnEnemies(GameObject[,] grid, GameObject enemyPrefab, int amount, int level)
        {
            for (int i = 0; i < amount; i++) {
                int rand = Random.Range(0, freeFloorPositions.Count);
                SpawnEnemy(grid, enemyPrefab, freeFloorPositions[rand], level);
                freeFloorPositions.RemoveAt(rand);
            }
        }

        public void DestroyEnemies()
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy")) {
                Destroy(enemy);
            }
        }

        public void DestroyPowerUps()
        {
            foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag("power up")) {
                Destroy(powerUp);
            }
        }

        public void FloorCountDebugger()
        {
            for (int i = 0; i < freeFloorPositions.Count; i++) {
                Debug.Log("freeFloorPositions[" + i + "] = " + freeFloorPositions[i]);
            }
        }

        public void LoadLevel(GameObject[,] grid, string levelName)
        {
            //Tutorial floor
            if (levelName.Equals("tutorial")) {
                FillGrid(grid, Resources.Load("Wall") as GameObject);
                Vector2[] floorTiles = { new Vector2(14, 19), new Vector2(16, 19), new Vector2(18, 19),
                                         new Vector2(13, 18), new Vector2(14, 18), new Vector2(15, 18), new Vector2(16, 18), new Vector2(17, 18), new Vector2(18, 18), new Vector2(19, 18), new Vector2(20, 18),
                                         new Vector2(14, 17), new Vector2(20, 17),
                                         new Vector2(13, 16), new Vector2(14, 16), new Vector2(20, 16),
                                         new Vector2(14, 15), new Vector2(20, 15),
                                         new Vector2(13, 14), new Vector2(14, 14), new Vector2(15, 14), new Vector2(16, 14), new Vector2(18, 14), new Vector2(19, 14), new Vector2(20, 14),
                                         new Vector2(14, 13), new Vector2(15, 13), new Vector2(16, 13), new Vector2(18, 13), new Vector2(19, 13), new Vector2(20, 13)
                                        };

                //Place floors
                for (int i = 0; i < floorTiles.Length; i++) {
                    grid[(int)floorTiles[i].x, (int)floorTiles[i].y] = Resources.Load("Floor") as GameObject;
                }

                //Stairs
                grid[15, 14] = Resources.Load("StairsUp") as GameObject;
                grid[18, 13] = Resources.Load("StairsDown") as GameObject;

                //Enemies
                SpawnEnemy(grid, Resources.Load("Enemy") as GameObject, new Vector2(20, 14), -1);
            }

            //Boss floor
            else if (levelName.Equals("boss")) {
                FillGrid(grid, Resources.Load("Wall") as GameObject);
                Vector2[] floorTiles = { new Vector2(14, 19),
                                         new Vector2(14, 18),
                                         new Vector2(12, 17), new Vector2(13, 17), new Vector2(14, 17),
                                         new Vector2(14, 16),
                                         new Vector2(12, 15), new Vector2(13, 15), new Vector2(14, 15), new Vector2(15, 15), new Vector2(16, 15),
                                         new Vector2(12, 14), new Vector2(13, 14), new Vector2(14, 14), new Vector2(15, 14), new Vector2(16, 14),
                                         new Vector2(12, 13), new Vector2(13, 13), new Vector2(14, 13), new Vector2(15, 13), new Vector2(16, 13),
                                         new Vector2(12, 12), new Vector2(13, 12), new Vector2(14, 12), new Vector2(15, 12), new Vector2(16, 12),
                                         new Vector2(12, 11), new Vector2(13, 11), new Vector2(14, 11), new Vector2(15, 11), new Vector2(16, 11),
                                         new Vector2(13, 10), new Vector2(14, 10), new Vector2(15,10),
                                         new Vector2(13, 9), new Vector2(14, 9), new Vector2(15,9)
                                        };
                //Place floors
                for (int i = 0; i < floorTiles.Length; i++) {
                    grid[(int)floorTiles[i].x, (int)floorTiles[i].y] = Resources.Load("Floor") as GameObject;
                }

                //Stairs
                grid[14, 12] = Resources.Load("StairsUp") as GameObject;
                grid[14, 19] = Resources.Load("StairsDown") as GameObject;

                //Enemies
                if(gridhandler.getDungeonFloor() < 6) {
                    SpawnEnemy(grid, Resources.Load("BedroomBoss") as GameObject, new Vector2(14, 16), 1);
                } else {
                    SpawnEnemy(grid, Resources.Load("DiscoBoss") as GameObject, new Vector2(14, 16), 5);
                }
            }

            //No floor
            else {
                Debug.Log("No level named \"" + levelName + "\" exists!");
            }
        }
    }
}
