using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    List<Node> path;
    AStar pathfinder;
    GameObject player;
    bool firstTurn;
    public bool isPlayed;
    int cpt;

    void Start() {
        pathfinder = GetComponent<AStar>();
        player = GameObject.FindGameObjectWithTag("Player");
        isPlayed = false;
        firstTurn = true;
        cpt = 0;

    }

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<MovePlayer>().isPlayed == true || firstTurn == true) {
    
            firstTurn = false;
            Vector3 startPos = transform.position;
            Vector3 targetPos = player.transform.position;

            if(startPos != targetPos) {
                path = pathfinder.Pathfinding(startPos , targetPos);
                pathfinder.initNode();

                isPlayed = true;
                player.GetComponent<MovePlayer>().isPlayed = false;
                transform.position = path[1].gridPosition;
            }
        }
    }
}
