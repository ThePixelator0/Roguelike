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
        PlayerStats.currentClass = classNum;
        print("Class changed to " + classNum);

        switch (classNum) {
            case 0:
                Fighter();
                break;
            case 1:
                Puncher();
                break;
            case 2:
                Bower();
                break;
            case 3:
                MagicUser();
                break;
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------

    void Fighter() {
        ChangePlayerWeapon(0);
        movement.canDash = true;
        // player.GetComponent<CooldownContoller>().EnableCooldown(0);
    }

    void Puncher() {
        ChangePlayerWeapon(1);
        movement.maxSpeed += 0.5f;
    }

    void Bower() {
        ChangePlayerWeapon(2);
    }

    void MagicUser() {
        ChangePlayerWeapon(3);
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------

    void ResetClass() {
        movement.maxSpeed = 4;

        movement.canDash = false;
        // player.GetComponent<CooldownContoller>().EnableCooldown(0, false);

    }

    void ChangePlayerWeapon(int weaponNum) {
        foreach (GameObject weapon in playerWeapons) {
            weapon.SetActive(false);
        }


        playerWeapons[weaponNum].SetActive(true);
        playerWeaponController.weapon = playerWeapons[weaponNum].GetComponent<PlayerWeapon>();
    }
}
