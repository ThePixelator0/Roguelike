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
    /* values:
        0 - Sword Jab
        1 - Shield Bash
        2 - Sword Slash
    
    
    */

    public float timeActive;
    public float timeInactive;

    void Start() {
        // sword.GetComponent<AttackMelee>().EnableSword(0);
        // shield.GetComponent<Shield>().EnableShield();
    }

    void FixedUpdate() {
        if (timeActive == 0 && timeInactive == 0) {
            if ((Input.GetAxis("Primary") != 0)) {
                useEquipment(primary);
            } else if ((Input.GetAxis("Secondary") != 0)) {
                useEquipment(secondary);
            }
        } 
        
        else if (timeActive != 0) {
            timeActive -= Time.deltaTime;
            if (timeActive <= 0) {
                timeActive = 0;
            }
        } else if (timeInactive != 0) {
            timeInactive -= Time.deltaTime;
            if (timeInactive <= 0) {
                timeInactive = 0;
            }
        }


        // Paused
        if (Time.timeScale == 0) {
            timeInactive = 0.2f;
        }
    }

    void useEquipment(int weapon) {
        Vector2 times = new Vector2();
        
        switch (weapon) {
            case 0:
                times = sword.BeginAttack("Jab");
                break;
            case 1: 
                times = shield.BeginBash();
                break;
        }

        timeActive = times.x;
        timeInactive = times.y;
    }
}