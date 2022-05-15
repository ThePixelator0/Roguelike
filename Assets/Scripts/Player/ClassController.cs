using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassController : MonoBehaviour
{
    // Controller for which class the player has.
    // Function changes the variables.
    public GameObject player;
    public PlayerMovement movement;
    public PlayerWeaponController playerWeaponController;
    public List<GameObject> playerWeapons;

    [Space] public int defaultClass = 0;

    void Start() {
        ChangeClass(defaultClass);
    }

    public void ChangeClass(int classNum) {
        ResetClass();

        switch (classNum) {
            case 0:
                Fighter();
                break;
            case 1:
                Puncher();
                break;
        }
    }

    void Fighter() {
        ChangePlayerWeapon(0);
        movement.canDash = true;
        player.GetComponent<CooldownContoller>().EnableCooldown(0);
    }

    void Puncher() {
        ChangePlayerWeapon(1);
    }


    void ResetClass() {
        movement.canDash = false;
        player.GetComponent<CooldownContoller>().EnableCooldown(0, false);

    }

    void ChangePlayerWeapon(int weaponNum) {
        foreach (GameObject weapon in playerWeapons) {
            weapon.SetActive(false);
        }


        playerWeapons[weaponNum].SetActive(true);
        playerWeaponController.weapon = playerWeapons[weaponNum].GetComponent<PlayerWeapon>();
    }
}
