using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public PlayerMovement movement;


    public void applyKnockback(Vector2 knockback) {
        movement.rb.AddForce(knockback);
    }
}
