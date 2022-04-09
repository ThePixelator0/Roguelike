using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;    // Speed of normal movement

    [SerializeField]
    private float dashMultiplier;   // How fast dash is
    [SerializeField]
    private Vector2 dashMod;    // Which direction dashing
    [SerializeField]
    private float dashCooldown; // How long until next dash available
    [SerializeField]
    private float timeBetweenDashes;    // How long between start of each dash
    [SerializeField]
    private float dashLength;   // How many seconds a dash lasts

    [SerializeField]
    private Rigidbody2D rb; // Local var of RigidBody2D (Physics for player)

    void Start() {
        dashMod = new Vector2(0, 0);
        dashCooldown = 0f;
    }

    void Update()
    {
        if (GetDashing() && dashCooldown == 0) {
            dashMod = InitDash(GetInputs(), dashMultiplier);
        }
        Move(GetInputs() * speed, dashMod);
        
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

    void Move(Vector2 inputs, Vector2 dashVec) {
        // Apply velocity
        rb.velocity = inputs + dashVec;
    }

    void Cooldowns() {
        if (dashCooldown > 0) {
            dashCooldown -= Time.deltaTime;
        }
        if (dashCooldown < 0) {
            dashCooldown = 0;
        }
        if (dashCooldown < timeBetweenDashes - dashLength && dashMod != new Vector2(0, 0)) {
            dashMod = new Vector2(0, 0);
        }
    }
}
