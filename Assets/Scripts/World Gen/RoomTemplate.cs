using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    // Bottom Rooms are rooms that go under other rooms - ones that have access (Doorway) from the North side.
    // The same is true for the other sides.
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    // A closed room has no access.
    public GameObject closedRoom;

    // The List of all rooms
    public List<GameObject> rooms;

    
    public float waitTime;      // Time to wait when generating the rooms
    private bool spawnedBoss;   // Has the boss been spawned?
    public GameObject boss;     // The boss to spawn

    void Update() {
        if (waitTime <= 0 && !spawnedBoss) {
            for (int i = 0; i < rooms.Count; i++) {
                if (i == rooms.Count - 1) {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        } else {
            // waitTime -= Time.deltaTime;
        }
    }
}
