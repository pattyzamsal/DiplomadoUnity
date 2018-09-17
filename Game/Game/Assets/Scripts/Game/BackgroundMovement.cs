using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {

    public float xVelocity = 0.08f;

    private Material materialBackground;
    private Vector2 offset;
    private GameObject player;
    private PlayerController playerScript;
    private SpriteRenderer playerSprite;

    private void Awake() {
        materialBackground = GetComponent<Renderer>().material;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerSprite = player.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerScript.isWalk) {
            offset = new Vector2(xVelocity, 0);
            if (playerSprite.flipX) {
                offset = -offset;
            }
            materialBackground.mainTextureOffset += offset * Time.deltaTime;
        }
	}
}
