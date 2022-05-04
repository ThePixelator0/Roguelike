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

    Vector3 posOffset;

    public int attackType;
        // 0 = Jab
    public float attackRange;
        // Range of the attack
    
    public float warmupTime;
    public float activeCooldown;
    public float inactiveCooldown;
    public float normalCooldown;

    void Update() {
        if (controller.attackWarmup > 0) {
            WarmupDetails(attackType);
        }
        else if (controller.timeActive > 0) {
            ActiveDetails(attackType);
        } 
        else if (controller.timeInactive > 0) {
            InactiveDetails(attackType);
        } 
        else {
            CooldownDetails(attackType);
        }
    }

    public void Attack(Vector3 attackDir) {
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

        posOffset = new Vector3(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos,
            0 );
    }





    void WarmupDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                selfRend.enabled = true;
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
        }
    }

    void ActiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                controller.movement.attackingDir = transform.position;
                selfCol.enabled = true;
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
        }
    }

    void InactiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                selfCol.enabled = false;
                if (controller.timeInactive > inactiveCooldown / 2) {
                    controller.movement.attackingDir = transform.position;
                } else {
                    controller.movement.attackingDir = new Vector2();
                }
                transform.position = transform.parent.position + (posOffset * (controller.timeInactive - inactiveCooldown) * 2);
                break;
            
        }
    } 

    void CooldownDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                selfRend.enabled = false;
                break;
        }
    }
}
