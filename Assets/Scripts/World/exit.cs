using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            GameObject.FindWithTag("GameController").SendMessage("NextLevel");
        }
    }
}
