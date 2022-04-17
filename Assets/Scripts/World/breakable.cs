using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public float health;
    [SerializeField]
    private Vector3 particleOffset;
    
    void CheckAlive() {
        if (health <= 0) {
            gameObject.SendMessage("DeathParticles", particleOffset);
            Destroy(gameObject);
        }
    }

    public void applyDamage(float damage) {
        health -= damage;

        CheckAlive();
    }
}
