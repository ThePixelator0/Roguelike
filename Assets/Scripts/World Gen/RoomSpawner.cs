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
    private Collider2D passOther;   // dumb workaround

    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.5f);
    }

    void Spawn() {
        if (spawned == false) {
            // No Requirements
            if (req == null) {  
                if (openingDirection == 1) {
                    // Need to spawn a room with a BOTTOM door.
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 2) {
                    // Need to spawn a room with a TOP door.
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 3) {
                    // Need to spawn a room with a RIGHT door.
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 4) {
                    // Need to spawn a room with a LEFT door.
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                }
            }

            // Requirements
            else { 
                List<GameObject> validRooms;
                
                if (openingDirection == 1) {
                    validRooms = new List<GameObject>(templates.bottomRooms);

                    foreach (int i in req) {
                        if (i == 2) {
                            validRooms = GetValidRooms(validRooms, templates.topRooms);
                        } else if (i == 3) {
                            validRooms = GetValidRooms(validRooms, templates.rightRooms);
                        } else if (i == 4) {
                            validRooms = GetValidRooms(validRooms, templates.leftRooms);
                        }
                    }

                    
                    rand = Random.Range(0, validRooms.Count);
                    Instantiate(validRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 2) {
                    validRooms = new List<GameObject>(templates.topRooms);

                    foreach (int i in req) {
                        if (i == 1) {
                            validRooms = GetValidRooms(validRooms, templates.bottomRooms);
                        } else if (i == 3) {
                            validRooms = GetValidRooms(validRooms, templates.rightRooms);
                        } else if (i == 4) {
                            validRooms = GetValidRooms(validRooms, templates.leftRooms);
                        }
                    }

                    
                    rand = Random.Range(0, validRooms.Count);
                    Instantiate(validRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 3) {
                    validRooms = new List<GameObject>(templates.rightRooms);

                    foreach (int i in req) {
                        if (i == 1) {
                            validRooms = GetValidRooms(validRooms, templates.bottomRooms);
                        } else if (i == 2) {
                            validRooms = GetValidRooms(validRooms, templates.topRooms);
                        } else if (i == 4) {
                            validRooms = GetValidRooms(validRooms, templates.leftRooms);
                        }
                    }

                    
                    rand = Random.Range(0, validRooms.Count);
                    Instantiate(validRooms[rand], transform.position, Quaternion.identity);
                } else if (openingDirection == 4) {
                    validRooms = new List<GameObject>(templates.leftRooms);

                    foreach (int i in req) {
                        if (i == 1) {
                            validRooms = GetValidRooms(validRooms, templates.bottomRooms);
                        } else if (i == 2) {
                            validRooms = GetValidRooms(validRooms, templates.topRooms);
                        } else if (i == 3) {
                            validRooms = GetValidRooms(validRooms, templates.rightRooms);
                        }
                    }

                    
                    rand = Random.Range(0, validRooms.Count);
                    Instantiate(validRooms[rand], transform.position, Quaternion.identity);
                }  

            }
            templates.waitTime = .4f;
            spawned = true;
        }
    }

    void SameSpotRooms() {
        if (passOther.CompareTag("SpawnPoint")) {
            if (passOther.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // Two rooms spawned at the same pos
                passOther.GetComponent<RoomSpawner>().req.Add(openingDirection);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        passOther = other;
        Invoke("SameSpotRooms", openingDirection / 10);
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
}
