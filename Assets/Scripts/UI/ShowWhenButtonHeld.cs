using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenButtonHeld : MonoBehaviour
{
    public string button = "Info";

    void FixedUpdate() {
        if (Input.GetAxis(button) != 0) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        } else {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }
    }
}
