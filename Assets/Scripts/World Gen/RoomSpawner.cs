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
    public List<int> req;   // List of requirements for the spawned room.


    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        req = new List<int>();
        Invoke("Spawn", 0.5f);
    }

    async void Spawn() {
        if (spawned == false) {

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

                
            foreach (int i in req) {
                print(i);
                if (i == openingDirection) {
                    break;
                }
                else if (i == 1) {
                    validRooms = GetValidRooms(validRooms, templates.bottomRooms);
                } else if (i == 2) {
                    validRooms = GetValidRooms(validRooms, templates.topRooms);
                } else if (i == 3) {
                    validRooms = GetValidRooms(validRooms, templates.rightRooms);
                } else if (i == 4) {
                    validRooms = GetValidRooms(validRooms, templates.leftRooms);
                }
            }
                
            rand = Random.Range(0, validRooms.Count);
            Instantiate(validRooms[rand], transform.position, Quaternion.identity);
            
            templates.waitTime = .4f;
            templates.positions.Add(transform.position);
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // Two rooms spawned at the same pos, this is first room
                other.GetComponent<RoomSpawner>().req.Add(openingDirection);
                Destroy(gameObject);
            }
            if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false) {
                // Two Rooms spawned at the same pos, this is second room
                // Do nothing? 
            }

            spawned = true;
        }
    }

    List<GameObject> GetValidRooms(List<GameObject> List1, GameObject[] List2) {
        List<GameObject> valid = new List<GameObject>();
        foreach (GameObject a in List2) {
            if (List1.Contains(a)) {
                valid.Add(a);
            }
        }

        return valid;
    }

    void CheckValidSpawns() {
        
    }
}
