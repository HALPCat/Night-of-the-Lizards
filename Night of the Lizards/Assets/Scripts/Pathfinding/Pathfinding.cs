using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LizardNight
{
    public class Pathfinding : MonoBehaviour
    {
        PathRequestManager requestManager;
        GridHandler grid;

        List<Node> path;

        private void Awake()
        {
            requestManager = GetComponent<PathRequestManager>();
            grid = GetComponent<GridHandler>();           
            
        }

     
        public void StartFindPath (Vector3 startPos, Vector3 endPos)
        {
            StartCoroutine(FindPath(startPos, endPos));
        }


        IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            
            Heap<Node> openSet = new Heap<Node>(grid.maxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            bool pathSuccess = false;
            bool isTarget = false;
            Vector3 waypoint= new Vector3();

            while (openSet.Count > 0)
            {

                Node node = openSet.RemoveFirst();
                closedSet.Add(node);
                
                if (node == targetNode)
                {
                    pathSuccess = true;                    
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(node))
                {
                    
                    if (!neighbour.walkable && neighbour != targetNode || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    

                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);

                    }
                }
            }
            yield return null;
            if (pathSuccess)
            {
               waypoint = RetracePath(startNode, targetNode);
               isTarget = waypoint == targetPos ? true : false;
                Debug.Log(isTarget);
            }

            requestManager.FinishedProcessingPath(waypoint, pathSuccess, isTarget);
        }

        Vector3 RetracePath (Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();

            grid.path = path;

            return path[0].worldPosition;
        }
        int GetDistance (Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        public void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i].worldPosition, Vector3.one);

                    //if (i == targetIndex)
                    //{
                    //    Gizmos.DrawLine(transform.position, path[i]);
                    //}
                    //else
                    //{
                    //    Gizmos.DrawLine(path[i - 1], path[i]);
                    //}
                }
            }
        }

    }
}
