using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {
    Grid grid;
    public List<Node> path;
    float timer = 10;
 
    private void Start() {
        GameObject gridObject = GameObject.FindGameObjectWithTag("grid");
        grid = gridObject.GetComponent<Grid>();
        Vector3 startPos = transform.position;
        Debug.Log(startPos);
        Vector3 targetPos = new Vector3(9, 0, 9);
        path = Pathfinding(startPos, targetPos);     
    }

    public List<Node> Pathfinding(Vector3 startPos , Vector3 targetPos) {
        // list of nodes already evaluate
        List<Node> openList = new List<Node>();

        // list of currently discovered nodes that are not evaluated yet
        List<Node> closedList = new List<Node>();
        
        Node startNode = grid.PosToNode(startPos);
        Node targetNode = grid.PosToNode(targetPos);

        startNode.heuristicCost = GetDistance(startNode, targetNode);
        startNode.gCost = 0;


        openList.Add(startNode);

        while (openList.Count > 0) {
            Node currentNode = openList[0];
            //current := the node in openSet having the lowest fScore[] value
            for (int i = 0; i < openList.Count; i++) {
                // if fcost are equals, compare heuristicCost
                openList[i].heuristicCost = GetDistance(openList[i] , targetNode);
                if (currentNode.fCost >= openList[i].fCost) {
                    if (currentNode.heuristicCost > openList[i].heuristicCost) {
                        currentNode = openList[i];
                    }
                }
            }

            // if the target is found return the path form start to end
            if (currentNode.Equals(targetNode)) {
                return ReconstructPath(startNode , targetNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<Node> neighbours = grid.GetNeighbours(currentNode);
            foreach (Node n in neighbours) {
                // ignore the nodes which are already evaluated and represent a wall
                if (closedList.Contains(n) || !n.walkable) { continue; }

                // discover a new Node
                if (!openList.Contains(n)) { openList.Add(n); }

                // distance from start to a neighbour
                float gCostToNeighbour = currentNode.gCost + GetDistance(n , currentNode);

                // this is a better path
                if (gCostToNeighbour < n.gCost) {
                    // record the path
                    n.gCost = gCostToNeighbour;
                    n.heuristicCost = GetDistance(n , targetNode);
                    n.link = currentNode;
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
        //14  for diagonal move
        //10  for vertical and horizontal move    
        if (distX > distZ) {
            return 14 * distZ + 10 * (distX - distZ);
        }
        else {
            return 14 * distX + 10 * (distZ - distX);
        }
    }

    public List<Node> ReconstructPath(Node start , Node end) {
        List<Node> path = new List<Node>();

        if (start.Equals(end)) {
            return path;
        }

        Node currentNode = end;

        // reconstruct path from the end to the start
        while (!currentNode.Equals(start)) {
            path.Add(currentNode);
            currentNode = currentNode.link;
        }

        path.Add(start);
        // path from start to end
        path.Reverse();
        return path;
    }

    // Default value for each nodes before aStar
    public void initNode() {
        foreach(Node n in grid.nodes) {
            n.heuristicCost = 0;
            n.gCost = Mathf.Infinity;
            n.link = null;
        }
    }
}
