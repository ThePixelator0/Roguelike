using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public List<int> items;

    public void PickupItem(int itemnum) {
        items.Add(itemnum);
    }
}
