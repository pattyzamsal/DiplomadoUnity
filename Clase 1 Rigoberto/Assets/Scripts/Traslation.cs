using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traslation : MonoBehaviour {

    public Transform pivotPosition;
    public float traslationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // pivote del sol, eje y, velocidad rotacion
        this.transform.RotateAround(pivotPosition.position, Vector3.up, Time.deltaTime * traslationSpeed);
	}
}
