using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

public class MeleeWeaponDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float kbStrength;
    [SerializeField]
    private string ignoreTag;

    [SerializeField]
    private BoxCollider2D selfCol;

    [SerializeField]
    private bool interrupt;

    List<GameObject> collisions;
    LOS sight = new LOS();

    void Start() {
        collisions = new List<GameObject>();
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

            Vector2 kbAngle = col.transform.position - transform.position;
            col.BroadcastMessage("applyKnockback", kbAngle.normalized * kbStrength);
            col.BroadcastMessage("applyDamage", damage);
            if (interrupt) col.BroadcastMessage("Interrupt");
        }

        // Sword hit a breakable object
        else if (col.tag == "Breakable") {
            col.SendMessage("applyDamage", damage);
        } 
    }
}
