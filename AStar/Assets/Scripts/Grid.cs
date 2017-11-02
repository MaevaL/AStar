using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject cell;
    public GameObject unWalkableCell;
    public Vector3 gridSize;
    private int rnd;
    Node[,] nodes;


    // Use this for initialization
    void Start() {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update() {

    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for (int i = -1; i <= 1; i++) {
            for (int j = -14; j <= 1; j++) {
                if (i == 0 && j == 0) { continue; }

                int neighbourX = (int)node.gridPosition.x + i;
                int neighbourZ = (int)node.gridPosition.z + j;

                if (neighbourX >= 0 && neighbourX < gridSize.x) {
                    if(neighbourZ >= 0 && neighbourZ < gridSize.z) {
                        neighbours.Add(nodes[neighbourX , neighbourZ]);
                    }
                }
            }
        }

        return neighbours;
    }

    public Node PosToNode(Vector3 pos) {
        return nodes[(int)pos.x , (int)pos.z];
    }
    private void GenerateGrid() {
        nodes = new Node[(int)gridSize.x , (int)gridSize.z];

        for (int i = 0; i < gridSize.x; i++) {
            for (int j = 0; j < gridSize.z; j++) {
                rnd = Random.Range(1 , 10);
                if (rnd == 1) {
                    Instantiate(unWalkableCell , new Vector3(i , 0 , j) , Quaternion.identity);
                    nodes[i , j] = new Node(false , new Vector3(i , 0 , j));
                }
                else {
                    Instantiate(cell , new Vector3(i , 0 , j) , Quaternion.identity);
                    nodes[i , j] = new Node(true , new Vector3(i , 0 , j));
                }
            }
        }
    }
}
