using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoom : MonoBehaviour
{
    void Start() {
        transform.SetParent(GameObject.Find("MinimapController/Minimap").transform, false);
    }
}
