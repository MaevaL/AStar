using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public bool walkable;
    public Vector3 gridPosition;

    public int gCost; //distance from starting node
    public int heuristicCost; // distance from end node

    public Node path;

    public Node(bool walkable, Vector2 gridPosition) {
        this.walkable = walkable;
        this.gridPosition = gridPosition;
    }

    public int fCost {
        get { return gCost + heuristicCost; }
    }

    public bool Equals(Node node) {
        if (gridPosition.x == node.gridPosition.x && gridPosition.z == node.gridPosition.z) {
            return true;
        } else {
            return false;
        }
    }
}
