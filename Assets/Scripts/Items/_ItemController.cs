using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class _ItemController : MonoBehaviour
{
    public List<Item> genericItemList;
    public List<Item> uniqueItemList;

    public List<Item> allItems;

    Vector2 itemDisplayPos;
    public GameObject blankItemDisplay;

    public List<Item> itemsOnDisplay;

    void Start() {
        itemDisplayPos = new Vector2(-750, 400);
        itemsOnDisplay = new List<Item>();
        allItems = uniqueItemList.Union(genericItemList).ToList<Item>();
    }

    public void AddItemToDisplay(int itemNum) {
        Item item = null;


        foreach (Item i in allItems) {
            if (i.id == itemNum) {
                item = i;
                break;
            }
        }

        // Check if item is already on display
        if (itemsOnDisplay.Contains(item)) {
            return;
        } else {
            itemsOnDisplay.Add(item);
        }


        GameObject itemDisplay = Instantiate(blankItemDisplay, itemDisplayPos, Quaternion.identity);
        itemDisplay.GetComponent<ItemDisplay>().SetItem(item);

        itemDisplayPos.x += 500;
        if (itemDisplayPos.x > 750) {
            itemDisplayPos.x = -750;
            itemDisplayPos.y -= 300;
        }
    }

    public void CreateItem(Vector2 pos, float uniqueChance = 100) {
        GameObject itemObject;
        int rand;
        
        if (Random.Range(0, 101) <= uniqueChance) {
            // The item is a unique item
            rand = Random.Range(0, uniqueItemList.Count);
            itemObject = uniqueItemList[rand].gameObject;
            uniqueItemList.RemoveAt(rand);
        } else {
            // The item is a generic item
            rand = Random.Range(0, genericItemList.Count);
            itemObject = genericItemList[rand].gameObject;
        }

        
        PhotonNetwork.Instantiate(itemObject.name, pos + new Vector2(0.5f, 0.5f), Quaternion.identity);
    }
}
