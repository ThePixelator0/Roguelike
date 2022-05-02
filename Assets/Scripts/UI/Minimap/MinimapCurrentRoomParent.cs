using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCurrentRoomParent : MonoBehaviour
{
    void Start() {
        transform.SetParent(GameObject.Find("MinimapController").transform, false);
    }
}
