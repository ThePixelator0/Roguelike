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
    private Vector2 lastPlayerPos;
    
    private bool touchingShield;

    LOS sight = new LOS();


    private bool touchingHoleWalls = true;

    void Awake() {
        // Find player and store in var
        touchingShield = false;
        player = GameObject.FindWithTag("Player");
        lastPlayerPos = transform.position;
    }
    
    void FixedUpdate() {
        if (player != null && !touchingShield) {
            if (Mathf.Abs(knockbackDir.x) > speed || Mathf.Abs(knockbackDir.y) > speed) {
                Move(knockbackDir);

                CheckKnockback();
            }  
            
            else if (Vector2.Distance(transform.position, player.transform.position) <= distance / PlayerStats.stealthMod) {
                if (sight.PositionLOS(transform.position - new Vector3(0, 0.4f, 0), player.transform.position - new Vector3(0, 0.4f, 0), player.tag, gameObject.tag) ) {
                    Move(DirTo(player.transform.position) * speed);
                    lastPlayerPos = player.transform.position;
                } else {
                    Move(DirTo(lastPlayerPos) * speed);
                }
            } else {
                Move(DirTo(lastPlayerPos) * speed);
            }
        } else {
            rb.velocity *= 0f;
        }
    }

    Vector2 DirTo(Vector2 pos) {
        Vector2 vec;
        vec = new Vector2(pos.x - gameObject.transform.position.x, pos.y - gameObject.transform.position.y);
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

    void OnCollisionEnter2D(Collision2D col) {
        // If being knocked back,
        if (knockbackDir != new Vector2()) {
            // hit an enemy or boss,
            if (col.collider.tag == "Enemy" || col.collider.tag == "Boss") {
                // knock them back
                col.collider.gameObject.SendMessage("applyKnockback", knockbackDir);
            } 
            
            else if (col.collider.tag == "Hole") {
                if (col.collider.gameObject.name == "Hole") {
                    Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
                    touchingHoleWalls = true;
                }
            }


            else {
                // update knockback direction
                Vector2 damageVec = knockbackDir - rb.velocity;
                damageVec = new Vector2(Mathf.Abs(damageVec.x), Mathf.Abs(damageVec.y));

                if (damageVec.x > damageVec.y && damageVec.x >= 4) {
                    // print("impact speed: " + damageVec.x);
                    gameObject.SendMessage("applyDamage", damageVec.x * 5);
                } else if (damageVec.y > damageVec.x && damageVec.y >= 4) {
                    // print("impact speed: " + damageVec.y);
                    gameObject.SendMessage("applyDamage", damageVec.y * 5);
                }

                knockbackDir = rb.velocity;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.tag == "Hole") {
            if (col.gameObject.name == "Darkness" && !touchingHoleWalls) {
                gameObject.SendMessage("applyDamage", 100000);
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.collider.gameObject.name == "Hole") {
            touchingHoleWalls = false;
        }
    }

    void CheckKnockback() {
        knockbackDir *= Mathf.Pow(Time.deltaTime, 1f / 120f);

        if (Mathf.Abs(knockbackDir.x) < speed && Mathf.Abs(knockbackDir.y) < speed) {
            knockbackDir = new Vector2(0f, 0f);
        }
    }
}
