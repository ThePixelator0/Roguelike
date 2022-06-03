using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class attackSpawner : MonoBehaviourPunCallbacks
{
    public GameObject creator;

    public void SpawnAttack(Vector3 spawnOffset, Projectile a, float chargeMod = 1) {
        GameObject clone = PhotonNetwork.Instantiate(a.gameObject.name, transform.position + spawnOffset, transform.rotation);
        clone.GetComponent<ProjectileObject>().SetInfo(creator, a, a.charged ? chargeMod : 1);

    }
}
