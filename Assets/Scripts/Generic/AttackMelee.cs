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

    // Update is called once per frame
    void Update() {
        // Visible when Active
        if (Input.GetAxis("Attack") != 0 && !(rend.enabled)) {
            rend.enabled = true;
            selfCol.enabled = true;
        } else if (Input.GetAxis("Attack") == 0 && rend.enabled) {
            rend.enabled = false;
            selfCol.enabled = false;
        }

        // Face towards Mouse
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f; // Distance between the camera and the object

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy") {
            col.SendMessage("applyDamage", damage);
        }
    }
}
