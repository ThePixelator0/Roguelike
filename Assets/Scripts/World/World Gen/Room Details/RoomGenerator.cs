using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomGenerator : MonoBehaviour
{
    // This script determines what the details of each room is. 
    public List<GameObject> rooms;

    void Start() {
        // *Add random decorations to the room*
        int roomType = Random.Range(0, rooms.Count);
        PhotonNetwork.Instantiate(rooms[roomType].name, transform.position, Quaternion.identity, 0);

        // *Leaves without elaborating*
        Destroy(gameObject);
        
    }
}
