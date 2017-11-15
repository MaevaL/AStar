using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    Grid grid;
    public bool isPlayed;
    GameObject enemy;
    bool firstTurn;
    private void Start() {
        grid = FindObjectOfType<Grid>();
        isPlayed = false;
        firstTurn = true;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update () {
        if (enemy.GetComponent<MoveEnemy>().isPlayed == true) {
            if (Input.GetKeyDown("left")) {
                if (grid.PosToNode(transform.position + new Vector3(-1 , 0 , 0)).walkable == true) {
                    transform.position += new Vector3(-1 , 0 , 0);
                    isPlayed = true;
                }
            }
            if (Input.GetKeyDown("right")) {
                if (grid.PosToNode(transform.position + new Vector3(1 , 0 , 0)).walkable == true) {
                    transform.position += new Vector3(1 , 0 , 0);
                    isPlayed = true;
                }
            }
            if (Input.GetKeyDown("down")) {
                if (grid.PosToNode(transform.position + new Vector3(0 , 0 , -1)).walkable == true) {
                    transform.position += new Vector3(0 , 0 , -1);
                    isPlayed = true;
                }
            }
            if (Input.GetKeyDown("up")) {
                if (grid.PosToNode(transform.position + new Vector3(0 , 0 , 1)).walkable == true) {
                    transform.position += new Vector3(0 , 0 , 1);
                    isPlayed = true;
                }
            }
        }
        //enemy.GetComponent<MoveEnemy>().isPlayed = false;


    }
}
