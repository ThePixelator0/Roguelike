using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject player;
    public GameObject entryRoom;
    public ClassController playerClass;

    [Space]
    public bool loadOnStart = false;
    
    void Start() {
        player = GameObject.Find("Player");

        if (PlayerStats.setup != true) {
            InitPlayerVars();
        }

        if (loadOnStart) LoadGame();
    }

    void FixedUpdate() {
        if (player == null) {
            Invoke("Restart", 2f);
        }
    }

    void Restart() {
        PlayerStats.setup = false;
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


        player.transform.position = new Vector3(0, 0, 0);
        Instantiate(entryRoom, new Vector2(0, 0), Quaternion.identity);

        
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
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null) return;

        Health playerHP = player.GetComponent<Health>();
        playerHP.health = data.health;
        playerHP.maxHealth = data.maxHealth;

        foreach (int item in data.items) {
            PlayerItems.PickupItem(item);
        }

        playerClass.ChangeClass(data.currentClass);
    }

    void SaveGame() {
        SaveSystem.SavePlayer(player.GetComponent<Health>());
    }
}
