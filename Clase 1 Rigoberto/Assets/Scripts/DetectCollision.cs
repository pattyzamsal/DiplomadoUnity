using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour {

    public Camera cam;
    public SelfRotation earth;
    public float rotationSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                //Debug.Log("You selected the " + hit.transform.name);
                if (hit.transform.name == "Earth") {
                    //Debug.Log("tierra");
                    earth.rotationSpeed = rotationSpeed;
                }
            }
        }
    }
}
