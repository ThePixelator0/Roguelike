using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public Collider2D selfCol;
    public int potionType;
        // 1 = Speed Buff
        // 2 = Damage Buff
        // 3 = Health Buff

    void Start() {
        potionType = Random.Range(0, 4);    
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            Buff(potionType, col.gameObject);
            Destroy(gameObject);
        }
    }

    void Buff(int buffType, GameObject buffTarget) {
        switch (buffType) {
            case 0:
                buffTarget.GetComponent<Movement>().speed += 1f;
                break;
            case 1:
                // buffTarget.GetComponent<AttackProjectile>().damageMod += .1f;
                // buffTarget.GetComponent<AttackMelee>().damageMod += .1f;
                break;
            case 2:
                buffTarget.GetComponent<Health>().maxHealth += 50f;
                buffTarget.GetComponent<Health>().health += 50f;
                break;
            default:
                break;
        }
    }
}
