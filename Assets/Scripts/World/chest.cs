using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject open;
    public GameObject closed;
    
    void Start() {
        closed.SetActive(true);
        open.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            Open();
        }
    }

    void Open() {
        closed.SetActive(false);
        open.SetActive(true);
    }
}
