using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerItems
{
    // This script stores variables for the player that other scripts may want to access.

    public static List<int> items = new List<int>();
    
    public static void PickupItem(int itemnum) {
        // Gives the player an item.
        items.Add(itemnum);
        GameObject.Find("Player").SendMessage("ReceiveItem", itemnum);
    }
    
}
