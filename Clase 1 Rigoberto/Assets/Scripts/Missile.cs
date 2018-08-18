using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float missileVelocity;
    private Rigidbody missileBody;

	// Use this for initialization
	void Awake() {
        missileBody = this.gameObject.GetComponent<Rigidbody>();
        missileBody.velocity = Vector3.forward * missileVelocity;
	}
	
	// Update is called once per frame
	void Update () {
        missileBody.velocity = Vector3.forward * missileVelocity;
    }
}
