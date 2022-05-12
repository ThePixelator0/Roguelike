using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    [Space]
    public float speed = 5000;                 // Speed of normal movement
    public float maxSpeed = 4;
    
    Vector2 inputs;

    void Update() {
        inputs = GetInputs();
    }

    void FixedUpdate() {
        if (rb.velocity.magnitude <= maxSpeed * PlayerStats.speedMod) {
            rb.AddForce(inputs * speed * Time.fixedDeltaTime * PlayerStats.speedMod);
        }
        
        animator.SetFloat("Speed_X", rb.velocity.x);
        animator.SetFloat("Speed_Y", rb.velocity.y);
    }

    
    public Vector2 GetInputs() {
        // Get the inputs for vertical and horizontal movement
        Vector2 inputs;

        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");

        inputs = inputs.normalized;

        return inputs;
    }
}
