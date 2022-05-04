using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float kbStrength;
    [SerializeField]
    private string ignoreTag;

    [SerializeField]
    private BoxCollider2D selfCol;

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
        if (col.tag == "Player") {
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
            col.SendMessage("applyKnockback", kbAngle.normalized * kbStrength);
            col.SendMessage("applyDamage", damage);
        }

        // Sword hit a breakable object
        else if (col.tag == "Breakable") {
            col.SendMessage("applyDamage", damage);
        } 
    }
}
