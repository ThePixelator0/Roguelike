using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public List<GameObject> livingPlayers;

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
