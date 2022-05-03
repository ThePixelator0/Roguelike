using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class exit : MonoBehaviour
{
    private bool active = false;
    [SerializeField]
    private TilemapRenderer rend;
    [SerializeField]
    private BoxCollider2D selfCol;

    bool triggered = false;

    void Start() {
        rend.enabled = false;
        selfCol.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (triggered || !active || Input.GetAxis("Interact") == 0) return;

        if (col.tag == "Player") {
            triggered = true;
            GameObject.FindWithTag("GameController").SendMessage("NextLevel");
        }
    }

    public void ShowExit() {
        rend.enabled = true;
        selfCol.enabled = true;
        active = true;
    }
}
