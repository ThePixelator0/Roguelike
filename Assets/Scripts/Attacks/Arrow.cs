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

    public void setParent(GameObject projectileParent) {
        transform.parent = projectileParent.transform;
    }

    void Update() {
        Move(FacingtoVec((double)gameObject.transform.rotation.eulerAngles.z + rotationOffset), speed);
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
        // Not on the team of the creator (Such as player or enemy)
        if (col.tag != creator.tag) {
            col.SendMessage("applyDamage", damage);
            Destroy(gameObject);
        }
    }
}
