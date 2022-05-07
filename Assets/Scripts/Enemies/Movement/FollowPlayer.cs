using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

[RequireComponent(typeof(StatController))]
public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public bool attacking;
    public Vector2 attackingDir;
    private Rigidbody2D rb;
    
    public Vector2 knockbackDir;
    private Vector2 lastPlayerPos;

    private StatController stats;
    LOS sight = new LOS();

    private List<Collider2D> ignoredCollisions;


    // private bool touchingHoleWalls = true;

    void Awake() {
        stats = GetComponent<StatController>();

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        lastPlayerPos = transform.position;
        ignoredCollisions = new List<Collider2D>();
    }
    
    void FixedUpdate() {
        if (player != null && stats != null) {
            // Attack Movement
            if (attacking && attackingDir != new Vector2()) {
                Move(DirTo(attackingDir) * stats.speed * -2f);
            }

            // Knockback if no attacking movement
            else if (Mathf.Abs(knockbackDir.x) > stats.speed || Mathf.Abs(knockbackDir.y) > stats.speed) {
                Move(knockbackDir);

                CheckKnockback();
            } 

            // If no knockback, attack stops movement
            else if (attacking && attackingDir == new Vector2()) {
                rb.velocity = new Vector3();
            }

            // If no attack, Normal Movement
            else if ((Vector2.Distance(transform.position, player.transform.position) <= stats.vision / PlayerStats.stealthMod) && (sight.PositionLOS(transform.position - new Vector3(0, 0.4f, 0), player.transform.position - new Vector3(0, 0.4f, 0), player.tag, gameObject.tag)) ){
                // Player is close enough to be seen and can be seen
                Move(DirTo(player.transform.position) * stats.speed);
                lastPlayerPos = player.transform.position;
            } else {
                // Player is either too far to be seen or is not in LOS
                float distance = Vector2.Distance(transform.position, lastPlayerPos);
                float distanceMod = distance > 1 ? 1 : distance;
                Move(DirTo(lastPlayerPos) * stats.speed * distanceMod);
            }
        } 
        // Player doesn't exist
        else {
            rb.velocity *= 0f;
        }

        // Undo collision ignoring
        if (knockbackDir == new Vector2() && ignoredCollisions.Count > 0) {
            foreach (Collider2D col in ignoredCollisions) {
                Physics2D.IgnoreCollision(col, GetComponent<Collider2D>(), false );
            }
            ignoredCollisions = new List<Collider2D>();
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
        if (!attacking) {
            knockbackDir = knockback;
        } else {
            // attacking = false;
        }
    }

    public void Interrupt() {
        if (attacking) {
            attacking = false;
            foreach (Transform child in transform) {
                child.SendMessage("Interrupt", null, SendMessageOptions.DontRequireReceiver);
            }
        }
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
                Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>(), true);
                ignoredCollisions.Add(col.collider);
            }


            else {
                // update knockback direction
                Vector2 damageVec = knockbackDir - rb.velocity;
                damageVec = new Vector2(Mathf.Abs(damageVec.x), Mathf.Abs(damageVec.y));

                if (damageVec.x > damageVec.y && damageVec.x >= 4) {
                    gameObject.SendMessage("applyDamage", damageVec.x * 5);

                } else if (damageVec.y > damageVec.x && damageVec.y >= 4) {
                    gameObject.SendMessage("applyDamage", damageVec.y * 5);

                }

                knockbackDir = rb.velocity;
            }
        }

        // If attacking
        else if (attacking && attackingDir != new Vector2()) {
            if (col.collider.tag == "Hole") {
                Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>(), true);
                ignoredCollisions.Add(col.collider);
            }   
        }
    }

    void CheckKnockback() {
        knockbackDir *= Mathf.Pow(Time.deltaTime, 1f / 120f);

        if (Mathf.Abs(knockbackDir.x) < stats.speed && Mathf.Abs(knockbackDir.y) < stats.speed) {
            knockbackDir = new Vector2(0f, 0f);
        }
    }
}
