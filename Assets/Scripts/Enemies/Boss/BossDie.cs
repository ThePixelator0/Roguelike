using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
    public GameObject goldChest;

    void Die() {
        GameObject templates = GameObject.FindGameObjectWithTag("Rooms");
        Instantiate(goldChest, templates.GetComponent<RoomTemplate>().positions[templates.GetComponent<RoomTemplate>().positions.Count - 1] * 14, Quaternion.identity);
    }
}
