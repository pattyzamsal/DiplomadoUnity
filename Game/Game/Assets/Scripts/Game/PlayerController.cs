using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Tooltip("Pass the main camera of the scene")]
	public Camera mainCamera;	

	private float velocityMovement = 2.0f;
	private float forceJump = 5.0f;

	private SpriteRenderer playerSprite;
	private Rigidbody2D playerRigidbody;

	// Use this for initialization
	void Awake () {
		playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
		playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(Vector2.right * Time.deltaTime * velocityMovement);
            playerSprite.flipX = false;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(Vector2.left * Time.deltaTime * velocityMovement);
            playerSprite.flipX = true;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            // TODO excute animation of bark
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            Debug.Log("excavar");
        }
    }

	void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
            playerRigidbody.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }
	}

	void LateUpdate () {
		mainCamera.transform.position = new Vector3(this.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case "Outside":
                Debug.Log("It is an outside of the world");
                break;
            case "BigEnemy":
                Debug.Log("Big enemy");
                break;
            default:
                break;
        }
    }

}
