using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour {

	public float velocity;
	public float jump;

    private bool isMoving = false;
    private Animator kirbyAnimator;
    private SpriteRenderer kirbySprite;
    private Rigidbody2D kirbyRigidbody;
    private AudioSource kirbyAudioSource;

	// Use this for initialization
	void Awake () {
        kirbySprite = this.gameObject.GetComponent<SpriteRenderer>();
        kirbyRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        kirbyAudioSource = this.gameObject.GetComponent<AudioSource>();
        kirbyAnimator = this.gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        isMoving = false;
        // We detect when the user is pressing
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(Vector2.right * Time.deltaTime * velocity);
            isMoving = true;
            kirbySprite.flipX = false;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(Vector2.left * Time.deltaTime * velocity);
            isMoving = true;
            kirbySprite.flipX = true;
        }
        kirbyAnimator.SetBool("kirbyIsMoving", isMoving);
    }

	void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
            kirbyRigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            kirbyAudioSource.Play();
        }
	}

    private void startKirbyAnimation(bool isStart) {
        kirbyAnimator.SetBool("kirbyIsOnFloor", isStart);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        startKirbyAnimation(true);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        startKirbyAnimation(false);
    }
}
