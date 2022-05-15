using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerWeapon : MonoBehaviour
{
    
    public PlayerWeaponController controller;

    [SerializeField]
    private TilemapRenderer selfRend;
    [SerializeField]
    private BoxCollider2D selfCol;

    Vector3 posOffset;

    public int attackType;
        // 0 = Jab
    
    [Space]
    [Header("Weapon Stats")]
    public float warmupTime;            // How long before attack starts. -1 = this is a charge weapon
    public float activeCooldown;        // First part of attack
    public float inactiveCooldown;      // Second part of attack
    public float normalCooldown;        // how long before input can be made

    public float warmupSpeedMod;        // Speed Mod during weapon warmup[Space]
    [Space]
    public float chargeTimeMin;        // The minimum amout of time the weapon can be charged for.
    public float chargeTimeMax;        // The maximum amout of time the weapon can be charged for.
    public float chargeTime;            // How long the weapon was charged for (for charged weapons)

    bool doneWarmup = false;
    bool doneActive = false;
    bool doneInactive = false;
    bool doneNormal = false;

    void Start() {
        controller = transform.parent.GetComponent<PlayerWeaponController>();
    }

    void Update() {
        if (controller.attackWarmup > 0 || controller.attackWarmup == -1) {
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
            case 1:
                // Punch
                chargeTime = 0f;
                PlayerStats.speedMod += warmupSpeedMod;
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

    Vector2 RotationToVector() {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 direction = Vector3.up;

        posOffset = new Vector3(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos,
            0 );
        return posOffset.normalized;
    }


    // --------------- Attack Details ---------------

    void WarmupDetails(int type) {

        switch (type) {
            case 0:
                // Jab
                if (!doneWarmup) {
                    selfRend.enabled = true;
                }
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
            case 1:
                // Punch
                if (!doneWarmup) {
                    selfRend.enabled = true;
                }
                transform.position = transform.parent.position;
                transform.rotation = Quaternion.Euler(controller.DirToMouse());
                controller.timeActive = chargeTime / 2;   // Punch lasts for half time spent charging
                break;
        }
    }

    void ActiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                if (!doneActive) {
                    selfCol.enabled = true;
                }
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
            case 1:
                // Punch
                if (chargeTime < chargeTimeMin) {
                    controller.timeActive = 0;
                }
                else if (!doneActive) {
                    selfCol.enabled = true;
                    controller.movement.rb.AddForce(RotationToVector() * 4000 * chargeTime);
                    controller.conditions.Immune(controller.timeActive);
                }

                break;
        }
    }

    void InactiveDetails(int type) {
        if (!doneInactive) {
            selfCol.enabled = false;
        }

        switch (type) {
            case 0:
                // Jab
                if (!doneInactive) {
                    
                }
                transform.position = transform.parent.position + (posOffset * (controller.timeInactive - inactiveCooldown) * 2);
                break;
            case 1:
                // Punch
                if (!doneInactive) {
                    
                }
                break;
        }
    } 

    void CooldownDetails(int type) {
        if (!doneNormal) {
            selfRend.enabled = false;
        }

        switch (type) {
            case 0:
                // Jab
                if (!doneNormal) {
                    
                }
                break;
            case 1:
                // Punch
                if (!doneNormal) {
                    
                }
                
                break;
        }
    }
}
