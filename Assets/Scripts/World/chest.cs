using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField]
    private GameObject open;
    [SerializeField]
    private GameObject closed;

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

        List<GameObject> itemList = itemController.GetComponent<_ItemController>().itemList;
        if (itemList.Count > 0) {
            int rand = Random.Range(0, itemList.Count);
            GameObject loot = itemList[rand];
            itemController.GetComponent<_ItemController>().itemList.RemoveAt(rand);

            Instantiate(loot, transform.position, Quaternion.identity);
        } else {
            print("No more items to give");
        }
        


    }
}
