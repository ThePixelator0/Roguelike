using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
    public GameObject exit;

    void Die() {
        Instantiate(exit, transform.position, Quaternion.identity);
        print("Created Exit at " + transform.position);
        Destroy(gameObject);
    }
}
