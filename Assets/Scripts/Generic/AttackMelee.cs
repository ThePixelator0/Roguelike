using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackMelee : MonoBehaviour
{
    
    [SerializeField]
    private float damage;
    [SerializeField]
    private TilemapRenderer rend;
    [SerializeField]
    private BoxCollider2D selfCol;
    
    [Space]
    public bool attacking;
    public int attackType;  // What attack to use when used
            // 1 = Jab
            // 2 = Slash
    public float timeActive;


    // Update is called once per frame
    void Update() {
        if (timeActive == 0 && Input.GetAxis("Attack") != 0) {
            attackJab();
        } else {
            EndAttack();
        }

        transform.position = transform.parent.position;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy") {
            col.SendMessage("applyDamage", damage);
        }
    }

    void FaceMouse(float offset = 0f) {
        // Face towards Mouse

        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f; // Distance between the camera and the object

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void EndAttack() {
        if (timeActive >= 0) {
            timeActive -= Time.deltaTime;

            if (timeActive <= 0) {
                rend.enabled = false;
                selfCol.enabled = false;
                timeActive = 0;
            }
        }
    }

    // --------------- Attacks ---------------

    void attackJab() {
        // 0. Setup Attack
        attacking = true;
        damage = 25;

        // 1. Face Mouse
        FaceMouse();

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;

        // 3. Disappear after 0.5s
        timeActive = 0.5f;
    }

    void attackSlash() {
        /* 
            1. Face Mouse
            2. Turn x degrees away instantly
            3. Turn 2x degrees back (the swing)
        */
    }
}
