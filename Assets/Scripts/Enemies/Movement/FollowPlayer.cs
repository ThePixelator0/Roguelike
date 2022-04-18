using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float distance; // Max distance the entity can see the player from.
    public Vector2 knockbackDir;

    void Awake() {
        // Find player and store in var
        player = GameObject.FindWithTag("Player");
    }
    
    void FixedUpdate() {
        if (player != null) {
            if (Mathf.Abs(knockbackDir.x) > speed || Mathf.Abs(knockbackDir.y) > speed) {
                Move(knockbackDir);
                knockbackDir *= Mathf.Pow(Time.deltaTime, 1f / 120f);

                if (Mathf.Abs(knockbackDir.x) < speed && Mathf.Abs(knockbackDir.y) < speed) {
                    knockbackDir = new Vector2(0f, 0f);
                }
            }  
            
            else if (Vector2.Distance(transform.position, player.transform.position) <= distance / PlayerStats.stealthMod) {
                Move(DirTo(player) * speed);
            } else if ( rb.velocity != new Vector2(0, 0) ) {
                rb.velocity = new Vector2(0, 0);
            }
        } else {
            rb.velocity *= 0f;
        }
    }

    Vector2 DirTo(GameObject obj) {
        Vector2 vec;
        vec = new Vector2(obj.transform.position.x - gameObject.transform.position.x, obj.transform.position.y - gameObject.transform.position.y);
        vec = vec.normalized;

        return vec;
    }

    void Move(Vector2 vel) {
        // Set velocity
        rb.velocity = vel;
    }

    public void applyKnockback(Vector2 knockback) {
        knockbackDir = knockback;
    }
}
