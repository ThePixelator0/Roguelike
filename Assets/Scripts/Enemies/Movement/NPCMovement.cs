using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

public class NPCMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    // public Animator animator;
    private GameObject player;
    [Space]
    public float speed = 5000;                 // Speed of normal movement
    public float maxSpeed = 4;

    public bool canMove = true;

    StatController stats; 

    Vector2 moveDir;
    Vector2 playerLastSeenPos;
    LOS sight = new LOS();

    void Awake() {
        playerLastSeenPos = transform.position;
        player = GameObject.Find("Player");
        stats = transform.parent.GetComponent<StatController>();
    }

    void FixedUpdate() {
        if (player != null && canMove) {
            if (canSee(player.transform.position - new Vector3(0, 0.4f, 0))) {
                playerLastSeenPos = player.transform.position - new Vector3(0, 0.4f, 0);
            } 

            moveDir = DirTo(playerLastSeenPos);

            if (rb.velocity.magnitude <= maxSpeed * stats.speedMod) {   // If not moving faster than max speed
                if (Vector2.Distance(playerLastSeenPos, transform.position) > 1) {  // if close enough to position of player last seen
                    rb.AddForce(moveDir * speed * Time.fixedDeltaTime * stats.speedMod);
                }
            }

            // Un-Comment when naxx is out (Animations added)
            // animator.SetFloat("Speed_X", rb.velocity.x);
            // animator.SetFloat("Speed_Y", rb.velocity.y);
        }
    }


    Vector2 DirTo(Vector2 pos) {
        // Takes a position Vector and returns a directional vector towards it
        Vector2 vec;
        vec = new Vector2(pos.x - gameObject.transform.position.x, pos.y - gameObject.transform.position.y);
        vec = vec.normalized;

        return vec;
    }

    bool canSee(Vector2 checkPos) {
        // Returns true if this gameobject can see the Vector2

        // Check is player is close enough to see
        if (Vector2.Distance(transform.position - new Vector3(0, 0.4f, 0), checkPos) > stats.vision / PlayerStats.stealthMod) return false;

        // Check LOS
        return sight.PositionLOS(transform.position, checkPos, stats.tag, "Player");
    }
}
