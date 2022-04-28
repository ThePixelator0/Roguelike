using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider : MonoBehaviour
{
    private Vector3 roomPos;
    public string playerRoom;
    private GameObject player;
    private GameObject templates;

    void Start() {
        player = GameObject.Find("Player");
        templates = GameObject.Find("Room Templates");
    }

    void FixedUpdate()
    {
        if (player != null) {
            roomPos = RoomPos(player.transform.position);
            transform.position = roomPos + new Vector3(0, 1, 0);
            playerRoom = RoomNameAtPos(roomPos / 14);
        }
    }

    Vector3 RoomPos(Vector3 pos) {
        // Returns the position of the middle of the closest room
        pos = ( (pos + new Vector3(7 * (pos.x / Mathf.Abs(pos.x)), 7 * (pos.y / Mathf.Abs(pos.y)), 0)) / 14);
        return new Vector3Int((int)pos.x, (int)pos.y, 0) * 14;
    }

    string RoomNameAtPos(Vector2 pos) {
        // Returns the name of the room at a position
        RoomTemplate template = templates.GetComponent<RoomTemplate>();
        if (template.positions.Contains(pos)) {
            int index = template.positions.IndexOf(pos);
            return template.rooms[index].name;
        } else {
            return "Entry Room";
        }
        
    }
}
