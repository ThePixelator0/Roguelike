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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        }
    }


    public void CreateMinimapRoom(Vector2 roomPos, bool[] directions) {
        // roomPos will be a vector of the relative pos. (0, 0) is the center. (0, 1) is one to the right, etc.
        Vector2 realPos = roomPos * 32;
        realPos += centerPos;

        Instantiate(minimapRoom, realPos, Quaternion.identity);

        // 0, 1, 2, 3 is top, bottom, left, right respectively 
        if (directions[0]) {
            Instantiate(hallwayVertical, realPos + new Vector2(0, 16), Quaternion.identity);
        }
        if (directions[1]) {
            Instantiate(hallwayVertical, realPos + new Vector2(0, -16), Quaternion.identity);
        }
        if (directions[2]) {
            Instantiate(hallwayHorizontal, realPos + new Vector2(-16, 0), Quaternion.identity);
        }
        if (directions[3]) {
            Instantiate(hallwayHorizontal, realPos + new Vector2(16, 0), Quaternion.identity);
        }
    }
}
