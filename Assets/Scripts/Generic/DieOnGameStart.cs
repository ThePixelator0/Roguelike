using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Tilemaps;

public class DieOnGameStart : MonoBehaviourPunCallbacks, IPunObservable
{
    public void Start() {
        GameObject.Find("Room Templates").GetComponent<RoomTemplate>().SendStartMessage.Add(gameObject);
    }

    public void StartGame() {
        GetComponent<TilemapCollider2D>().enabled = false;
        GetComponent<TilemapRenderer>().enabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // Sync Data
        if (stream.IsWriting) {
            // We are writing
            stream.SendNext(GetComponent<TilemapCollider2D>().enabled);
            stream.SendNext(GetComponent<TilemapRenderer>().enabled);
        }
        else {
            // We are reading
            GetComponent<TilemapCollider2D>().enabled = (bool)stream.ReceiveNext();
            GetComponent<TilemapRenderer>().enabled = (bool)stream.ReceiveNext();
        }
    }
}
