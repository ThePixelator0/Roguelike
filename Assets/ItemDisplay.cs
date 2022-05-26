using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Item item;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    public void Start() {
        transform.SetParent(GameObject.Find("ItemDisplay").transform, false);
    }

    public void SetItem(Item itemToBecome) {
        item = itemToBecome;

        nameText.text = item.name;
        descriptionText.text = item.description;

        artworkImage.sprite = item.art;
    }
}
