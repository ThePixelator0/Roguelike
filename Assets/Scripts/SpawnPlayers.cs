using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameController controller;

    void Start() {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(), Quaternion.identity);
        controller.PlayerJoinedLobby(player);
    }
}
