using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            Open();
        }
    }
}
