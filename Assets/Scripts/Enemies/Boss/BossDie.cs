using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossDie : MonoBehaviourPunCallbacks
{
    public void Die_Boss() {
        GameObject.Find("Exit").SendMessage("ShowExit");
        Destroy(gameObject);
    }
}
