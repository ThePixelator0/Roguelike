using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSpawner : MonoBehaviour
{
    public GameObject spawn;
    public GameObject creator;

    public void SpawnAttack(List<Vector3> spawnInfo) {
        transform.eulerAngles = spawnInfo[0];
        GameObject clone = Instantiate(spawn, transform.position, transform.rotation);
        clone.SendMessage("SetInfo", spawnInfo[1].x);
        clone.SendMessage("SetCreator", creator);
    }
}
