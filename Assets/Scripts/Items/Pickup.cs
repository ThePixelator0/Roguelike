using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemnum;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            col.SendMessage("PickupItem", itemnum);
            Destroy(gameObject);
        }
    }
}
