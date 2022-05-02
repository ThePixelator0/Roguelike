using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCurrentRoom : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerRelativePos;
    [SerializeField]
    private Vector2 centerPos = new Vector2(-144, -144);


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        playerRelativePos = player.transform.position / 14;
        transform.position = playerRelativePos + centerPos;
    }
}

