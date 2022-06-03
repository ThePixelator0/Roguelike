using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class randomChild : MonoBehaviourPunCallbacks
{
    // This script chooses one out of a number of children to remain.
    private List<Transform> childrenMarkedForDeath;

    void Start()
    {
        childrenMarkedForDeath = new List<Transform>();
        int i = Random.Range(0, transform.childCount);

        foreach (Transform child in transform) {
            if (transform.GetChild(i) != child) {
                childrenMarkedForDeath.Add(child);
            }
        }

        foreach (Transform child in childrenMarkedForDeath) {
            Destroy(child.gameObject);
        }
    }
}
