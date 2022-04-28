using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemnum;
    public float pickupDelay = 0.5f;
    private bool pickedUp = false;
    Vector3 dirToMoveTo;

    void FixedUpdate() {
        if (pickupDelay > 0) {
            pickupDelay -= Time.deltaTime;
            transform.position += dirToMoveTo * Time.deltaTime;
        }
    }

    void Start() {
        dirToMoveTo = (GameObject.Find("Player").transform.position - transform.position).normalized;
    }


    void OnTriggerStay2D(Collider2D col) {
        if (pickupDelay > 0 || Input.GetAxis("Interact") == 0) return;

        if (col.tag == "Player") {
            // Prevent the same item being picked up multiple times
            if (pickedUp) {
                return;
            } else {
                pickedUp = true;
            }

            PlayerItems.PickupItem(itemnum);
            Destroy(gameObject);
        }
    }
}
