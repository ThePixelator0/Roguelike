using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sprintModifier;
    [SerializeField]
    private Rigidbody2D rb;

    void Update()
    {
        Move(GetInputs() * speed, GetSprinting(), sprintModifier);
    }

    Vector2 GetInputs() {
        // Get the inputs for vertical and horizontal movement
        Vector2 inputs;

        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");

        inputs = inputs.normalized;

        return inputs;
    }

    // TODO: Replace Sprinting with a roll / dash ability
    bool GetSprinting() {
        // Get input from Sprint button and turn it into a boolean
        bool sprint;

        sprint = Input.GetAxis("Sprint") > 0 ? true : false;

        return sprint;
    }

    void Move(Vector2 inputs, bool sprinting, float sprintMod) {
        // Apply velocity
        rb.velocity = sprinting ? inputs * sprintMod : inputs;  // If sprinting then apply sprintMod. Otherwise normal speed.
    }
}
