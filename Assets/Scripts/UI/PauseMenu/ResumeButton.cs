using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public void Resume() {
        foreach (Transform child in transform.parent) {
            if (child != transform) {
                child.gameObject.SetActive(false);
            }
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
