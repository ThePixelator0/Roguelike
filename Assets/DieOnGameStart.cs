using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnGameStart : MonoBehaviour
{
    public void Start() {
        GameObject.Find("Room Templates").GetComponent<RoomTemplate>().SendStartMessage.Add(gameObject);
        // print(gameObject);
    }

    public void StartGame() {
        Destroy(gameObject);
    }
}
