using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KirbyController : MonoBehaviour {

	public float velocity;
	public float jump;
    public AudioSource coinsSound;
    public GameObject parentExplosion;
    public Text scoreText;

    private bool isMoving = false;
    private Animator kirbyAnimator;
    private SpriteRenderer kirbySprite;
    private Rigidbody2D kirbyRigidbody;
    private AudioSource kirbyAudioSource;
    private int score = 0;

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

    private void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case "Coin":
                coinsSound.Play();
                GameObject newExplosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
                newExplosion.transform.position = collision.gameObject.transform.position;
                newExplosion.name = "NewExplosion";
                newExplosion.transform.parent = parentExplosion.transform;
                score += 100;
                scoreText.text = "Score: " + score.ToString();
                Destroy(collision.gameObject);
                Destroy(newExplosion, 5);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.collider.gameObject.tag;
        switch (tag) {
            case "Platform":
                startKirbyAnimation(true);
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        string tag = collision.collider.gameObject.tag;
        switch (tag) {
            case "Platform":
                startKirbyAnimation(false);
                break;
            default:
                break;
        }
    }
}
