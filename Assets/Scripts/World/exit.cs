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

    void Start() {
        rend.enabled = false;
        selfCol.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (active == false) {
            return;
        }

        if (col.tag == "Player") {
            GameObject.FindWithTag("GameController").SendMessage("NextLevel");
        }
    }

    public void ShowExit() {
        rend.enabled = true;
        selfCol.enabled = true;
        active = true;
    }
}
