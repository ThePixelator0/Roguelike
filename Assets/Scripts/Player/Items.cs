using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Items : MonoBehaviour
{
    public List<int> items;

    public void PickupItem(int itemnum) {
        // Another GameObject will call this script to give the player an item.
        items.Add(itemnum);
        ReceiveItem(itemnum);
    }

    void ReceiveItem(int item) {
        // Some items do something when they are picked up. This is where that happens.
        switch (item) {
            case 0:
                // Turn the Lights Up
                gameObject.transform.Find("Player Light").GetComponent<Light2D>().intensity = 1;
                break;
            case 1:
                // Enable Dashing
                gameObject.GetComponent<Movement>().canDash = true;
                break;
            case 2:
                // Increase speed by 50%
                gameObject.GetComponent<PlayerStats>().speedMod += 0.15f;
                break;
            case 3:
                // Increase stealth by 1
                gameObject.GetComponent<PlayerStats>().stealthMod += 1f;
                break;
            case 4:
                // Heal player to Full
                gameObject.SendMessage("SetHealth", gameObject.GetComponent<Health>().maxHealth);
                break; 
        }
    }
}
