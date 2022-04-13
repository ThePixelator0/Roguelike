using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    void Start()
    {
        transform.parent = GameObject.FindWithTag("Grid").transform;
    }
}
