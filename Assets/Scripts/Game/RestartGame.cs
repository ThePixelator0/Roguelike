using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject player;
    
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


    void InitPlayerVars() {
        PlayerStats.speedMod = 1;
        PlayerStats.stealthMod = 1;
        PlayerStats.damageMod = 1;
    }
}
