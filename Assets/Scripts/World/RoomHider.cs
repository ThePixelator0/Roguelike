using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider : MonoBehaviour
{
    [SerializeField]
    private Vector3[] checkPositions;
    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        foreach (Vector3 vec in checkPositions) {
            if (PositionLOS(player.transform.position, vec + transform.position)) {
                print("Player is in LOS of " + (vec + transform.position));
                GameObject.Find("Game Controller").SendMessage("CreateMarker", vec + transform.position);
                Destroy(gameObject);
                break;
            } else {
                // print("Player is not in LOS of " + (vec + transform.position));
            }
        }
    }

    public bool PositionLOS(Vector2 pos1, Vector2 pos2) {
            Vector2 posDir = (pos2 - pos1).normalized;
            float distance = Vector2.Distance(pos1, pos2);

            RaycastHit2D hit = Physics2D.Raycast(pos1, posDir, distance);

            if (hit.collider.tag == "Player") {
                // Raycast hit the player first
                return true;
            }
            else {
                // Raycast hit something other than player
                return false;
            }
        }
}
