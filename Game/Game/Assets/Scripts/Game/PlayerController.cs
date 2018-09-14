using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Tooltip("Pass the main camera of the scene")]
	public Camera mainCamera;	

	private float velocityMovement = 2.0f;
    private float forceJump = 8.0f;
    private bool isOnFloor = true;
    private bool isJump = false;
    private bool isWalk = false;
    private bool isBark = false;

	private SpriteRenderer playerSprite;
	private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

	// Use this for initialization
	void Awake () {
		playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
		playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(Vector2.right * Time.deltaTime * velocityMovement);
            playerSprite.flipX = false;
            isWalk = true;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(Vector2.left * Time.deltaTime * velocityMovement);
            playerSprite.flipX = true;
            isWalk = true;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            Debug.Log("excavar");
        } else if (Input.GetKeyDown(KeyCode.E)) {
            isBark = true;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            isBark = false;
        } else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            isWalk = false;
        }
        activateAnimation();
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
            case "Platform":
                isOnFloor = true;
                isJump = false;
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case "Platform":
                isOnFloor = false;
                isJump = true;
                break;
            default:
                break;
        }
    }

    private void activateAnimation() {
        playerAnimator.SetBool("isJump", isJump);
        playerAnimator.SetBool("isOnFloor", isOnFloor);
        playerAnimator.SetBool("isWalk", isWalk);
        playerAnimator.SetBool("isBark", isBark);
    }
}
