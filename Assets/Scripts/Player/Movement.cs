using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Normal Movement
    public float speed = 5;     // Speed of normal movement

    // Dash
    [Space]
    public bool canDash = false;
    private float dashMultiplier = 2f;      // How fast dash is
    private Vector2 dashMod;                // Which direction dashing
    public float dashCooldown = 0f;         // How long until next dash available
    private float timeBetweenDashes = 2f;   // How long between start of each dash
    private float dashLength = 0.15f;       // How many seconds a dash lasts


    [Space]
    // External Components
    [SerializeField]
    private Rigidbody2D rb;     // Local var of RigidBody2D (Physics for player)

    // Knockback
    [SerializeField]
    private Vector2 knockbackDir;


    void Start() {
        dashMod = new Vector2(0, 0);
        dashCooldown = 0f;

    }


    void Update() {
        // Dashing Currently Disabled (hence the "&& false")
        
        if (Mathf.Abs(knockbackDir.x) > speed / 2 || Mathf.Abs(knockbackDir.y) > speed / 2) {
            // Being Knocked Back
            Move(knockbackDir);
            knockbackDir *= Mathf.Pow(Time.deltaTime, 1f / 120f);

        } else {
            // Not Being Knocked Back
            if (GetDashing() && dashCooldown == 0 && canDash) {
                dashMod = InitDash(GetInputs(), dashMultiplier) * PlayerStats.speedMod;
            }
            Move(GetInputs() * speed * PlayerStats.speedMod, dashMod);
        }
    }

    void FixedUpdate() {
        Cooldowns();
    }

    Vector2 GetInputs() {
        // Get the inputs for vertical and horizontal movement
        Vector2 inputs;

        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");

        inputs = inputs.normalized;

        return inputs;
    }

    bool GetDashing() {
        // Get input from Dash button and turn it into a boolean
        bool dash;

        dash = Input.GetAxis("Dash") > 0 ? true : false;

        return dash;
    }

    Vector2 InitDash(Vector2 inputs, float dashModifier) {
        dashCooldown = timeBetweenDashes;

        Vector2 dashVec = inputs * dashModifier * speed;

        return dashVec;
    }

    void Move(Vector2 moveVec, Vector2 dashVec = new Vector2() ) {
        // Apply velocity
        rb.velocity = moveVec + dashVec;
    }

    void Cooldowns() {
        // Dash Cooldowns
        if (dashCooldown > 0) {
            dashCooldown -= Time.deltaTime;

            if (dashCooldown < 0) {
                dashCooldown = 0;
            }
        }
        if (dashCooldown < timeBetweenDashes - dashLength && dashMod != new Vector2(0, 0)) {
            // Stop Dash
            dashMod = new Vector2(0, 0);
        }

        // Clear Knockback
        if ((Mathf.Abs(knockbackDir.x) < speed / 2 && Mathf.Abs(knockbackDir.y) < speed / 2) && (knockbackDir != new Vector2() )) {
            knockbackDir = new Vector2(0f, 0f);
        }
        
    }

    public void applyKnockback(Vector2 knockback) {
        knockbackDir = knockback;
    }

    void OnCollisionEnter2D() {
        if (knockbackDir != new Vector2()) {
            knockbackDir = rb.velocity;
        }
    }
}
