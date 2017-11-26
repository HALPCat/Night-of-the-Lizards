using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight {

    public class GridHandler : MonoBehaviour {

        //Width and height of the dungeon
        [SerializeField]
        private int dungeonWidth = 10;
        [SerializeField]
        private int dungeonHeight = 10;
        
        private int dungeonFloor = 0;

        //Grid for dungeon tiles
        private GameObject[,] tileGrid;
        //characters grid
        private GameObject[,] charGrid;

        GameObject player;
        PlayerScript playerScript;

        //addind this for enemy handling!! - ANTONIO
       public GameMaster GM;



        //The dungeon generator that this grid handler uses for building dungeons
        [SerializeField]
        private DungeonGenerator dungeonGenerator;

        public int DungeonWidth { get { return dungeonWidth; } }
        public int DungeonHeight { get { return dungeonHeight; } }




        //PATHFINDING STUFF
        public LayerMask unwalkableMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        [SerializeField]
        private Node[,] nodeGrid;
        float nodeDiameter;
        int gridSizeX, gridSizeY;




        //I have no idea how array getters and setters work so I'll just make bootleg methods
        public GameObject getTileGrid(int x, int y) {
            return tileGrid[x, y];
        }
        public GameObject getCharGrid(int x, int y) {
            return charGrid[x, y];
        }
        public void setTileGrid(int x, int y, GameObject gameObject) {
            tileGrid[x, y] = gameObject;
        }
        public void setCharGrid(int x, int y, GameObject gameObject) {
            
            charGrid[x, y] = gameObject;
        }
        public int getDungeonFloor()
        {
            return dungeonFloor;
        }


        void Awake() {
            //Assigning dungeonGenerator a DungeonGenerator
            if (dungeonGenerator == null) {
                dungeonGenerator = GetComponent<DungeonGenerator>();
                Debug.Log("GridHandler has no dungeonGenerator assigned in Unity UI, getting component");
            }
            if(dungeonGenerator == null) {
                Debug.LogError("GridHandler couldn't find an instance of DungeonGenerator!");
            }

            player = GameObject.Find("Player");
            playerScript = player.GetComponent<PlayerScript>();


            //Set the tileGrid and charGrid to be the given size
            tileGrid = new GameObject[dungeonWidth, dungeonHeight];
            charGrid = new GameObject[dungeonWidth, dungeonHeight];

            GM = GetComponent<GameMaster>();


            // - - - - - - - - - - Begin dungeon generation - - - - - - - - - -

            LoadTutorialStage();

            // - - - - - - - - - - End dungeon generation - - - - - - - - - -

            /*
            CreateNodeGrid();
            fillCharGrid();
            */
        }

        public void NewFloor() {
            dungeonGenerator.DestroyEnemies();
            dungeonGenerator.DestroyPowerUps();
            dungeonGenerator.DestroyDungeon(tileGrid);
            dungeonGenerator.FillGrid(tileGrid, Resources.Load("Wall") as GameObject);  //Fills the entire floor with walls
            dungeonGenerator.RandomTunneler(tileGrid, dungeonGenerator.tunnelLength);   //Creates a tunnel on the floor
            dungeonGenerator.RemoveCorners(tileGrid);                                   //Removes corners that make diagonals look weird
            dungeonGenerator.PlaceRandomStairs(tileGrid);                                     //Places stairs on the floor
            dungeonGenerator.UpdateFreeFloors(tileGrid);                                //Used for finding spots for the enemies
            dungeonGenerator.SpawnEnemies(tileGrid, (GameObject)Resources.Load("Enemy"), 10, dungeonFloor);   //Places enemies
            dungeonGenerator.BuildDungeon(tileGrid);                                    //Instantiates the gameobjects in tileGrid
            StartCoroutine(TelePlayerBeginning());

            CreateNodeGrid();
            fillCharGrid();

            dungeonFloor++;
        }

        public void LoadTutorialStage()
        {
            dungeonGenerator.DestroyEnemies();
            dungeonGenerator.DestroyPowerUps();
            dungeonGenerator.DestroyDungeon(tileGrid);
            dungeonGenerator.LoadLevel(tileGrid, "tutorial");
            dungeonGenerator.BuildDungeon(tileGrid);                                    //Instantiates the gameobjects in tileGrid
            StartCoroutine(TelePlayerBeginning());
            CreateNodeGrid();
            fillCharGrid();
        }

        IEnumerator TelePlayerBeginning() {
            yield return new WaitForEndOfFrame();

            playerScript.teleportToBeginning();
        }
        void Start() {

        }

        //starts the char grid as null
        void fillCharGrid() {
            for (int i = 0; i < dungeonWidth; i++) {
                for (int j = 0; j < dungeonHeight; j++) {
                    charGrid[i, j] = null;
                }
            }
        }

        //MORE PATHFINDING STUFF

        // returns max size of heap for pathfinding
        public int maxSize
        {
            get { return DungeonHeight * DungeonWidth; }
        }

        //draws path, for testing
        public List<Node> path;
        
        void OnDrawGizmos() {
            Gizmos.DrawWireCube(new Vector3(8, 4), new Vector3(dungeonWidth, dungeonHeight));
            if (nodeGrid != null)
                foreach (Node n in nodeGrid) {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if (path != null)
                        if (path.Contains(n)) {

                            Gizmos.color = Color.blue;
                        }

                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .75f));
                }
        }
        

        public void CreateNodeGrid() {
            nodeDiameter = nodeRadius * 2;
            nodeGrid = new Node[dungeonWidth, dungeonHeight];
            for (int x = 0; x < dungeonWidth; x++) {
                for (int y = 0; y < DungeonHeight; y++) {
                    //Debug.Log("Checking " + x + y);
                    if (tileGrid[x, y].name.Equals("Wall(Clone)")) {
                        nodeGrid[x, y] = new Node(false, new Vector2(x, y), x, y);
                        //Debug.Log("Wall node created");

                    } else {
                        nodeGrid[x, y] = new Node(true, new Vector2(x, y), x, y);
                        //Debug.Log("Floor node created");
                    }
                }
            }

        }

        public bool getWalkable(int x, int y) {
            return nodeGrid[x, y].walkable;
        }
        public void setNode(int x, int y, bool isWalkable) {
            nodeGrid[x, y].walkable = isWalkable;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition) {
            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.y);

            return nodeGrid[x, y];
        }

        public List<Node> GetNeighbours(Node node) {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < dungeonWidth && checkY >= 0 && checkY < dungeonHeight) {

                        neighbours.Add(nodeGrid[checkX, checkY]);

                    }
                }
            }


            return neighbours;
        }
    }

}
