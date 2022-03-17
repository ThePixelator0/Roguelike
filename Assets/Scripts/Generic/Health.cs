using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private GameObject blood;


    void Awake() {
        health = maxHealth;
    }

    public void applyDamage(float damage) {
        health -= damage;
        bleed();
        checkAlive();
    }

    void bleed() {
        var bloodClone = Instantiate(blood, transform.position, Quaternion.identity);
    }

    void checkAlive() {
        if (health <= 0) {
            if (gameObject.tag == "Player") {
                print("Game Over!");
                Destroy(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
