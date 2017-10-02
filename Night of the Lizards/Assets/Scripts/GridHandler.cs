using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{

    public class GridHandler : MonoBehaviour
    {

        //Width and height of the dungeon
        [SerializeField]
        private int dungeonWidth = 10;
        [SerializeField]
        private int dungeonHeight = 10;

        //The grid that is inhabited by tiles
        private GameObject[,] grid;
        //characters grid
        private GameObject[,] charGrid;

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
        public GameObject getGrid(int x, int y)
        {
            return grid[x, y];
        }
        public GameObject getCharGrid(int x, int y)
        {
            return charGrid[x, y];
        }
        public void setGrid(int x, int y, GameObject gameObject)
        {
            grid[x, y] = gameObject;
        }
        public void setCharGrid(int x, int y, GameObject gameObject)
        {
            charGrid[x, y] = gameObject;
        }

        void Awake()
        {
            //Set the grid to be the size of given width and height
            grid = new GameObject[dungeonWidth, dungeonHeight];
            charGrid = new GameObject[dungeonWidth, dungeonHeight];
            //Fills the grid with tiles
            fillGrid();
            fillCharGrid();
            CreateNodeGrid();

        }

        void Start()
        {
            buildDungeon();
            

        }

        void fillGrid()
        {
            //For every slot in width
            for (int i = 0; i < dungeonWidth; i++)
            {
                //For every slot in height
                for (int j = 0; j < dungeonHeight; j++)
                {
                    //If current tile is a border tile
                    if (i == 0 || j == 0 || i == dungeonWidth - 1 || j == dungeonHeight - 1)
                    {
                        //Occupy this grid slot with a wall
                        grid[i, j] = Resources.Load("Wall") as GameObject;
                    }
                    else
                    {
                        //Occupy this grid slot with a floor
                        grid[i, j] = Resources.Load("Floor") as GameObject;
                    }
                }
            }
        }

        //starts the char grid as null
        void fillCharGrid ()
        {
            for (int i = 0; i < dungeonWidth; i++)
            {
                for(int j = 0; j < dungeonHeight; j++)
                {
                    charGrid[i, j] = null;
                }
            }
        }

        void buildDungeon()
        {
            for (int i = 0; i < dungeonWidth; i++)
            {
                for (int j = 0; j < dungeonHeight; j++)
                {
                    //Create a tile in the grid in corresponding coordinates
                    Instantiate(grid[i, j], new Vector3(transform.position.x + i, transform.position.y + j), Quaternion.identity);
                }
            }
        }

        //MORE PATHFINDING STUFF

        //draws path, for testing
        public List<Node> path;
        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(new Vector3(8,4), new Vector3(dungeonWidth, dungeonHeight));
            if (nodeGrid!= null)
                foreach (Node n in nodeGrid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if (path != null)
                        if (path.Contains(n)) {
                            
                            Gizmos.color = Color.blue;
                         }
                            
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
        }

      
        private void CreateNodeGrid()
        {
            nodeDiameter = nodeRadius * 2;
            nodeGrid = new Node[dungeonWidth, dungeonHeight];
            for (int x = 0; x < dungeonWidth; x++)
            {
                for (int y = 0; y < DungeonHeight; y++)
                {
                    if (grid[x, y].name.Equals("Wall"))
                    {
                        nodeGrid[x, y] = new Node(false, new Vector2(x, y), x, y);
                        
                    }
                        
                    else
                        nodeGrid[x, y] = new Node(true, new Vector2(x, y), x, y);
                }
            }

        }

        public bool getWalkable(int x, int y)
        {
           return nodeGrid[x, y].walkable;
        }
        public void setNode(int x, int y, bool isWalkable)
        {
            nodeGrid[x, y].walkable = isWalkable;
        }

       public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.y);
            
            return nodeGrid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            
            for (int x = -1; x <=1; x++)
            {
                for (int y = -1; y <= 1; y++ )
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;
                                       
                    if (checkX >=0 && checkX < dungeonWidth && checkY >= 0 && checkY < dungeonHeight)
                    {
                        
                        neighbours.Add(nodeGrid[checkX, checkY]);
                       
                    }
                }
            }

            
            return neighbours;
        }
    }

}
