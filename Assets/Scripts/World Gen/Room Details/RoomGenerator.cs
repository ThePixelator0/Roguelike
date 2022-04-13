using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    // This script determines what the details of each room is. 
    public int roomType;
    public List<GameObject> rooms;

    void Start() {
        roomType = Random.Range(0, rooms.Count);

        Instantiate(rooms[roomType], transform.position, Quaternion.identity);
    }
}
