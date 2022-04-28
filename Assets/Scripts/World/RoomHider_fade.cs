using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider_fade : MonoBehaviour
{
    float fadePerSec;
    SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
    }

    public void FadeInSeconds(float seconds) {
        rend.color = rend.color - new Color(0, 0, 0, 255);
        fadePerSec = 255 / seconds;
    }

    void FixedUpdate() {
        if (rend.color.a < 255) {
            rend.color += new Color(0, 0, 0, fadePerSec * Time.deltaTime);
            // if (rend.color.a > 255) {
            //     rend.color = new Color();
            // }
        }
    }
}
