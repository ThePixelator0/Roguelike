using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Items : MonoBehaviour
{
    // This script is used for the items the player currently has

    [HideInInspector]
    public bool silverskull = false;

    public List<int> heldItems;

    private GameObject itemInformation;

    void Start() {
        itemInformation = GameObject.Find("ItemInformation");
    }


    void ReceiveItem(int item) {
        // print("Picked up item " + item);
        heldItems.Add(item);
        itemInformation.SendMessage("AddItemInformation", item);
        
        switch (item) {
            case 0:
                // Turn the Lights Up, decrease stealth, increase damage
                gameObject.transform.Find("Player Light").GetComponent<Light2D>().intensity = 1;
                PlayerStats.stealthMod -= 0.15f;
                PlayerStats.damageMod += 1f;
                break;
            case 1:
                // Enable Dashing
                gameObject.GetComponent<PlayerMovement>().canDash = true;
                break;
            case 2:
                // Increase speed by 15%
                PlayerStats.speedMod += 0.15f;
                break;
            case 3:
                // Increase stealth by 75%
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