using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    // Bottom Rooms are rooms that go under other rooms - ones that have access (Doorway) from the North side.
    // The same is true for the other sides.
    public List<GameObject> bottomRooms;
    public List<GameObject> topRooms;
    public List<GameObject> leftRooms;
    public List<GameObject> rightRooms;

    // A closed room has no access.
    public GameObject closedRoom;

    // The List of all rooms
    public List<GameObject> rooms;
    public List<Vector2> positions;

    
    public float waitTime;      // Time to wait when generating the rooms
    private bool spawnedBoss;   // Has the boss been spawned?
    public GameObject boss;     // The boss to spawn

    async void Update() {
        if (waitTime <= 0 && !spawnedBoss) {
            // Wait until rooms have stopped generating, then find most recent and spawn boss.
            for (int i = 0; i < rooms.Count; i++) {
                if (i == rooms.Count - 1) {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        } else if (waitTime > 0) {
            // waitTime -= Time.deltaTime;
        }
    }
}
