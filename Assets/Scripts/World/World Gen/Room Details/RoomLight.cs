using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;  

public class RoomLight : MonoBehaviour
{
    // A room's Light will turn on when a player enters
    public Light2D li;
    public float target;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && !li.enabled) {
            target = li.intensity;
            li.intensity = 0f;
            li.enabled = true;
        }
    }

    void FixedUpdate() {
        if (target > li.intensity) {
            li.intensity += Time.deltaTime / 1;
            if (target < li.intensity) {
                li.intensity = target;
            }
        }  
    }
}
