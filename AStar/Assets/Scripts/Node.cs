using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public bool walkable;
    public Vector3 gridPosition;

    public float gCost = Mathf.Infinity; //distance from starting node
    public float heuristicCost; // distance from end node

    public Node link; // define a link between nodes, used for reconstruct the path from the target to the start

    public Node(bool walkable, Vector3 gridPosition) {
        this.walkable = walkable;
        this.gridPosition = gridPosition;
    }

    // total cost for each node of getting from the start to the target. Partly known, partly heuristic
    public float fCost {
        get { return gCost + heuristicCost; }
    }

    public bool Equals(Node node) {
        if (gridPosition.x == node.gridPosition.x && gridPosition.z == node.gridPosition.z) {
            return true;
        } else {
            return false;
        }
    }

    public string tostring() {
        string s = " ( " + walkable + " ) ( " + gridPosition.x + "," + gridPosition.y + "," + gridPosition.z + " )";
        return s;
    }
}
