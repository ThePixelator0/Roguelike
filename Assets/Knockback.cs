using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public PlayerMovement movement;
    public NPCMovement NPCmovement;

    Vector2 kbDir;

    public void applyKnockback(Vector2 knockback) {
        kbDir = knockback;

        if (NPCmovement != null) {
            NPCmovement.rb.AddForce(knockback);
        } else {
            movement.rb.AddForce(knockback);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (movement.rb.velocity.magnitude < 4) return;

        Vector2 contactPoint = col.GetContact(0).normal;

        // This is very confusing, come back to it later

        // If the collision is within 45 degrees of the knockback direction
        if (contactPoint.y <= kbDir.y + 0.5 || contactPoint.y >= kbDir.y - 0.5) {
            if (contactPoint.x <= kbDir.x + 0.5 || contactPoint.x >= kbDir.x - 0.5) {
                print("Slam Damage!");
                transform.parent.SendMessage("applyDamage", movement.rb.velocity.magnitude * 10);
            }
        }
    }
}
