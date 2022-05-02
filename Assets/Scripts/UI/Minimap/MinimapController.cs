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

    private GameObject player;
    public Vector2 playerRelativePos;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRoom = Instantiate(currentRoom, centerPos, Quaternion.identity);
    }

    void FixedUpdate() {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        playerX = (playerX + 7) / 14;
        playerY = (playerY + 7) / 14;

        playerX = Mathf.Floor(playerX);
        playerY = Mathf.Floor(playerY);

        playerRelativePos = new Vector2(playerX, playerY);

        currentRoom.transform.position = (playerRelativePos * 32) + centerPos + new Vector2(1920, 1080);
    }


    public void CreateMinimapRoom(Vector2 roomPos) {
        // roomPos will be a vector of the relative pos. (0, 0) is the center. (0, 1) is one to the right, etc.
        Vector2 realPos = roomPos * 32;
        realPos += centerPos;

        Instantiate(minimapRoom, realPos, Quaternion.identity);
    }
}
