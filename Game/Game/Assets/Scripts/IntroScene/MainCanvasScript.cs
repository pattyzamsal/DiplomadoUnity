using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MainCanvasScript : MonoBehaviour {

	[Tooltip("Pass the play and exit button of the hierarchy")]
    public Button playBtn, exitBtn;

	void Start () {
        Button btn1 = playBtn.GetComponent<Button>();
        Button btn2 = exitBtn.GetComponent<Button>();

        btn1.onClick.AddListener(PlayOnClick);
        btn2.onClick.AddListener(ExitOnClick);
	}

    void PlayOnClick() {
        SceneManager.LoadScene(1);
    }

    void ExitOnClick() {
        #if UNITY_EDITOR
        if(EditorApplication.isPlaying) {
          UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        Application.Quit();
    }
}
