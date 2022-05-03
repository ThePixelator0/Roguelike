using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField]
    private List<Collider2D> enemiesTouchingHole;

    void OnTriggerEnter2D(Collider2D col) {
        print(col.gameObject + " is touching trigger");
    }

    void OnCollisionEnter2D(Collision2D col) {
        print(col.gameObject + " is touching collider");
    }
}
