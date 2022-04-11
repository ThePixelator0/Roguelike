using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject player;
    
    void Start() {
        player = GameObject.Find("Player");
    }

    void Update() {
        if (player == null) {
            Invoke("Restart", 2f);
        }
    }

    void Restart() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
