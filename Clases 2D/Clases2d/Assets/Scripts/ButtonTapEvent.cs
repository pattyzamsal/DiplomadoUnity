using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTapEvent : MonoBehaviour {

    public KirbyController kirbyController;

    public void ClickOnJumpButton() {
        kirbyController.executeJump();
    }

    public void ClickOnRestartButton() {
        SceneManager.LoadScene(0);
    }
}
