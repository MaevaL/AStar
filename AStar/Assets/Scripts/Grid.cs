using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public GameObject cell;
    public Vector3 gridSize;

	// Use this for initialization
	void Start () {
        GenerateGrid();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateGrid() {
        for(int i = 0; i < gridSize.x; i++) {
            for(int j = 0; j < gridSize.z; j++) {
                
                Instantiate(cell, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
