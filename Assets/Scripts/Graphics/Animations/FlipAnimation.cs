using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAnimation : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool facingRight = true;

    void FixedUpdate() {
        if (rb.velocity.x > .01 && !facingRight) {
            Flip();
        } else if (rb.velocity.x < -.01 && facingRight) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        if (facingRight) transform.Rotate(new Vector3(0, -180, 0));
        else transform.Rotate(new Vector3(0, 180, 0));

        foreach (Transform child in transform) {
            if (facingRight) child.Rotate(new Vector3(0, -180, 0));
            else child.Rotate(new Vector3(0, 180, 0));
        }
    }
}
