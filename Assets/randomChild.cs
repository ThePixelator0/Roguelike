using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomChild : MonoBehaviour
{
    // This script chooses one out of a number of children to remain.
    // Used for randomizing visuals
    public List<GameObject> children;


    void Start()
    {
        int i;
        while (children.Count > 1) {
            i = Random.Range(0, children.Count);
            Destroy(children[i]);
            children.RemoveAt(i);
        } 
    }
}
