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

    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn() {
        if (spawned == false) {
            templates.positions.Add(transform.position / 14);
            templates.rooms.Add(transform.parent.gameObject);
            // The index of either the Room or the position will be the same.

            List<GameObject> validRooms = new List<GameObject>();

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

            int i = 0;
            foreach (Vector2 coord in templates.positions) {
                Vector2 real = coord * 14;
                if (transform.position.x + 14 == coord.x) {
                    // Space Above Occupied
                    if (templates.topRooms.Contains(templates.rooms[i])) {
                        // Above room has a bottom doorway
                        // Remove rooms from validRooms that do not have an upwards doorway
                        GetValidRooms(validRooms, templates.topRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        // Remove rooms from validRooms that have an upwards doorway
                        GetValidRooms(validRooms, templates.topRooms, false);
                    }
                } else if (transform.position.x - 14 == coord.x) {
                    // Space Below Occupied
                    if (templates.bottomRooms.Contains(templates.rooms[i])) {
                        // Above room has a bottom doorway
                        // Remove rooms from validRooms that do not have a downwards doorway
                        GetValidRooms(validRooms, templates.bottomRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        // Remove rooms from validRooms that have a downwards doorway
                        GetValidRooms(validRooms, templates.bottomRooms, false);
                    }
                } else if (transform.position.y + 14 == coord.y) {
                    // Space to Right Occupied
                    if (templates.leftRooms.Contains(templates.rooms[i])) {
                        // Above room has a bottom doorway
                        // Remove rooms from validRooms that do not have a left doorway
                        GetValidRooms(validRooms, templates.leftRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        // Remove rooms from validRooms that have a left doorway
                        GetValidRooms(validRooms, templates.leftRooms, false);
                    }
                } else if (transform.position.y - 14 == coord.y) {
                    // Space to Left Occupied
                    if (templates.rightRooms.Contains(templates.rooms[i])) {
                        // Above room has a bottom doorway
                        // Remove rooms from validRooms that do not have a right doorway
                        GetValidRooms(validRooms, templates.rightRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        // Remove rooms from validRooms that have a right doorway
                        GetValidRooms(validRooms, templates.rightRooms, false);
                    }
                }
                i++;
            }
            print(validRooms.Count);   
            rand = Random.Range(0, validRooms.Count);
            Instantiate(validRooms[rand], transform.position, Quaternion.identity);
            
            templates.waitTime = .4f;
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // Two rooms spawned at the same pos, this is first room
                Destroy(gameObject);
            }
            if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false) {
                // Two Rooms spawned at the same pos, this is second room
                // Do nothing? 
            }

            //spawned = true;
        }
    }

    List<GameObject> GetValidRooms(List<GameObject> List1, List<GameObject> List2, bool findValid) {
        // If findValid is true, returns common items between the lists. If not, returns items in List1 that are not in List2
        List<GameObject> valid = new List<GameObject>();
        if (findValid) {
            foreach (GameObject a in List2) {
                if (List1.Contains(a)) {
                    valid.Add(a);
                }
            }
        } else {
            valid = List1;
            foreach (GameObject b in List2) {
                valid.Remove(b);
            }
        }

        return valid;
    }
}
