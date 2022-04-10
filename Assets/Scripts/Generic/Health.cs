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
    [SerializeField]
    private HealthBar healthBar;


    void Awake() {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (gameObject.tag == "Enemy") {
            // Enemy Health bars disappear at start
            healthBar.FadeAway(0f);
        }
        if (gameObject.tag == "Player" || gameObject.tag == "Boss") {
            // Player and Boss health bars always appear
            healthBar.FadeAway(-69f);
        }
    }

    public void applyDamage(float damage) {
        health -= damage;
        healthBar.SetHealth(health);
        if (gameObject.tag == "Enemy") {
            healthBar.FadeAway(2f);
        }

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
