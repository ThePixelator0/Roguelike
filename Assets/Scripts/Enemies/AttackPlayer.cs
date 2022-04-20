using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineOfSight;

public class AttackPlayer : MonoBehaviour
{
    public float attackCooldown;
    public float timeBetweenAttacks;

    public float damage;
    public float knockbackStr;

    LOS sight = new LOS();

    void OnTriggerStay2D(Collider2D col) {
        if (col.tag == "Player" && attackCooldown == 0) {
            // Check line of sight
            bool inLOS = sight.PositionLOS(transform.position, col.transform.position, "Player", "Enemy");
            if (inLOS == false) {
                return;
            }

            attackCooldown = timeBetweenAttacks;

            Vector2 kbAngle = col.transform.position - transform.position;

            col.SendMessage("applyKnockback", kbAngle.normalized * knockbackStr);
            col.SendMessage("applyDamage", damage);
        }
    }

    void Update() {
        if (attackCooldown > 0) {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown < 0) {
                attackCooldown = 0;
            }
        }
    }
}
