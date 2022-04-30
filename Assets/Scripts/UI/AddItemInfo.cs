using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemInfo : MonoBehaviour
{
    public List<GameObject> itemObjects;
    private List<ItemInfo> itemIDs; 

    private int spawnHeight = -450;

    void Start() {
        itemIDs = new List<ItemInfo>();
        
        // Assign the itemIDs var to the ItemInfo component of each element in itemObjects
        foreach (GameObject item in itemObjects) {
            itemIDs.Add(item.GetComponent<ItemInfo>());
        }
    }

    public void AddItemInformation(int ID) {
        int x = 0;
        foreach (ItemInfo item in itemIDs) {
            if (item.itemID == ID) {
                SpawnInfo(spawnHeight, x);
                spawnHeight += 200;
                break;
            } else {
                x++;
            }
        }
    }

    void SpawnInfo(float height, int spawnNum) {
        Vector3 spawnPos = new Vector3(-850, height, 0);
        GameObject spawn = Instantiate(itemObjects[spawnNum], spawnPos, Quaternion.identity);
        spawn.transform.SetParent(transform, false);
    }
}
