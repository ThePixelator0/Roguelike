using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    void Start() {
        // This makes the Heirarchy look cleaner

        if (this.gameObject != null) {
            transform.parent = GameObject.Find("Parents/Rooms").transform;
        }
    }
}
