using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    public GameObject creator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private double rotationOffset;
    public float damage;

    void Move(Vector2 dir, float speed) {
        rb.velocity = dir * speed;
    }

    void Update() {
        Move(FacingtoVec((double)gameObject.transform.rotation.eulerAngles.z + 90), speed);
    }

    Vector2 FacingtoVec(double degrees) {
        Vector2 vec;
        float x, y;

        degrees *= Math.PI / 180;

        x = (float)Math.Cos(degrees);
        y = (float)Math.Sin(degrees);

        vec = new Vector2(x, y);

        return vec;
    }

    // Collided with a Damage Hitbox
    void OnTriggerEnter2D(Collider2D col) {
        // Collision with Environment
        if (col.tag == "Environment") {
            Destroy(gameObject);
        }

        // Collided with Room Generation
        else if (col.tag == "SpawnPoint") {

        }
        
        // Doesn't have the same tag as it's creator (No Friendly Fire)
        else if (col.tag != creator.tag) {
            col.SendMessage("applyDamage", damage);
            Destroy(gameObject);
        }
        
    }
}
