using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerWeaponController controller;

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

    public float warmupSpeedMod;        // Speed Mod during weapon warmup
    [HideInInspector]
    public float chargeTime;            // How long the weapon was charged for (for charged weapons)


    void Start() {
        controller = transform.parent.GetComponent<PlayerWeaponController>();
    }

    void Update() {
        if (controller.attackWarmup > 0 || controller.attackWarmup == -1) {
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
        // This is called when the attack begins

        switch (attackType) {
            case 0:
                // Jab
                transform.rotation = Quaternion.Euler(attackDir);
                controller.movement.attacking = true;
                controller.movement.attackingDir = new Vector2();
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


    // --------------- Attack Details ---------------

    void WarmupDetails(int type) {
        // PlayerStats.speedMod += warmupSpeedMod;

        switch (type) {
            case 0:
                // Jab
                selfRend.enabled = true;
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
            case 1:
                // Punch
                selfRend.enabled = true;
                transform.position = transform.parent.position;
                transform.rotation = Quaternion.Euler(controller.DirToMouse());
                controller.movement.attackingDir = transform.up * 15f;
                controller.timeActive = chargeTime < 2 ? chargeTime / 4 : 0.5f;   // Charge time caps at .8s, punch lasts for charge time ^ 3 seconds. (max is 0.8^3 or .4096)
                break;
        }
    }

    void ActiveDetails(int type) {
        switch (type) {
            case 0:
                // Jab
                // controller.movement.attackingDir = transform.position;
                selfCol.enabled = true;
                transform.position = transform.parent.position - (posOffset * controller.timeActive * 2);
                break;
            case 1:
                // Punch
                selfCol.enabled = true;
                controller.movement.attacking = true;
                break;
        }
    }

    void InactiveDetails(int type) {
        selfCol.enabled = false;
        
        switch (type) {
            case 0:
                // Jab
                transform.position = transform.parent.position + (posOffset * (controller.timeInactive - inactiveCooldown) * 2);
                break;
            case 1:
                // Punch
                
                controller.movement.attackingDir = new Vector2();
                break;
        }
    } 

    void CooldownDetails(int type) {
        selfRend.enabled = false;
        controller.movement.attacking = false;

        switch (type) {
            case 0:
                // Jab
                break;
            case 1:
                // Punch
                
                break;
        }
    }
}
