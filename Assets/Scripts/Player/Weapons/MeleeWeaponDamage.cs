using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using LineOfSight;

public class MeleeWeaponDamage : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float kbStrength;
    [SerializeField]
    private string ignoreTag;

    [SerializeField]
    private BoxCollider2D selfCol;

    [SerializeField] private bool stun;
    [SerializeField] private float stunDuration;


    List<GameObject> collisions;
    LOS sight = new LOS();
    PlayerWeapon weapon;
    PhotonView view;

    // To send data between RPC and non-RPC
    Collider2D hitCollider;

    void Start() {
        collisions = new List<GameObject>();
        weapon = GetComponent<PlayerWeapon>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate() {
        if (selfCol.enabled == false && collisions.Count != 0) {
            collisions = new List<GameObject>();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" || col.tag == "Boss") {
            // Check line of sight
            bool inLOS = sight.PositionLOS(transform.parent.position - new Vector3(0, 0.4f, 0), col.transform.position - new Vector3(0, 0.4f, 0), col.tag, ignoreTag);
            if (inLOS == false) {
                return;
            }

            // Check if Already Hit 
            if (collisions.Contains(col.gameObject)) {
                return;
            } else {
                collisions.Add(col.gameObject);
            }

            hitCollider = col;
            view.RPC("HitEnemy", RpcTarget.All);
        }

        // Sword hit a breakable object
        else if (col.tag == "Breakable") {
            col.SendMessage("applyDamage", damage);
        } 
    }

    
    [PunRPC]
    void HitEnemy() {
            Vector2 kbAngle = hitCollider.transform.position - transform.position;
            kbAngle = (kbAngle.normalized + weapon.attackAngle.normalized).normalized;

            if (weapon.chargeTime > 0) {
                // Melee weapons with a charge time do less knockback over time
                hitCollider.BroadcastMessage("applyKnockback", kbAngle * kbStrength * weapon.controller.movement.rb.velocity.magnitude);
            } else {
                hitCollider.BroadcastMessage("applyKnockback", kbAngle * kbStrength);
            }
            hitCollider.BroadcastMessage("applyDamage", damage * PlayerStats.damageMod);
            if (stun) hitCollider.BroadcastMessage("Stun", stunDuration);
    }
}
