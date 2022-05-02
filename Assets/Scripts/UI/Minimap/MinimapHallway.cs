using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHallway : MonoBehaviour
{
    void Start() {
        transform.SetParent(GameObject.Find("MinimapController/Hallways").transform, false);
    }
}
