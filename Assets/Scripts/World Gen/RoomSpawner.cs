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

    public List<Vector2> checkedPos;


    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 1f);
    }

    void Spawn() {
        if (spawned == false) {
            templates.positions.Add(transform.position / 14);
            templates.rooms.Add(transform.parent.gameObject);
            // The index of either the Room or the position will be the same.

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

            int i = 0;
            foreach (Vector2 coord in templates.positions) {
                Vector2 real = coord * 14;
                if ((transform.position.x == real.x) && (transform.position.y == real.y)) {
                    continue;
                }

                checkedPos.Add(coord);

                if ((transform.position.y + 14 == real.y) && (transform.position.x == real.x)) {
                    // Space Above Occupied
                    if (openingDirection == 2) {
                        break;
                    }
                    print("There is a room above " + (transform.position / 14));
                    if (templates.bottomRooms.Contains(templates.rooms[i])) {
                        // Above room has a bottom doorway
                        print("There is a room above " + (transform.position / 14) + " that has a bottom doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that do not have a top doorway
                        GetValidRooms(validRooms, templates.topRooms, true);
                    } else {
                        // Above room does not have a bottom doorway
                        print("There is a room above " + (transform.position / 14) + " that does not have a bottom doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that have a top doorway
                        GetValidRooms(validRooms, templates.topRooms, false);
                    }
                } else if ((transform.position.y - 14 == real.y) && (transform.position.x == real.x)) {
                    // Space Below Occupied
                    if (openingDirection == 1) {
                        break;
                    }
                    
                    print("There is a room below " + (transform.position / 14));
                    if (templates.topRooms.Contains(templates.rooms[i])) {
                        // Below room has a top doorway
                        print("There is a room below " + (transform.position / 14) + " that has a top doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that do not have a top doorway
                        GetValidRooms(validRooms, templates.bottomRooms, true);
                    } else {
                        // Below room does not have a top doorway
                        print("There is a room below " + (transform.position / 14) + " that does not have a top doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that have a top doorway
                        GetValidRooms(validRooms, templates.bottomRooms, false);
                    }
                } else if ((transform.position.x + 14 == real.x) && (transform.position.y == real.y)) {
                    // Space to Right Occupied
                    if (openingDirection == 3) {
                        break;
                    }
                    print("There is a room right of " + (transform.position / 14));
                    if (templates.leftRooms.Contains(templates.rooms[i])) {
                        // Right room has a left doorway
                        print("There is a room to the right of " + (transform.position / 14) + " that has a left doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that do not have a right doorway
                        GetValidRooms(validRooms, templates.rightRooms, true);
                    } else {
                        // Right room does not have a left doorway
                        print("There is a room to the right of " + (transform.position / 14) + " that does not have a left doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that have a right doorway
                        GetValidRooms(validRooms, templates.rightRooms, false);
                    }
                } else if ((transform.position.x - 14 == real.x) && (transform.position.y == real.y)) {
                    // Space to Left Occupied
                    if (openingDirection == 4) {
                        break;
                    }
                    print("There is a room left of " + (transform.position / 14));
                    if (templates.rightRooms.Contains(templates.rooms[i])) {
                        // Left room has a Right doorway
                        print("There is a room to the left of " + (transform.position / 14) + " that has a right doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that do not have a right doorway
                        GetValidRooms(validRooms, templates.leftRooms, true);
                    } else {
                        // Left room does not have a Right doorway
                        print("There is a room to the left of " + (transform.position / 14) + " that does not have a right doorway - " + templates.rooms[i]);
                        // Remove rooms from validRooms that have a right doorway
                        GetValidRooms(validRooms, templates.leftRooms, false);
                    }
                }
                i++;
            }
            rand = Random.Range(0, validRooms.Count);
            Instantiate(validRooms[rand], transform.position, Quaternion.identity);
            
            templates.waitTime = .4f;
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false) {
                // Spawned where a room already exists
                Destroy(gameObject);
            } else if (other.GetComponent<RoomSpawner>().openingDirection > openingDirection) {
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
                } else {
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
