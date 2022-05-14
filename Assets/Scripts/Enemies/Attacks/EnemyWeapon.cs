using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LineOfSight;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyWeaponController controller;

    [SerializeField]
    private TilemapRenderer selfRend;
    [SerializeField]
    private BoxCollider2D selfCol;
    [SerializeField]
    private StatController stats;
    [SerializeField]
    private NPCMovement movement;

    Vector3 posOffset;
    Vector2 attackingDir;
    [Space]
    [Space]

    public int attackType;
        // 0 = Jab
    public float attackRange;
        // Range of the attack
    
    public float warmupTime;
    public float activeCooldown;
    public float inactiveCooldown;
    public float normalCooldown;

    bool doneWarmup = false;
    bool doneActive = false;
    bool doneInactive = false;
    bool doneNormal = false;

    void Update() {
        if (controller.attackWarmup > 0) {
            WarmupDetails(attackType);
            doneWarmup = true;
        }
        else if (controller.timeActive > 0) {
            ActiveDetails(attackType);
            doneActive = true;
        } 
        else if (controller.timeInactive > 0) {
            InactiveDetails(attackType);
            doneInactive = true;
        } 
        else if (controller.attackCooldown > 0) {
            CooldownDetails(attackType);
            doneNormal = true;
        }
    }

    public void Attack(Vector3 attackDir) {
        // Start Attack
        doneWarmup = false;
        doneActive = false;
        doneInactive = false;
        doneNormal = false;

        switch (attackType) {
            case 0:
                // Jab
                transform.rotation = Quaternion.Euler(attackDir);
                setPosOffset();
                break;
        }
    }

    void setPosOffset() {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 direction = Vector3.up;
        direction = new Vector3(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos,
            0 );

        posOffset = direction; 
        attackingDir = direction;
    }


    // -----------------------------------------------------------------------


    void WarmupDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                if (!doneWarmup) {
                    selfRend.enabled = true;
                    stats.speedMod -= 0.7f;
                }

                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
        }
    }

    void ActiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                if (!doneActive) {
                    selfCol.enabled = true;
                    movement.rb.AddForce(attackingDir * 675);   // Jump towards attacking direction
                }

                // controller.movement.attackingDir = transform.position;
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
        }
    }

    void InactiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                if (!doneInactive) {
                    selfCol.enabled = false;
                }
                
                // if (controller.timeInactive > inactiveCooldown / 2) {
                //     controller.movement.attackingDir = transform.position;
                // } else {
                //     controller.movement.attackingDir = new Vector2();
                // }
                transform.position = transform.parent.position + (posOffset * (controller.timeInactive - inactiveCooldown) * 2);
                break;
            
        }
    } 

    void CooldownDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                if (!doneNormal) {
                    selfRend.enabled = false;
                    stats.speedMod += 0.7f;
                }

                break;
        }
    }
}
