using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBroadcaster : MonoBehaviour
{
    public void GameStart() {
        BroadcastMessage("GameStart");
    }
}
