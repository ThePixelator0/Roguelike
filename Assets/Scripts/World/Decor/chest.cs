using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class chest : MonoBehaviour
{
    [SerializeField]
    private GameObject open;
    [SerializeField]
    private GameObject closed;

    public bool goldChest;

    public bool isClosed;

    [SerializeField]
    private _ItemController itemController;
    
    void Start() {
        closed.SetActive(true);
        open.SetActive(false);

        itemController = GameObject.Find("_ItemController").GetComponent<_ItemController>();
    }

    void Open() {
        closed.SetActive(false);
        open.SetActive(true);
        isClosed = false;
        
        if (goldChest) itemController.CreateItem(transform.position);       // Gold Chests have 100% chance at unqiue item
        else itemController.CreateItem(transform.position, 20);             // Silver Chests have 20% chance at unqiue item
    }

    public void applyDamage(float damage) {
        if (isClosed) {
            Open();
        }
    }
}
