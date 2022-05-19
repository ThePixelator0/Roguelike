using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    public GameObject creator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private double rotationOffset;
    public float damage;
    private List<GameObject> alreadyHit;
    [SerializeField] private List<string> ignoredTags;
    private Vector2 attackDir;
    public float kbStr;
    float timeToDeath = 10;

    void Awake() {
        transform.parent = GameObject.Find("Parents/Projectiles").transform;
        alreadyHit = new List<GameObject>();
        attackDir = transform.rotation * Vector3.up;
    }

    public void SetCreator(GameObject creatorToSet) {
        creator = creatorToSet;
    }

    public void SetInfo(float info = 0f) {
        /*  infoVector
            x = speed modifier
            y = damage modifier
            z = knockback modifier
        */
        damage *= info;
        speed *= info;
        kbStr *= info;
        timeToDeath *= info;
    }

    void Move(Vector2 dir, float speed) {
        rb.velocity = dir * speed;
    }

    void FixedUpdate() {
        Move(FacingtoVec((double)gameObject.transform.rotation.eulerAngles.z + 90), speed);
        if (timeToDeath < 0) {
            Destroy(gameObject);
        } else {
            timeToDeath -= Time.fixedDeltaTime;
        }
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

        // Prevent multiple triggers from the same object
        if (alreadyHit.Contains(col.gameObject)) {
            return;
        } else {
            alreadyHit.Add(col.gameObject);
        }

        if (ignoredTags.Contains(col.tag)) {

        }


        // Collision with Environment
        else if (col.tag == "Environment") {
            Destroy(gameObject);
        }
        
        // Collision with hole
        else if (col.tag == "Hole") {
            Physics2D.IgnoreCollision(col, GetComponent<Collider2D>());
        }

        // Doesn't have the same tag as it's creator (No Friendly Fire)
        else if (col.tag != creator.tag && creator != null) {
            col.SendMessage("applyDamage", damage);
            if (col.tag != "Breakable") {
                Vector2 awarePos = creator.transform.position;
                col.BroadcastMessage("MakeAware", awarePos);
                col.BroadcastMessage("applyKnockback", attackDir * kbStr);
                Destroy(gameObject);
            } else {
            }
        }
        
    }
}
