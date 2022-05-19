using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownHole : MonoBehaviour
{
    // void OnCollisionEnter2D(Collision2D col) {
    //     if (col.collider.gameObject.tag != "Player") {
    //         NPCMovement movement = col.collider.gameObject.GetComponentInChildren(typeof(NPCMovement)) as NPCMovement;
    //         print(col.relativeVelocity.magnitude);
    //         if (col.relativeVelocity.magnitude > movement.maxSpeed) Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
    //     }
    // }

    // void OnCollisionExit2D(Collision2D col) {
    //     Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>(), false);
    // }
}
