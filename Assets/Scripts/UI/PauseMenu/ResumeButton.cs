using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public bool resume;

    void Update() {
        if (resume && (Input.GetAxis("Primary") == 0)) {
            foreach (Transform child in transform.parent) {
                if (child != transform) {
                    child.gameObject.SetActive(false);
                }
            }
            Time.timeScale = 1;
            gameObject.SetActive(false);
            resume = false;
        }
    }

    public void Resume() {
        resume = true;
    }
}
