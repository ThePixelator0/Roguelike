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

    public List<GameObject> spawnQueue;
    public bool canSpawn;
    public GameObject blocker;

    
    public float waitTime;      // Time to wait when generating the rooms
    private bool spawnedBoss;   // Has the boss been spawned?
    public GameObject boss;     // The boss to spawn

    void Start() {
        canSpawn = true;
    }

    async void Update() {
        if (spawnQueue.Count > 0 && canSpawn && waitTime > 1) {
            waitTime = 0;
            canSpawn = false;
            if (spawnQueue[0] != null) {
                spawnQueue[0].SendMessage("Spawn");
            } else {
                canSpawn = true;
                waitTime = 1;
            }
            spawnQueue.RemoveAt(0);
        } else {
            waitTime += Time.deltaTime;
        }
    }
}
