using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using LineOfSight;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private TilemapRenderer rend;
    [SerializeField]
    private BoxCollider2D selfCol;

    [Space]
    [SerializeField]
    private float damage;
    [SerializeField]
    private bool usingShield;

    LOS sight = new LOS();
    List<GameObject> collisions;

    private Vector3 posOffset;
    WeaponController parent;


    void Start() {
        rend.enabled = false;
        selfCol.enabled = false;
        parent = transform.parent.GetComponent<WeaponController>();
    }

    void Update() {
        if (usingShield) {
            if (parent.timeActive > 0) {
                transform.position = transform.parent.position - (posOffset * parent.timeActive);
            } else if (parent.timeInactive > 0) {
                transform.position = transform.parent.position + (posOffset * (parent.timeInactive - 0.4f));
                selfCol.enabled = false;
            } else {
                rend.enabled = false;
                usingShield = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy" || col.tag == "Boss") {
            // Check line of sight
            bool inLOS = sight.PositionLOS(transform.parent.parent.position - new Vector3(0, 0.4f, 0), col.transform.position - new Vector3(0, 0.4f, 0), col.tag, "Player");
            if (inLOS == false) {
                // return;
            }

            // Check if Already Hit 
            if (collisions.Contains(col.gameObject)) {
                return;
            } else {
                collisions.Add(col.gameObject);
            }

            Vector2 kbAngle = col.transform.position - transform.position;
            col.SendMessage("applyKnockback", kbAngle.normalized * 10f);
            col.SendMessage("applyDamage", damage * PlayerStats.damageMod);
            col.SendMessage("applyEffect", new Vector2(0, 1));
        }

        // Hit a breakable object
        else if (col.tag == "Breakable") {
            col.SendMessage("applyDamage", damage * PlayerStats.damageMod);
        } 
    }




    public Vector2 BeginBash() {
        // 0. Setup Attack
        damage = 15 * PlayerStats.damageMod;
        usingShield = true;
        collisions = new List<GameObject>();

        // 1. Face Mouse
        FaceMouse();
        setPosOffset();
        

        // 2. Appear
        rend.enabled = true;
        selfCol.enabled = true;

        return new Vector2(0.2f, 0.4f);
    }

    void FaceMouse(float offset = 0f) {
        // Face towards Mouse

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f; // Distance between the camera and the object

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        angle += offset - 90;

        Vector3 angleVec = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(angleVec);
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
}
