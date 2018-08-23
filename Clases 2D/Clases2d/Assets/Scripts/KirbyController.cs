using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour {

	public float velocity;
	public float jump;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// We detect when the user is pressing
		if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.Translate(Vector2.right * Time.deltaTime * velocity);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.Translate(Vector2.left * Time.deltaTime * velocity);
		}
	}

	void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jump, ForceMode2D.Impulse);
		}
	}
}
