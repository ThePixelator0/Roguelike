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
            templates.waitTime = .4f;
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                // Two rooms spawned at the same pos and time
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
