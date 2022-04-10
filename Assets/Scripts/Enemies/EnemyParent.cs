using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    void Awake() {
        transform.parent = GameObject.Find("Parents/Enemies").transform;
    }
}
