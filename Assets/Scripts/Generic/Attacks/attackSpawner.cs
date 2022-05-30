using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSpawner : MonoBehaviour
{
    public GameObject creator;

    public void SpawnAttack(Vector3 spawnOffset, Projectile a, float chargeMod = 1) {
        GameObject clone = Instantiate(a.gameObject, transform.position + spawnOffset, transform.rotation);
        clone.GetComponent<ProjectileObject>().SetInfo(creator, a, a.charged ? chargeMod : 1);

    }
}
