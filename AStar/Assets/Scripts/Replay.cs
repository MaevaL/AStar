using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour {
    GameObject enemy;
    GameObject player;
	// Use this for initialization
	void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("r")) {
            enemy.transform.position = new Vector3(0 , 0 , 0);
            player.transform.position = new Vector3(9 , 0 , 9);
        }
	}
}
