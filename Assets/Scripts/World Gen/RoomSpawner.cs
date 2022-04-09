using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need right door
    // 4 --> need left door

    private RoomTemplate templates;
    private int rand;
    public bool spawned = false;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    public List<GameObject> validRooms;

    public List<Vector3> checkedPos;


    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        // Invoke("Spawn", 1f);
        templates.spawnQueue.Add(gameObject);
    }

    public void Spawn() {
        if (spawned == false) {

            validRooms = new List<GameObject>();

            if (openingDirection == 1) {    
                // Rooms with a BOTTOM door
                validRooms = new List<GameObject>(templates.bottomRooms);
            } else if (openingDirection == 2) {
                // Rooms with a TOP door
                validRooms = new List<GameObject>(templates.topRooms);
            } else if (openingDirection == 3) {
                // Rooms with a RIGHT door
                validRooms = new List<GameObject>(templates.rightRooms);
            } else if (openingDirection == 4) {
                // Rooms with a LEFT door
                validRooms = new List<GameObject>(templates.leftRooms);
            }

            int i = -1;
            foreach (Vector2 coord in templates.positions) {
                // I have no idea why, but adding 1 at the start instead of the end fixed an issue where sometimes i would not increase. That's why it starts at -1.
                i += 1;

                // selfCoord is set to the in-game position as a grid with room size = 1
                Vector2 selfCoord = transform.position / 14;

                if ((selfCoord.y + 1 == coord.y) && (selfCoord.x == coord.x)) {
                    // Space Above Occupied
                    if (openingDirection == 2) {
                        continue;
                    }
                    if (templates.bottomRooms_Names.Contains(templates.rooms[i].name)) {
                        // Above room has a bottom doorway
                        // Remove rooms from validRooms that do not have a top doorway
                        validRooms = GetValidRooms(validRooms, templates.topRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        // Remove rooms from validRooms that have a top doorway
                        validRooms = GetValidRooms(validRooms, templates.topRooms, false);
                    }
                } else if ((selfCoord.y - 1 == coord.y) && (selfCoord.x == coord.x)) {
                    // Space Below Occupied
                    if (openingDirection == 1) {
                        continue;
                    }
                    
                    if (templates.topRooms_Names.Contains(templates.rooms[i].name)) {
                        // Below room has a top doorway
                        // Remove rooms from validRooms that do not have a top doorway
                        validRooms = GetValidRooms(validRooms, templates.bottomRooms, true);
                    } else {
                        // Below room does not have a top doorway
                        // Remove rooms from validRooms that have a top doorway
                        validRooms = GetValidRooms(validRooms, templates.bottomRooms, false);
                    }
                } else if ((selfCoord.y == coord.y) && (selfCoord.x + 1 == coord.x)) {
                    // Space to Right Occupied
                    if (openingDirection == 3) {
                        continue;
                    }
                    if (templates.leftRooms_Names.Contains(templates.rooms[i].name)) {
                        // Right room has a left doorway
                        // Remove rooms from validRooms that do not have a right doorway
                        validRooms = GetValidRooms(validRooms, templates.rightRooms, true);
                    } else {
                        // Right room does not have a left doorway
                        validRooms = GetValidRooms(validRooms, templates.rightRooms, false);
                    }
                } else if ((selfCoord.y == coord.y) && (selfCoord.x - 1 == coord.x)) {
                    // Space to Left Occupied
                    if (openingDirection == 4) {
                        continue;
                    }
                    if (templates.rightRooms_Names.Contains(templates.rooms[i].name)) {
                        // Left room has a Right doorway
                        // Remove rooms from validRooms that do not have a left doorway
                        validRooms = GetValidRooms(validRooms, templates.leftRooms, true);
                    } else {
                        // Left room does not have a Right doorway
                        // Remove rooms from validRooms that have a left doorway
                        validRooms = GetValidRooms(validRooms, templates.leftRooms, false);
                    }
                }
            }
            rand = Random.Range(0, validRooms.Count);
            GameObject createdRoom = Instantiate(validRooms[rand], transform.position, Quaternion.identity);

            templates.positions.Add(transform.position / 14);
            templates.rooms.Add(createdRoom);
            // The index of either the Room or the position will be the same.
            
            templates.waitTime = .4f;
            spawned = true;
        }

        templates.canSpawn = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("SpawnPoint")) {
            if (spawned) {
                // Room already made, ignore
            } else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false) {
                // Spawned where a room already exists
                Destroy(gameObject);
            } else if (other.GetComponent<RoomSpawner>().openingDirection < openingDirection) {
                // 2 rooms spawned at the same pos. Destroy 1 of them
                Destroy(gameObject);
            }
        }
    }

    List<GameObject> GetValidRooms(List<GameObject> List1, List<GameObject> List2, bool findValid) {
        // If findValid is true, returns common items between the lists. If not, returns items in List1 that are not in List2
        List<GameObject> valid = new List<GameObject>();
        if (findValid == true) {
            foreach (GameObject a in List2) {
                if (List1.Contains(a)) {
                    valid.Add(a);
                }
            }
        } else {
            valid = List1;
            foreach (GameObject b in List2) {
                if (valid.Contains(b)) {
                    valid.Remove(b);
                }
            }
        }
        
        return valid;
    }
}
