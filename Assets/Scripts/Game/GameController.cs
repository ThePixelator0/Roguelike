using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject entryRoom;

    [Space]
    public bool loadOnStart = false;

    [Space]
    public int playersInLobby = 0;
    public int livingPlayers = 0;
    
    void Start() {
        if (PlayerStats.setup != true) {
            InitPlayerVars();
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // // Sync Data
        // if (stream.IsWriting) {
        //     // We are writing
        //     stream.SendNext();
        // }
        // else {
        //     // We are reading
        //      = ()stream.ReceiveNext();
        // }
    }

    [PunRPC]
    public void PlayerJoinedLobby() {

        if (PhotonNetwork.IsMasterClient) {
            print("Master Client!");
            PhotonNetwork.InstantiateRoomObject(entryRoom.name, new Vector2(), Quaternion.identity, 0);
        }

        playersInLobby += 1;
        livingPlayers += 1;
    } 

    public void PlayerLeftLobby(GameObject player) {
        playersInLobby -= 1;
        livingPlayers -= 1;
    }

    public void PlayerDied(GameObject player) {
        livingPlayers -= 1;
    }

    void FixedUpdate() {
        if (playersInLobby > 0 && livingPlayers == 0) {
            Invoke("Restart", 2f);
        }
    }

    void Restart() {
        PlayerStats.setup = false;
        livingPlayers = playersInLobby;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel() {
        SaveGame();

        RoomTemplate template = GameObject.Find("Room Templates").GetComponent<RoomTemplate>();

        template.spawnQueue = new List<GameObject>();
        template.rooms = new List<GameObject>();
        template.positions = new List<Vector2>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy") ) {
            Destroy(enemy);
        }
        
        foreach (GameObject boss in GameObject.FindGameObjectsWithTag("Boss") ) {
            Destroy(boss);
        }
        
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room") ) {
            Destroy(room);
        }

        foreach (Transform child in GameObject.Find("Grid").transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in GameObject.Find("MinimapController/Hallways").transform) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("MinimapController/Minimap").transform) {
            Destroy(child.gameObject);
        }


        PhotonNetwork.InstantiateRoomObject(entryRoom.name, new Vector2(0, 0), Quaternion.identity, 0);

        
        template.Start();
    }


    void InitPlayerVars() {
        PlayerStats.speedMod = 1;
        PlayerStats.stealthMod = 1;
        PlayerStats.damageMod = 1;
        PlayerStats.weaknessMod = 1;

        PlayerStats.canTakeDamage = true;

        PlayerItems.items = new List<int>();
    }

    void LoadGame() {
        // SAVES BROKE WHEN ADDED MULTIPLAYER. FIX IT.

    //     PlayerData data = SaveSystem.LoadPlayer();
    //     if (data == null) return;

    //     // Health playerHP = player.GetComponent<Health>();
    //     playerHP.health = data.health;
    //     playerHP.maxHealth = data.maxHealth;

    //     foreach (int item in data.items) {
    //         PlayerItems.PickupItem(item);
    //     }

    //     playerClass.ChangeClass(data.currentClass);
    }

    void SaveGame() {
    //     SaveSystem.SavePlayer(player.GetComponent<Health>());
    }
}
