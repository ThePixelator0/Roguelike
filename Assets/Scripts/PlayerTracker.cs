using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTracker : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<GameObject> livingPlayers;

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // Sync Data
        if (stream.IsWriting) {
            // We are writing
            stream.SendNext(livingPlayers.ToArray());
        }
        else {
            // We are reading
            livingPlayers = (List<GameObject>)stream.ReceiveNext();
        }
    }

    public GameObject GetClosestPlayer(Vector2 pos) {
        if (livingPlayers.Count == 0) return null;

        float closestDistance = Vector2.Distance(livingPlayers[0].transform.position, pos);
        GameObject closestPlayer = livingPlayers[0];

        foreach (GameObject player in livingPlayers) {
            if (player == livingPlayers[0]) continue;

            float newDistance = Vector2.Distance(livingPlayers[0].transform.position, pos);
            if (newDistance < closestDistance) {
                closestDistance = newDistance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}
