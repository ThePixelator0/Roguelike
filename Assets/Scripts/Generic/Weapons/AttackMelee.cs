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

    public float turnDir;   // Dir to turn during attack


    private Vector3[] attack_stats =    {
                                            new Vector3(0.2f, 0.2f, 40f),      // Jab
                                            new Vector3(0.2f, 0.3f, 25f)       // Slash
                                        };

    private List<GameObject> collisions;
    LOS sight = new LOS();
    
    Vector3 posOffset;
    WeaponController parent;

    void Start() {
        rend.enabled = false;
        selfCol.enabled = false;
        parent = transform.parent.GetComponent<WeaponController>();
    }

    void Update() {
        if (attacking) {
            AttackDetails();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" || col.tag == "Boss") {
            // Check line of sight
            bool inLOS = sight.PositionLOS(transform.parent.position - new Vector3(0, 0.4f, 0), col.transform.position - new Vector3(0, 0.4f, 0), col.tag, "Player");
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

                    selfCol.enabled = false;
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

        if (attackType == 0) {
            // Jab attack moves in, then out
            if (parent.timeActive > 0) {
                transform.position = transform.parent.position - (posOffset * parent.timeActive * 2);
            } else if (parent.timeInactive > 0) {
                transform.position = transform.parent.position + (posOffset * (parent.timeInactive - attack_stats[0].y) * 2);
                selfCol.enabled = false;
            } else {
                rend.enabled = false;
                attacking = false;
            }
        }

        else if (attackType == 1) {
            // Slash attack needs to turn a total of 90 degrees over the attack time
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (turnDir * 360 * Time.deltaTime)));
        }
    }

    public Vector2 BeginAttack(string attack) {
        collisions = new List<GameObject>();

        if (attack == "Jab") {
            attackType = 0;
            attackJab();
            return attack_stats[0];
        } else if (attack == "Slash") {
            attackType = 1;
            attackSlash();
            return attack_stats[1];
        } 

        else {
            return new Vector2();
        }
    }

    void setPosOffset() {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 direction = Vector3.up;

        posOffset = new Vector3(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos,
            0 );
    }

    // --------------- Attacks ---------------

    void attackJab() {
        // 0. Setup Attack
        attacking = true;
        damage = attack_stats[0].z * (PlayerStats.damageMod);

        // 1. Face Mouse
        FaceMouse();
        setPosOffset();

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;
    }

    void attackSlash() {
        // // 0. Setup Attack
        // attacking = true;
        // damage = 20 * (1 + damageMod);

        // // 1. Face Mouse, then turn +/- 45 degrees
        // int rand = Random.Range(0, 2);      // Between 0 and 1
        // FaceMouse((rand * 90f) - 45f);      // (90 * [0 or 1]) - 45 will be (0 - 45) or (90 - 45) which will be -45 or 45

        // if (rand == 0) {
        //     turnDir = 1;
        // } else {
        //     turnDir = -1;
        // }

        // // 2. Appear
        // rend.enabled = true;
        // selfCol.enabled = true;
    }
}
