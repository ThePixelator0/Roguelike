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
            // 0 = Jab
            // 1 = Slash
    public float timeActive;
    public float timeInactive;

    public float turnDir;   // Dir to turn during attack


    // Update is called once per frame
    void Update() {
        if (timeActive == 0 && timeInactive == 0 && Input.GetAxis("Attack") != 0) {
            switch (attackType) {
                case 0:
                    attackJab();
                    break;
                case 1:
                    attackSlash();
                    break;
                default:
                    break;
            }
        } else {
            AttackDetails();
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 + offset));
    }

    void AttackDetails() {
        // This is where the "Special Effects" for each attack happen

        if (attacking = true && attackType == 1) {
            // Slash attack needs to turn a total of 90 degrees over the attack time
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (turnDir * 360 * Time.deltaTime)));
        }
    }

    void EndAttack() {
        if (timeActive > 0) {
            timeActive -= Time.deltaTime;

            if (timeActive <= 0) {
                rend.enabled = false;
                selfCol.enabled = false;
                attacking = false;
                timeActive = 0;
            }
        } else if (timeInactive > 0) {
            timeInactive -= Time.deltaTime;

            if (timeInactive < 0) {
                timeInactive = 0;
            }
        } else if (timeInactive == 0 && timeActive == 0) {
            attacking = false;
        }
    }

    // --------------- Attacks ---------------

    void attackJab() {
        // 0. Setup Attack
        attacking = true;
        damage = 40;

        // 1. Face Mouse
        FaceMouse();

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;

        // 3. Disappear after a time
        timeActive = 0.25f;
        timeInactive = 0.25f;
    }

    void attackSlash() {
        // 0. Setup Attack
        attacking = true;
        damage = 25;

        // 1. Face Mouse, then turn +/- 45 degrees
        int rand = Random.Range(0, 2);      // Between 0 and 1
        FaceMouse((rand * 90f) - 45f);      // (90 * [0 or 1]) - 45 will be (0 - 45) or (90 - 45) which will be -45 or 45

        if (rand == 0) {
            turnDir = 1;
        } else {
            turnDir = -1;
        }

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;

        // Disappear after a time
        timeActive = 0.2f;
        timeInactive = 0.2f;
    }
}
