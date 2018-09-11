using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigEnemyController : MonoBehaviour {

    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private bool activateMovement = false;
    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        calcuateNewMovementVector();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose()) {
            activateMovement = true;
        }
        if (activateMovement)
        {
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y);
        }
        else {
        }
    }

    void calcuateNewMovementVector() {
        movementDirection = new Vector2(-1f, 0).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    bool playerIsClose() {
        return Vector2.Distance(player.transform.position, this.transform.position) < 5f;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case "Trap":
                if (Mathf.Min(collision.gameObject.transform.position.x, this.transform.position.x) == this.transform.position.x) {
                    activateMovement = false;
                }
                break;
            default:
                break;
        }
    }
}