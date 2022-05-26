using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHUD : MonoBehaviour
{
    bool showingHUD = false;

    void FixedUpdate() {
        if (Input.GetAxis("Info") != 0 && !showingHUD) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
            showingHUD = true;
        } else if (Input.GetAxis("Info") == 0 && showingHUD) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            showingHUD = false;
        }
    }
}
