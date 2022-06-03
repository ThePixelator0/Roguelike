using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flying : MonoBehaviourPunCallbacks
{
    void Start() {
        GetComponent<SpriteRenderer>().sortingLayerName = "AboveUnits";
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Stop collisions with holes
        if (col.gameObject.name.Contains("Hole") ) {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }

        // Crate
        else if (col.gameObject.name.Contains("Crate") ) {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }

        // Torch
        else if (col.gameObject.name.Contains("Torch") ) {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }
}
