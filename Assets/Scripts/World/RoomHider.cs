using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider : MonoBehaviour
{
    private Vector3 roomPos;
    public string playerRoom;
    public GameObject player;
    private GameObject templates;

    void Start() {
        templates = GameObject.Find("Room Templates");
    }

    void Update()
    {   
        transform.position = RoomPos(player.transform.position) + new Vector3Int(0, 1, 0);

        if (player != null) {
            Vector3 newRoomPos = RoomPos(player.transform.position);
            if (newRoomPos == roomPos) {

            } else {
                roomPos = newRoomPos;
                playerRoom = RoomNameAtPos(roomPos / 14);
            }
        }
    }

    Vector3 RoomPos(Vector3 pos) {
        // Returns the position of the middle of the closest room
        pos = ( (pos + new Vector3(7 * (pos.x / Mathf.Abs(pos.x)), 7 * (pos.y / Mathf.Abs(pos.y)), 0)) / 14);
        return new Vector3Int((int)pos.x, (int)pos.y, 0) * 14;
    }

    public string RoomNameAtPos(Vector2 pos) {
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
