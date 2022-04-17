using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
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
            // Player and Boss health bars never disappear
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
        CheckAlive();
    }

    public void SetHealth(float newHealth) {
        health = newHealth;
        healthBar.SetHealth(health);

        CheckAlive();
    }

    void bleed() {
        var bloodClone = Instantiate(blood, transform.position, Quaternion.identity);
    }

    void CheckAlive() {
        if (health <= 0) {
            if (gameObject.tag == "Player") {
                print("Game Over!");
                Destroy(gameObject);
            } 

            else if (gameObject.tag == "Boss") {
               gameObject.SendMessage("BossDie"); 
            } 
            
            else {
                Destroy(gameObject);
            }
        }
    }
}
