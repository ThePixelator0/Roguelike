using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    void Start() {
        // This makes the Heirarchy look cleaner BUT
        // It has the downside of breaking the shadows between rooms

        if (this.gameObject != null) {
            transform.parent = GameObject.Find("Parents/Rooms").transform;
        }
    }
}
