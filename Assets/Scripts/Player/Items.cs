using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Items : MonoBehaviour
{
    [HideInInspector]
    public bool silverskull = false;

    void ReceiveItem(int item) {
        // Some items do something when they are picked up. This is where that happens.
        switch (item) {
            case 0:
                // Turn the Lights Up
                gameObject.transform.Find("Player Light").GetComponent<Light2D>().intensity = 1;
                PlayerStats.stealthMod -= 0.15f;
                PlayerStats.damageMod += 1f;
                break;
            case 1:
                // Enable Dashing
                gameObject.GetComponent<Movement>().canDash = true;
                break;
            case 2:
                // Increase speed by 50%
                PlayerStats.speedMod += 0.15f;
                break;
            case 3:
                // Increase stealth by 1
                PlayerStats.stealthMod += 0.75f;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 4:
                // Heal player to Full
                gameObject.SendMessage("SetHealth", gameObject.GetComponent<Health>().maxHealth);
                break; 
            case 5:
                // Skulls now heal player when broken
                silverskull = true;
                break;
        }
    }
}