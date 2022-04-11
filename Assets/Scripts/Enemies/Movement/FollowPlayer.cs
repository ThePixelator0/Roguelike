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

    void Awake() {
        // Find player and store in var
        player = GameObject.FindWithTag("Player");
    }
    
    void Update() {
        if (player != null) {
            if (Vector2.Distance(transform.position, player.transform.position) <= distance) {
                Move(DirTo(player) * speed);
            } else {
                rb.velocity *= 0.5f;
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

    void Move(Vector2 inputs) {
        // Apply velocity
        rb.velocity = inputs;
    }


}
