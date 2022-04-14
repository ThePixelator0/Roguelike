using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public float health;
    
    void CheckAlive() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void applyDamage(float damage) {
        health -= damage;

        CheckAlive();
    }
}
