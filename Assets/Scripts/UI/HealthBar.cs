using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private float timeUntilFade;

    public void SetHealth(float health) {
        slider.value = health;
    }

    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void FadeAway(float seconds) {
        // Fade health bar away after X seconds
        transform.parent.GetComponent<Canvas>().enabled = true;
        timeUntilFade = seconds;
    }

    void FixedUpdate() {
        if (timeUntilFade >= 0 && timeUntilFade != -69) {
            timeUntilFade -= Time.deltaTime;
            if (timeUntilFade < 0) {
                transform.parent.GetComponent<Canvas>().enabled = false;
            }
        }
    }
}
