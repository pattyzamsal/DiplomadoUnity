using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour {

    public Text score;
    public GameObject panelWin;
    public GameObject panelLose;
    public Button retryInLose, retryInWin, levels, next;

    private GameObject player;
    private PlayerController playerController;
    private Scene actualScene;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void Start() {
        actualScene = SceneManager.GetActiveScene();
        Button btn1 = retryInLose.GetComponent<Button>();
        Button btn2 = retryInWin.GetComponent<Button>();
        Button btn3 = levels.GetComponent<Button>();
        Button btn4 = next.GetComponent<Button>();

        btn1.onClick.AddListener(RetryOnClick);
        btn2.onClick.AddListener(RetryOnClick);
        btn3.onClick.AddListener(LevelsOnClick);
        btn4.onClick.AddListener(NextOnClick);
    }

    void RetryOnClick() {
        SceneManager.LoadScene(actualScene.name);
    }

    void LevelsOnClick() {
        SceneManager.LoadScene("LevelScene");
    }

    void NextOnClick() {
        SceneManager.LoadScene(actualScene.buildIndex + 1);
    }

    // Update is called once per frame
    void Update () {
        score.text = "Score: " + playerController.score.ToString();
        if (playerController.isGameOver) {
            StartCoroutine(showLosePanel());
        }
	}

    IEnumerator showLosePanel() {
        yield return new WaitForSeconds(1);
        panelLose.SetActive(true);
    }
}
