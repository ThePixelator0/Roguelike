using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackCooldown;
    private Vector3 mousePos;

    void throwProjectile(GameObject proj, Vector2 pos, float rot) {
        var projectileClone = Instantiate(proj, pos, Quaternion.Euler(0, 0, rot - 90));
        projectileClone.GetComponent<Arrow>().creator = gameObject;
        // print(projectileClone.GetComponent<Arrow>().creator);
    }

    void Update() {
        // Attack
        if (Input.GetAxis("Attack") > 0 && attackTime <= 0) {
            Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
            mousePos = Input.mousePosition;
            mousePos.z = 5.23f; // Distance between the camera and the object

            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            attackTime = attackCooldown;
            throwProjectile(projectile, transform.position, angle);
        }

        // Attack Cooldown
        if (attackTime > 0) {
            attackTime -= Time.deltaTime;
        }
    }
}
