using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    // Bottom Rooms are rooms that have a doorway on the bottom.
    // The same is true for the other directions.
    public List<GameObject> bottomRooms;
    public List<GameObject> topRooms;
    public List<GameObject> leftRooms;
    public List<GameObject> rightRooms;

    [HideInInspector]
    public List<string> bottomRooms_Names;
    [HideInInspector]
    public List<string> topRooms_Names;
    [HideInInspector]
    public List<string> leftRooms_Names;
    [HideInInspector]
    public List<string> rightRooms_Names;

    [Space]
    // The List of all rooms
    public List<GameObject> rooms;
    public List<Vector2> positions;

    [Space]
    public List<GameObject> spawnQueue;
    public bool canSpawn;
    public float waitTime;      // Time to wait when generating the rooms
    [Space]
    private bool spawnedBoss;   // Has the boss been spawned?
    public GameObject boss;     // The boss to spawn
    [Space]
    public GameObject roomDecor;
    public GameObject bossRoomDecor;

    void Start() {
        canSpawn = true;

        bottomRooms_Names = new List<string>();
        topRooms_Names = new List<string>();
        leftRooms_Names = new List<string>();
        rightRooms_Names = new List<string>();
        
        // Set each room_names var to that room's name, avoiding duplicates
        foreach (GameObject room in bottomRooms) {
            if (bottomRooms_Names.Contains(room.name + "(Clone)")) {
                continue;
            }
            bottomRooms_Names.Add(room.name + "(Clone)");
        }
        foreach (GameObject room in topRooms) {
            if (topRooms_Names.Contains(room.name + "(Clone)")) {
                continue;
            }
            topRooms_Names.Add(room.name + "(Clone)");
        }
        foreach (GameObject room in leftRooms) {
            if (leftRooms_Names.Contains(room.name + "(Clone)")) {
                continue;
            }
            leftRooms_Names.Add(room.name + "(Clone)");
        }
        foreach (GameObject room in rightRooms) {
            if (rightRooms_Names.Contains(room.name + "(Clone)")) {
                continue;
            }
            rightRooms_Names.Add(room.name + "(Clone)");
        }
    }

    void Update() {
        if (spawnQueue.Count > 0 && canSpawn && waitTime > 0.1) {
            if (spawnQueue[0] != null) {
                waitTime = 0;
                canSpawn = false;
                spawnQueue[0].SendMessage("Spawn");
            }
            spawnQueue.RemoveAt(0);

            if (spawnQueue.Count == 0) {
                GameObject bossObject = Instantiate(boss, positions[positions.Count - 1] * 14, Quaternion.identity);

                foreach (Vector2 coord in positions) {
                    Vector3 real = new Vector3(coord.x, coord.y, 0);
                    real *= 14;
                    
                    if (coord == positions[positions.Count - 1]) {
                        Instantiate(bossRoomDecor, real, Quaternion.identity);
                    } else {
                        Instantiate(roomDecor, real, Quaternion.identity);
                    }
                }
            }
        } else if (spawnQueue.Count > 0) {
            waitTime += Time.deltaTime;
        }
    }
}
