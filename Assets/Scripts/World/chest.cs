using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject open;
    public GameObject closed;

    public int reward;
    
    void Start() {
        closed.SetActive(true);
        open.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            Open(col);
        }
    }

    void Open(Collider2D col) {
        closed.SetActive(false);
        open.SetActive(true);

        if (reward == -1) {
            col.SendMessage("applyDamage", 69420);
        }
    }
}
