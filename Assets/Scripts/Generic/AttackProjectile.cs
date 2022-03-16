using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject projectileParent;
    public bool canDo;

    void throwProjectile(GameObject proj, Vector2 pos, float rot) {
        var projectileClone = Instantiate(proj, pos, Quaternion.Euler(0, 0, rot));
        projectileClone.GetComponent<Arrow>().creator = gameObject;
        projectileClone.transform.parent = projectileParent.transform;
    }

    void Update() {
        if (Input.GetAxis("Attack") > 0 && canDo) {
            throwProjectile(projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0);
            canDo = false;
        }
    }
}
