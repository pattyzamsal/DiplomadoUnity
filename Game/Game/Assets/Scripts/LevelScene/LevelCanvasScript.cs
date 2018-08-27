using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCanvasScript : MonoBehaviour {

	[Tooltip("Pass the play and exit button of the hierarchy")]
    public Button backBtn, lvl1Btn, lvl2Btn, lvl3Btn;

	// Use this for initialization
	void Start () {
		Button btn1 = backBtn.GetComponent<Button>();
        Button btn2 = lvl1Btn.GetComponent<Button>();
        Button btn3 = lvl2Btn.GetComponent<Button>();
        Button btn4 = lvl3Btn.GetComponent<Button>();

        btn1.onClick.AddListener(BackOnClick);
        btn2.onClick.AddListener(delegate { ChargeLevel(2); });
        btn3.onClick.AddListener(delegate { ChargeLevel(3); });
        btn4.onClick.AddListener(delegate { ChargeLevel(4); });
    }

	void BackOnClick() {
        SceneManager.LoadScene(0);
    }

    void ChargeLevel(int level) {
        SceneManager.LoadScene(level);
    }
}
