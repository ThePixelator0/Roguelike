using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LineOfSight;

public class AttackMelee : MonoBehaviour
{
    public float damageMod;
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

    private List<GameObject> collisions;
    LOS sight = new LOS();


    // Update is called once per frame
    void Update() {
        transform.position = transform.parent.position;

        if (timeActive == 0 && timeInactive == 0 && (Input.GetAxis("Primary") != 0)) {
            BeginAttack();
            attackType = 0;
            attackJab();
        } else if (timeActive == 0 && timeInactive == 0 && (Input.GetAxis("Secondary") != 0)) {
            BeginAttack();
            attackType = 1;
            attackSlash();
        } else {
            AttackDetails();
            EndAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" || col.tag == "Boss") {
            // Check line of sight
            bool inLOS = sight.PositionLOS(transform.position, col.transform.position, col.tag);
            if (inLOS == false) {
                return;
            }


            // Check if Already Hit 
            if (collisions.Contains(col.gameObject)) {
                return;
            } else {
                collisions.Add(col.gameObject);
            }

            if (attackType == 0) {
                // Jab
                if (collisions.Count == 1) {
                    // Jab can only hit 1 enemy

                    Vector2 kbAngle = col.transform.position - transform.position;
                    col.SendMessage("applyKnockback", kbAngle.normalized * 5f);
                    col.SendMessage("applyDamage", damage * PlayerStats.damageMod);
                }
            }
            else if (attackType == 1) {
                // Slash
                Vector2 kbAngle = col.transform.position - transform.position;
                col.SendMessage("applyKnockback", kbAngle.normalized * 10f);
                col.SendMessage("applyDamage", damage * PlayerStats.damageMod);
            }
        }

        // Sword hit a breakable object
        else if (col.tag == "Breakable") {
            col.SendMessage("applyDamage", damage * PlayerStats.damageMod);
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

        if (attacking == true && attackType == 0) {
            // Jab attack moves in, then out
            
            // Do this later
        }

        else if (attacking == true && attackType == 1) {
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

    void BeginAttack() {
        collisions = new List<GameObject>();
    }

    // --------------- Attacks ---------------

    void attackJab() {
        // 0. Setup Attack
        attacking = true;
        damage = 40 * (1 + damageMod);

        // 1. Face Mouse
        FaceMouse();

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;

        // 3. Disappear after a time
        timeActive = 0.2f;
        timeInactive = 0.1f;
    }

    void attackSlash() {
        // 0. Setup Attack
        attacking = true;
        damage = 20 * (1 + damageMod);

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
