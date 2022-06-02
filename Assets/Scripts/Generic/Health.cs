using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private HealthBar healthBar;

    bool player = false;
    StatController stats;


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

        if (gameObject.tag == "Player") player = true;
        else stats = GetComponent<StatController>();
    }

    public void applyDamage(float damage) {
        if (player) {
            if (PlayerStats.canTakeDamage) health -= damage * PlayerStats.weaknessMod;
        }
        else if (stats.canTakeDamage) health -= damage * stats.weaknessMod;

        healthBar.SetHealth(health);
        if (gameObject.tag == "Enemy") {
            healthBar.FadeAway(2f);
        }

        bleed();
        CheckAlive();
    }

    public void applyHealing(float heal) {
        health += heal;
        if (health > maxHealth) {
            health = maxHealth;
        }
        healthBar.SetHealth(health);
    }

    public void SetHealth(float newHealth) {
        health = newHealth;
        healthBar.SetHealth(health);

        CheckAlive();
    }

    void bleed() {
        var bloodClone = PhotonNetwork.Instantiate(blood.name, transform.position, Quaternion.identity);
    }

    void CheckAlive() {
        if (health <= 0) {
            if (gameObject.tag == "Player") {
                print("Game Over!");
                Destroy(gameObject);
            } 

            else if (gameObject.tag == "Boss") {
                gameObject.SendMessage("Die_Boss"); 
            } 
            
            else {
                Destroy(gameObject);
            }
        }
    }
}
