using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapRoom;
    [SerializeField]
    private Vector2 centerPos = new Vector2(-144, -144);
    [SerializeField]
    private GameObject currentRoom;

    [SerializeField]
    private GameObject hallwayVertical;
    [SerializeField]
    private GameObject hallwayHorizontal;

    private GameObject player;
    public Vector2 playerRelativePos;

    private List<Vector2> minimapRoomPositions;
    private List<GameObject> minimapRooms;

    public Vector2 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRoom = Instantiate(currentRoom, centerPos, Quaternion.identity);
        minimapRoomPositions = new List<Vector2>();
        minimapRooms = new List<GameObject>();
    }

    public void CreateMinimapCurrentRoom() {
        currentRoom = Instantiate(currentRoom, centerPos, Quaternion.identity);
    }

    void FixedUpdate() {
        if (player != null) {    
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;

            playerX = (playerX + 7) / 14;
            playerY = (playerY + 7) / 14;

            playerX = Mathf.Floor(playerX);
            playerY = Mathf.Floor(playerY);

            playerRelativePos = new Vector2(playerX, playerY);

            currentRoom.transform.position = (playerRelativePos * 32) + centerPos + new Vector2(1920, 1080);

            if (playerRelativePos*32 + centerPos == lastPos) {
            } else {
                lastPos = playerRelativePos;
                CheckPos(lastPos);
            }
        }
    }


    public void CreateMinimapRoom(Vector2 roomPos, bool[] directions) {
        // roomPos will be a vector of the relative pos. (0, 0) is the center. (0, 1) is one to the right, etc.
        Vector2 realPos = roomPos * 32;
        realPos += centerPos;

        GameObject clone = Instantiate(minimapRoom, realPos, Quaternion.identity);
        minimapRooms.Add(clone);
        minimapRoomPositions.Add(roomPos);

        // 0, 1, 2, 3 is top, bottom, left, right respectively 
        if (directions[0]) {
            clone = Instantiate(hallwayVertical, realPos + new Vector2(0, 16), Quaternion.identity);
            minimapRooms.Add(clone);
            minimapRoomPositions.Add(roomPos);
        }
        if (directions[1]) {
            clone = Instantiate(hallwayVertical, realPos + new Vector2(0, -16), Quaternion.identity);
            minimapRooms.Add(clone);
            minimapRoomPositions.Add(roomPos);
        }
        if (directions[2]) {
            clone = Instantiate(hallwayHorizontal, realPos + new Vector2(-16, 0), Quaternion.identity);
            minimapRooms.Add(clone);
            minimapRoomPositions.Add(roomPos);
        }
        if (directions[3]) {
            clone = Instantiate(hallwayHorizontal, realPos + new Vector2(16, 0), Quaternion.identity);
            minimapRooms.Add(clone);
            minimapRoomPositions.Add(roomPos);
        }
    }

    public void CheckPos(Vector2 pos) {
        int i = 0;
        foreach (Vector2 roomPos in minimapRoomPositions) {
            if (minimapRooms[i] == null) {
                i++;
                continue;
            }

            if (roomPos == pos) {
                minimapRooms[i].SendMessage("Activate");
            }
            i++;
        }
    }
}
