using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    // This script controls which weapon the enemy has and can use.
    [SerializeField]
    private EnemyWeapon weapon;
    // public FollowPlayer movement;

    private GameObject player;

    public float attackWarmup;
    public float timeActive;
    public float timeInactive;
    public float attackCooldown;

    void Start() {
        player = GameObject.FindWithTag("Player");

        // sword.GetComponent<AttackMelee>().EnableSword(0);
        // shield.GetComponent<Shield>().EnableShield();
    }

    void FixedUpdate() {
        if (player == null) return;

        if (timeActive == 0 && timeInactive == 0 && attackCooldown == 0 && attackWarmup == 0) {
            if (Vector2.Distance(transform.position, player.transform.position) <= weapon.attackRange) {
                // movement.attacking = true;
                // movement.attackingDir = new Vector2();
                Attack();
            } 
        } 
        
        else if (attackWarmup != 0) {
            attackWarmup -= Time.deltaTime;
            if (attackWarmup <= 0) attackWarmup = 0;
            
        } else if (timeActive != 0) {
            timeActive -= Time.deltaTime;
            if (timeActive <= 0) timeActive = 0;
            
        } else if (timeInactive != 0) {
            timeInactive -= Time.deltaTime;
            if (timeInactive <= 0) {
                timeInactive = 0;
                // movement.attacking = false;
            }
            
        } else if (attackCooldown != 0) {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0) attackCooldown = 0;
        }


    }

    void Attack() {
        weapon.Attack(DirTowardsPos(player.transform.position));

        attackWarmup = weapon.warmupTime;
        timeActive = weapon.activeCooldown;
        timeInactive = weapon.inactiveCooldown;
        attackCooldown = weapon.normalCooldown;
    }


    public void Interrupt() {
        attackWarmup = 0;
        timeActive = 0;
        timeInactive = 0;
    }

    Vector3 DirTowardsPos(Vector2 pos, float offset = 0f) {
        // Face towards pos
        Vector2 objectPos = transform.position;

        pos.x -= objectPos.x;
        pos.y -= objectPos.y;

        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, (angle - 90) + offset);
    }
}
