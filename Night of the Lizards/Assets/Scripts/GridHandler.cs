using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour {

    //Width and height of the dungeon
    [SerializeField]
    private int dungeonWidth = 10;
    [SerializeField]
    private int dungeonHeight = 10;

    //The grid that is inhabited by tiles
    private GameObject[,] grid;


    public int DungeonWidth { get { return dungeonWidth; } }
    public int DungeonHeight { get { return dungeonHeight; } }

    //I have no idea how array getters and setters work so I'll just make bootleg methods
    public GameObject getGrid(int x, int y) {
        return grid[x, y];
    }
    public void setGrid(int x, int y, GameObject gameObject) {
        grid[x, y] = gameObject;
    }

    void Awake() {
        //Set the grid to be the size of given width and height
        grid = new GameObject[dungeonWidth, dungeonHeight];
        //Fills the grid with tiles
        fillGrid();
    }

    void Start() {
        buildDungeon();
    }
    
    void fillGrid() {
        //For every slot in width
        for (int i = 0; i < dungeonWidth; i++) {
            //For every slot in height
            for (int j = 0; j < dungeonHeight; j++) {
                //If current tile is a border tile
                if(i == 0 || j == 0 || i == dungeonWidth-1 || j == dungeonHeight-1) {
                    //Occupy this grid slot with a wall
                    grid[i, j] = Resources.Load("Wall") as GameObject;
                }else {
                    //Occupy this grid slot with a floor
                    grid[i, j] = Resources.Load("Floor") as GameObject;
                }
            }
        }
    }
    
    void buildDungeon() {
        for (int i = 0; i < dungeonWidth; i++) {
            for (int j = 0; j < dungeonHeight; j++) {
                //Create a tile in the grid in corresponding coordinates
                Instantiate(grid[i, j], new Vector3(transform.position.x + i, transform.position.y + j), Quaternion.identity);
            }
        }
    }
}
