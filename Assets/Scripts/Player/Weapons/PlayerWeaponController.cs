using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerWeaponController : MonoBehaviourPunCallbacks, IPunObservable
{
    // This script controls which weapon the player has and can use.
    public PlayerWeapon weapon;         // Local var of PlayerWeapon Script 
    public PlayerMovement movement;     // Local var of Movement Script
    public Conditions conditions;       // conditions script
    private GameObject player;

    [Space]
    public float attackWarmup;   // How long before attack starts
    public float timeActive;     // First part of attack
    public float timeInactive;   // Second part of attack
    public float attackCooldown; // how long before input can be made

    public PhotonView view;

    void Start() {
        player = transform.parent.gameObject;
        view = player.GetComponent<PhotonView>();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // // Sync Data
        // if (stream.IsWriting) {
        //     // We are writing
        //     stream.SendNext(weapon.selfCol.enabled);
        //     stream.SendNext(weapon.selfRend.enabled);
        // }
        // else {
        //     // We are reading
        //     weapon.selfCol.enabled = (bool)stream.ReceiveNext();
        //     weapon.selfRend.enabled = (bool)stream.ReceiveNext();
        // }
    }



    void Update() {
        if (view.IsMine) {
            transform.position = player.transform.position;

            if (timeActive == 0 && timeInactive == 0 && attackCooldown == 0 && attackWarmup == 0) {
                if (Input.GetAxis("Primary") != 0) {
                    Attack();
                } 
            } 
            
            else if (attackWarmup != 0 && attackWarmup != -1) {
                attackWarmup -= Time.deltaTime;
                if (attackWarmup <= 0) attackWarmup = 0;
                
            } else if (attackWarmup == -1) {
                // attackWarmup == -1 means that it is a charged attack
                if (Input.GetAxis("Primary") != 0 && weapon.chargeTime < weapon.chargeTimeMax) {
                    weapon.chargeTime += Time.deltaTime;
                    if (weapon.chargeTime > weapon.chargeTimeMax) weapon.chargeTime = weapon.chargeTimeMax;
                } else if (Input.GetAxis("Primary") == 0) {
                    if (weapon.chargeTimeMin > weapon.chargeTime) {
                        weapon.chargeTime += Time.deltaTime;
                        if (weapon.chargeTime > weapon.chargeTimeMax) weapon.chargeTime = weapon.chargeTimeMax;
                    } else {
                        attackWarmup = 0;
                    }
                }
            } else if (timeActive != 0) {
                timeActive -= Time.deltaTime;
                if (timeActive <= 0) timeActive = 0;
                
            } else if (timeInactive != 0) {
                timeInactive -= Time.deltaTime;
                if (timeInactive <= 0) {
                    timeInactive = 0;
                }
                
            } else if (attackCooldown != 0) {
                attackCooldown -= Time.deltaTime;
                if (attackCooldown <= 0) attackCooldown = 0;
            }
        }
    }

    void Attack() {
        weapon.Attack(DirToMouse());

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

    public Vector3 DirToMouse(float offset = 0) {
        // Returns a Vector3 that will be pointing to the mouse position

        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;     // Distance between the camera and the object

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, angle - 90 + offset);
    }
}
