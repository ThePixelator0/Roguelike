using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField]
    private GameObject open;
    [SerializeField]
    private GameObject closed;

    public bool goldChest;

    public bool isClosed;

    [SerializeField]
    private GameObject itemController;
    
    void Start() {
        closed.SetActive(true);
        open.SetActive(false);

        itemController = GameObject.Find("_ItemController");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && isClosed) {
            Open(col);
        }
    }

    void Open(Collider2D col) {
        closed.SetActive(false);
        open.SetActive(true);
        isClosed = false;
        List<GameObject> itemList;

        if ((int)Random.Range(0, 9) == 0) { // 10% Change silver chest has unqiue loot
            goldChest = true;
        }

        if (goldChest) {
            itemList = itemController.GetComponent<_ItemController>().uniqueItemList;
        } else {
            itemList = itemController.GetComponent<_ItemController>().genericItemList;
        }
        
        if (itemList.Count > 0) {
            int rand = Random.Range(0, itemList.Count);
            GameObject loot = itemList[rand];
            if (goldChest) {
                itemController.GetComponent<_ItemController>().uniqueItemList.RemoveAt(rand);
            }

            Instantiate(loot, transform.position, Quaternion.identity);
        } else {
            print("No more items to give");
        }
        


    }
}
