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
    public Slider slider;
    public GameObject panelGameOver;
    public GameObject panelWin;

    private bool isMoving = false;
    private Animator kirbyAnimator;
    private SpriteRenderer kirbySprite;
    private Rigidbody2D kirbyRigidbody;
    private AudioSource kirbyAudioSource;
    private int score = 0;
    private float life = 1;
    private float minusLife = 0.2f;
    private bool gameOver = false;
    private bool win = false;

	// Use this for initialization
	void Awake () {
        kirbySprite = this.gameObject.GetComponent<SpriteRenderer>();
        kirbyRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        kirbyAudioSource = this.gameObject.GetComponent<AudioSource>();
        kirbyAnimator = this.gameObject.GetComponent<Animator>();
        slider.value = life;
        panelGameOver.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameOver && !win) {
            isMoving = false;
            // We detect when the user is pressing
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(Vector2.right * Time.deltaTime * velocity);
                isMoving = true;
                kirbySprite.flipX = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(Vector2.left * Time.deltaTime * velocity);
                isMoving = true;
                kirbySprite.flipX = true;
            }
            kirbyAnimator.SetBool("kirbyIsMoving", isMoving);
        } else {
            if (gameOver) {
                panelGameOver.SetActive(true);
            }
            if (win) {
                panelWin.SetActive(true);
            }
        }
    }

	void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
            executeJump();
        }
	}

    public void executeJump() {
        kirbyRigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        kirbyAudioSource.Play();
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
                if (score == 500) {
                    win = true;
                }
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
            case "Enemy":
                startKirbyAnimation(true);
                kirbyRigidbody.AddForce(Vector2.right * -2, ForceMode2D.Impulse);
                life -= minusLife;
                slider.value = life;
                Debug.Log(life);
                if (life <= 0.0f) {
                    gameOver = true;
                }
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
