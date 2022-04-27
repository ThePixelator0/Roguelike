using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(Resume);
    }

    public void Resume() {
        print("resume");
        foreach (Transform child in transform.parent) {
            if (child != transform) {
                child.gameObject.SetActive(false);
            }
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
