using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour {
    public Camera camera;
    private int cpt;
    private Ray ray;
    private RaycastHit hit;
    private Transform start;
    private Transform end;

    // Use this for initialization
    void Start() {
        cpt = 0; 
    }

    // Update is called once per frame
    void Update() {
        if (cpt < 2) {
            if (Input.GetMouseButtonDown(0)) {
            cpt += 1;
            
                ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) {
                    Transform objectHit = hit.transform;
                    if(cpt == 1) {
                        start = objectHit;
                    } else {
                        end = objectHit;
                    }
                    objectHit.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
           
        
    }
}

