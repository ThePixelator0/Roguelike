using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_Darkness : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag != "Enemy" && col.tag != "Boss") return;

        if (col.gameObject != null) {
            col.gameObject.SendMessage("applyDamage", 69420);
        }
    }

    
}
