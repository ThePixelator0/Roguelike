using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField]
    private float maxHealth;


    void Awake() {
        health = maxHealth;
    }

    public void applyDamage(float damage) {
        health -= damage;
        checkAlive();
    }

    void checkAlive() {
        if (health <= 0) {
            print("Game Over!");
            Destroy(gameObject);
        }
    }
}
