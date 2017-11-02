using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {
    private Vector3 startPos;
    private Vector3 targetPos;
    private List<Node> cameFrom;

    Grid grid;

    private void Awake() {
        startPos = ClickEvent.startPos;
        targetPos = ClickEvent.targetPos;
    }
    private List<Node> Pathfinding(Vector3 startPos , Vector3 targetPos) {
        Node startNode = grid.PosToNode(startPos);
        Node targetNode = grid.PosToNode(targetPos);

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(startNode);

        while(openList.Count > 0) {
            Node currentNode = openList[0];
            //current := the node in openSet having the lowest fScore[] value
            for(int i = 0; i < openList.Count; i++) {
                if(currentNode.fCost >= openList[i].fCost) {
                    if(currentNode.heuristicCost > openList[i].heuristicCost) {
                        currentNode = openList[i];
                    }
                }
            }

            //if current = goal
            //return reconstruct_path(cameFrom , current)

            if(currentNode.Equals(targetNode)) {
                return ReconstructPath(startNode , targetNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<Node> neighbours = grid.GetNeighbours(currentNode);
            foreach(Node n in neighbours){
                if (closedList.Contains(n) || !n.walkable) {
                    continue;
                }
                if (!openList.Contains(n)) {
                    openList.Add(n);
                }

                // The distance from start to a neighbor
                //tentative_gScore := gScore[current] + dist_between(current, neighbor)
                //if tentative_gScore >= gScore[neighbor]
                //  continue		
                // This is not a better path.

                int gCostToNeighbour = currentNode.gCost + GetDistance(n , targetNode);
                if(gCostToNeighbour < n.gCost || !openList.Contains(n)) {
                    n.gCost = gCostToNeighbour;
                    n.heuristicCost = GetDistance(n , targetNode);
                    n.path = currentNode;
                }

                if (!openList.Contains(n)) {
                    openList.Add(n);
                }
            }
        }

    }

    private int GetDistance(Node n1, Node n2) {
        int distX = Mathf.Abs((int)n1.gridPosition.x - (int)n2.gridPosition.x);
        int distZ = Mathf.Abs((int)n1.gridPosition.z - (int)n2.gridPosition.z);

        if(distX > distZ) {
            return 14 * distZ + 10 * (distX - distZ);
        }
        else {
            return 14 * distX + 10 * (distZ - distX);
        }
    }

    private List<Node> ReconstructPath(Node n1, Node n2) {
        List<Node> path = new List<Node>();
        Node targetNode = grid.PosToNode(targetPos);
        Node startNode = grid.PosToNode(startPos);
        Node currentNode = targetNode;
        while(currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.path;
        }

        return path.Reverse();
    }
}
