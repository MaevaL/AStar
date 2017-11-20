using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public Vector3 gridSize;
    private int rnd;
    public Node[,] nodes;


    // Use this for initialization
    void Awake() {
        GenerateGridPacMan();
    }

    private void GenerateGridPacMan() {
        nodes = new Node[(int)gridSize.x , (int)gridSize.z];
        foreach (Transform child in transform) {
            Vector3 posChild = child.transform.position;
            if (child.CompareTag("unWalkable")) {
                nodes[(int)posChild.x , (int)posChild.z] = new Node(false , posChild);
            }
            else {
                nodes[(int)posChild.x , (int)posChild.z] = new Node(true , posChild);
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i == 0 && j == 0) { continue; }

                int neighbourX = (int)node.gridPosition.x + i;
                int neighbourZ = (int)node.gridPosition.z + j;

                if (neighbourX >= 0 && neighbourX < gridSize.x) {
                    if (neighbourZ >= 0 && neighbourZ < gridSize.z) {
                        neighbours.Add(nodes[neighbourX, neighbourZ]);
                    }
                }
            }
        }

        return neighbours;
    }

    public Node PosToNode(Vector3 pos) {
        return nodes[(int)pos.x, (int)pos.z];
    }
}
