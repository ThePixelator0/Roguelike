using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemnum;
    public float pickupDelay = 1;

    void fixedUpdate() {
        if (pickupDelay > 0) {
            pickupDelay -= Time.deltaTime;
        }
    }


    void OnTriggerStay2D(Collider2D col) {
        if (pickupDelay > 0) {
            return;
        }

        if (col.tag == "Player") {
            PlayerItems.PickupItem(itemnum);
            Destroy(gameObject);
        }
    }
}
