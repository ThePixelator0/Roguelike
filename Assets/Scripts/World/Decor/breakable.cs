using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public float health;
    [SerializeField]
    private Vector3 particleOffset;
    [SerializeField]
    private bool particles;
    [SerializeField]
    private bool deathEffect;
    
    void CheckAlive() {
        if (health <= 0) {
            if (particles) {
                gameObject.SendMessage("DeathParticles", particleOffset);
            }

            if (deathEffect) {
                gameObject.SendMessage("DeathEffect");
            }

            Destroy(gameObject);
        }
    }

    public void applyDamage(float damage) {
        health -= damage;

        CheckAlive();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Environment") {
            Destroy(gameObject);
        }
    }
}
