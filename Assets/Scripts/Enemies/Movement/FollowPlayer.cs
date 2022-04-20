using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

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

    LOS sight = new LOS();

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
                if (sight.PositionLOS(transform.position - new Vector3(0, 0.4f, 0), player.transform.position - new Vector3(0, 0.4f, 0), player.tag, gameObject.tag) ) {
                    Move(DirTo(player) * speed);
                } else {
                    rb.velocity *= 1 - (Time.deltaTime * 2);
                }
            } else if ( rb.velocity != new Vector2(0, 0) ) {
                rb.velocity *= 1 - (Time.deltaTime * 2);
            }
        } else {
            rb.velocity *= 1 - (Time.deltaTime * 2);
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
