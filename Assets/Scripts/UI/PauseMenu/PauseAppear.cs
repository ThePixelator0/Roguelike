using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAppear : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetAxis("Pause") != 0) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }
}
