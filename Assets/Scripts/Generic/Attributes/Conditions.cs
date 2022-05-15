using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{
    // NPC
    NPCMovement npc_movement;
    EnemyWeaponController npc_weapon;
    StatController stats;

    // PC
    bool player;
    PlayerMovement movement;
    PlayerWeaponController weapon;
    
    // Condition Timers
    public float stunTime;
    public float immuneTime;



    void Awake() {
        // Get needed components from the Game Object
        if (gameObject.tag == "Player") {
            player = true;
            movement = GetComponentInChildren(typeof(PlayerMovement)) as PlayerMovement;
            weapon = GetComponentInChildren(typeof(PlayerWeaponController)) as PlayerWeaponController;
        } else {
            npc_movement = GetComponentInChildren(typeof(NPCMovement)) as NPCMovement;
            npc_weapon = GetComponentInChildren(typeof(EnemyWeaponController)) as EnemyWeaponController;
            stats = GetComponent<StatController>();
        }
    }

    void FixedUpdate() {
        // Stun
        if (stunTime > 0) {
            stunTime -= Time.fixedDeltaTime;

            if (stunTime <= 0) {
                stunTime = 0;
                if (!player) npc_movement.canMove = true;
                else movement.canMove = true;
            }
        }

        
        // Immune
        if (immuneTime > 0) {
            immuneTime -= Time.fixedDeltaTime;

            if (immuneTime <= 0) {
                immuneTime = 0;
                if (player) PlayerStats.canTakeDamage = true;
                else stats.canTakeDamage = true;
            }
        }
    }

    // ------------------------------------------------------------ Conditions ------------------------------------------------------------

    public void Stun(float time = 0) {
        Interrupt();

        stunTime += time;

        if (!player) {
            npc_weapon.attackCooldown += time;
            npc_movement.canMove = false;
        } else {
            weapon.attackCooldown += time;
            movement.canMove = false;
        }
    }

    public void Interrupt() {
        if (!player) {
            npc_weapon.attackWarmup = 0.01f;
            npc_weapon.timeActive = 0.01f;
            npc_weapon.timeInactive = 0.01f;
        } else {
            weapon.attackWarmup = 0.01f;
            weapon.timeActive = 0.01f;
            weapon.timeInactive = 0.01f;
        }
    }

    public void Immune(float length) {
        immuneTime += length;

        if (player) PlayerStats.canTakeDamage = false;
        else stats.canTakeDamage = false;
    }
}
