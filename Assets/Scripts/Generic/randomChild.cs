using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomChild : MonoBehaviour
{
    // This script chooses one out of a number of children to remain.
    public List<GameObject> children;
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
