using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamDamage : MonoBehaviour
{
    // Damage objects when they move into a wall fast enough
    Knockback kb;
    Rigidbody2D rb;

    void Awake() {
        kb = GetComponentInChildren(typeof(Knockback)) as Knockback;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (!(col.collider.gameObject.tag == "Environment" || col.collider.gameObject.tag == "Breakable")) return;
        if (rb.velocity.magnitude < kb.NPCmovement.maxSpeed) return;

        SendMessage("applyDamage", rb.velocity.magnitude * 4);
        BroadcastMessage("Stun", .8);
    }
}
