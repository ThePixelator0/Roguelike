using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private bool buttonDown;
    private bool spawned;
    private Vector2 pos = new Vector2(-6, 0);
    [SerializeField]
    private GameObject spawn;

    void Update() {
        if (Input.GetAxis("Test") != 1) {
            buttonDown = true;
        } else {
            buttonDown = false;
        }

        if (buttonDown && !spawned) {
            Instantiate(spawn, pos, Quaternion.Euler(0, 0, 0));
            spawned = true;
        } 
        if (!buttonDown && spawned) {
            spawned = false;
        }
    }
}