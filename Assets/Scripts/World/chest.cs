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

        int rand = Random.Range(0, itemController.GetComponent<_ItemController>().itemList.Count);
        GameObject loot = itemController.GetComponent<_ItemController>().itemList[rand];

        Instantiate(loot, transform.position, Quaternion.identity);
    }
}
