using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour {
    public Camera camera;
    private int cpt;
    private Ray ray;
    private RaycastHit hit;
    public static Vector3 startPos;
    public static Vector3 targetPos;

    // Use this for initialization
    void Start() {
        cpt = 0;
    }

    // Update is called once per frame
    void Update() {
        if (cpt < 2) {
            if (Input.GetMouseButtonDown(0)) {
                ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray , out hit)) {
                    Transform objectHit = hit.transform;
                    if (objectHit.CompareTag("unWalkable")) { return; }
                    cpt += 1;
                    if (cpt == 1) { startPos = new Vector3(objectHit.transform.position.x , 0 , objectHit.transform.position.z); }
                    else { targetPos = new Vector3(objectHit.transform.position.x , 0 , objectHit.transform.position.z); ; }
                    objectHit.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
    }
}

