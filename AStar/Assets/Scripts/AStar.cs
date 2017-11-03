using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {
    Grid grid;

    private void Awake() {
        grid = GetComponent<Grid>();
    }
    public List<Node> Pathfinding(Vector3 startPos , Vector3 targetPos) {
        // list of nodes already evaluate
        List<Node> openList = new List<Node>();

        // list of currently discovered nodes that are not evaluated yet
        List<Node> closedList = new List<Node>();

        // the cost from going from start to start is 0
        Node startNode = grid.PosToNode(startPos);
        startNode.gCost = 0;

        Node targetNode = grid.PosToNode(targetPos);

        openList.Add(startNode);

        while (openList.Count > 0) {
            Node currentNode = openList[0];
            //current := the node in openSet having the lowest fScore[] value
            for (int i = 0; i < openList.Count; i++) {
                // if fcost are equals, compare heuristicCost
                if (currentNode.fCost >= openList[i].fCost) {
                //    if (currentNode.heuristicCost > openList[i].heuristicCost) {
                        currentNode = openList[i];
                //    }
                }
            }


            // if the target is found return the path form start to end
            if (currentNode.Equals(targetNode)) {
                Debug.Log("...");
                return ReconstructPath(startNode , targetNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<Node> neighbours = grid.GetNeighbours(currentNode);
           // Debug.Log("n" + neighbours.Count);
            foreach (Node n in neighbours) {
                // ignore the nodes which are already evaluated and represent a wall
                if (closedList.Contains(n) || !n.walkable) { continue; }

                // discover a new Node
                if (!openList.Contains(n)) { openList.Add(n); }

                // distance from start to a neighbour
                int gCostToNeighbour = currentNode.gCost + GetDistance(n , currentNode);
                Debug.Log(n.gCost);
                Debug.Log(GetDistance(n , currentNode));
                Debug.Log(gCostToNeighbour);

                // this is a better path
                if (gCostToNeighbour < n.gCost || !openList.Contains(n)) {
                    // record the path
                    n.gCost = gCostToNeighbour;
                    n.heuristicCost = GetDistance(n , targetNode);
                    n.link = currentNode;
                    Debug.Log(n.link.tostring());
                }

                if (!openList.Contains(n)) {
                    openList.Add(n);
                }
            }
        }
        return openList;
    }

    public int GetDistance(Node n1 , Node n2) {
        // horizontal
        int distX = Mathf.Abs((int)n1.gridPosition.x - (int)n2.gridPosition.x);
        // vertical
        int distZ = Mathf.Abs((int)n1.gridPosition.z - (int)n2.gridPosition.z);


        //calcul need the biggest dist
        //14 is for diagonal move
        //10 is for vertical and horizontal move    
        if (distX > distZ) {
            return 14 * distZ + 10 * (distX - distZ);
        }
        else {
            return 14 * distX + 10 * (distZ - distX);
        }
    }

    public List<Node> ReconstructPath(Node n1 , Node n2) {
        List<Node> path = new List<Node>();
        Debug.Log("g");
        Node targetNode = grid.PosToNode(ClickEvent.targetPos);
        Node startNode = grid.PosToNode(ClickEvent.startPos);
        Node currentNode = targetNode;

        // reconstruct path from the end to the start
        while (!currentNode.Equals(startNode)) {
            Debug.Log("j");
            path.Add(currentNode);
            currentNode = currentNode.link;
        }

        // path from start to end
        Debug.Log(path.Count);
        path.Reverse();
        Debug.Log(path.Count);
        grid.pathFromStartToTarget = path;

        return path;
    }
}
