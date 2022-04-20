using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // This script controls which weapon the play has and can use.
    [SerializeField]
    private Shield shield;
    [SerializeField]
    private AttackMelee sword;
    [SerializeField]
    private int primary;
    [SerializeField]
    private int secondary;

    void Start() {
        // sword.GetComponent<AttackMelee>().EnableSword(0);
        // shield.GetComponent<Shield>().EnableShield();
    }

    void Update() {
        if ((Input.GetAxis("Primary") != 0)) {
            usePrimary(primary);
        } else if ((Input.GetAxis("Secondary") != 0)) {
            useSecondary(secondary);
        }
    }

    void usePrimary(int weapon) {
        switch (weapon) {
            case 0:
                if (sword.timeActive == 0 && sword.timeInactive == 0) {
                    sword.BeginAttack(0);
                }
                break;
        }
    }

    void useSecondary(int weapon) {
        switch (weapon) {
            case 0:
                if (sword.timeActive == 0 && sword.timeInactive == 0) {
                    sword.BeginAttack(1);
                }
                break;
            case 1:
                shield.EnableShield();
                break;
        }
    }
}
