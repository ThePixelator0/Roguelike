using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassController : MonoBehaviour
{
    // Controller for which class the player has.
    // Function changes the variables.
    private GameObject player;
    private Movement playerMovement;
    private PlayerWeaponController playerWeaponController;
    public List<GameObject> playerWeapons;

    void Start() {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<Movement>();
        foreach (Transform child in player.transform) {
            if (child.gameObject.name == "PlayerWeaponController") playerWeaponController = child.gameObject.GetComponent<PlayerWeaponController>();
        }
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
        playerMovement.canDash = true;
    }

    void Puncher() {
        ChangePlayerWeapon(1);
    }


    void ResetClass() {
        playerMovement.canDash = false;

    }

    void ChangePlayerWeapon(int weaponNum) {
        foreach (GameObject weapon in playerWeapons) {
            weapon.SetActive(false);
        }


        playerWeapons[weaponNum].SetActive(true);
        playerWeaponController.weapon = playerWeapons[weaponNum].GetComponent<PlayerWeapon>();
    }
}
