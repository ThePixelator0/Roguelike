using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class PlayerWeapon : MonoBehaviourPunCallbacks, IPunObservable
{
    
    public PlayerWeaponController controller;
    public attackSpawner projectileSpawner;

    public TilemapRenderer selfRend;
    public BoxCollider2D selfCol;

    Vector3 posOffset;

    public int attackType;
        // 0 = Uncharged Melee
        // 1 = Charged Melee
        // 2 = Charged Projectile
        // 3 = Uncharged Projectile
    
    
    [Space]
    [Header("Weapon Stats")]
    public float warmupTime;            // How long before attack starts. -1 = this is a charge weapon
    public float activeCooldown;        // First part of attack
    public float inactiveCooldown;      // Second part of attack
    public float normalCooldown;        // how long before input can be made

    public float warmupSpeedMod;        // Speed Mod during weapon warmup[Space]
    [Space]
    public float chargeTimeMin;         // The minimum amout of time the weapon can be charged for.
    public float chargeTimeMax;         // The maximum amout of time the weapon can be charged for.
    public float chargeTime;            // How long the weapon was charged for (for charged weapons)

    bool doneWarmup = false;
    bool doneActive = false;
    bool doneInactive = false;
    bool doneNormal = false;
    [Space]
    [Header("Projectile Stats")]
    public Projectile spawnedProjectile;// What the projectile is
    public float spawnDistance;         // Distance from the center of the player and where the projectile is spawned

    public Vector2 attackAngle;         // Angle of the attack

    void Start() {
        controller = transform.parent.GetComponent<PlayerWeaponController>();
    }

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // Sync Data
        if (stream.IsWriting) {
            // We are writing
            stream.SendNext(selfRend.enabled);
            stream.SendNext(selfCol.enabled);
        }
        else {
            // We are reading
            selfRend.enabled = (bool)stream.ReceiveNext();
            selfCol.enabled = (bool)stream.ReceiveNext();
        }
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
        
        PlayerStats.speedMod += warmupSpeedMod;

        switch (attackType) {
            case 0:
                // Jab
                transform.rotation = Quaternion.Euler(attackDir);
                setPosOffset();
                attackAngle = RotationToVector();
                break;
            case 1:
                // Punch
                chargeTime = 0f;
                break;
            case 2:
                // Bow
                chargeTime = 0f;
                break;
            case 3:
                // Magic
                transform.rotation = Quaternion.Euler(attackDir);
                setPosOffset();
                attackAngle = RotationToVector();
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
            case 2:
                // Bow
                if (!doneWarmup) {
                    selfRend.enabled = true;
                }
                transform.position = transform.parent.position;
                transform.rotation = Quaternion.Euler(controller.DirToMouse());
                break;
            case 3:
                // Magic
                if (!doneWarmup) {
                    selfRend.enabled = true;
                }
                transform.position = transform.parent.position;
                break;
        }
    }

    void ActiveDetails(int type) {
        if (!doneActive) {
            // print("Changing speed mod from " + PlayerStats.speedMod + " to " + (PlayerStats.speedMod - warmupSpeedMod));
            PlayerStats.speedMod -= warmupSpeedMod;

        }

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
                    attackAngle = RotationToVector();
                    controller.movement.rb.AddForce(RotationToVector() * 4000 * chargeTime);
                    controller.conditions.Immune(controller.timeActive);
                }
                break;
            case 2:
                // Bow
                if (chargeTime < chargeTimeMin) {
                    controller.timeActive = 0;
                }
                else if (!doneActive) {
                    // The bow is already pointing towards the mouse from the charge up time
                    projectileSpawner.SpawnAttack(spawnDistance * attackAngle, spawnedProjectile, 2 * chargeTime/chargeTimeMax);
                }
                break;
            case 3:
                // Magic
                if (!doneActive) {
                    projectileSpawner.SpawnAttack(spawnDistance * attackAngle, spawnedProjectile);
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
            case 2:
                // Bow
                if (!doneInactive) {
                    
                }
                break;
            case 3:
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
            case 2:
                // Bow
                if (!doneNormal) {
                    
                }
                break;
            case 3:
                // Magic
                if (!doneNormal) {

                }
                break;
        }
    }
}
