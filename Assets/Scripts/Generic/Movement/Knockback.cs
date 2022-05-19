using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public PlayerMovement movement;
    public NPCMovement NPCmovement;

    public Vector2 kbDir;

    public void applyKnockback(Vector2 knockback) {
        // print("Knockback recieved: " + knockback);
        kbDir = knockback;

        if (NPCmovement != null) {
            NPCmovement.rb.AddForce(knockback);
        } else {
            movement.rb.AddForce(knockback);
        }
    }

}
