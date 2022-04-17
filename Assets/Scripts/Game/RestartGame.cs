using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject player;
    public GameObject entryRoom;
    
    void Start() {
        player = GameObject.Find("Player");

        if (PlayerStats.setup != true) {
            InitPlayerVars();
        }
    }

    void Update() {
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

        GameObject.Find("Player").transform.position = new Vector3(0, 0, 0);
        Instantiate(entryRoom, new Vector2(0, 0), Quaternion.identity);

        
        template.Start();
    }


    void InitPlayerVars() {
        PlayerStats.speedMod = 1;
        PlayerStats.stealthMod = 1;
        PlayerStats.damageMod = 1;

        PlayerItems.items = new List<int>();
    }
}
