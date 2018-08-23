using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		mainCamera.transform.position = new Vector3(this.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
	}
}
