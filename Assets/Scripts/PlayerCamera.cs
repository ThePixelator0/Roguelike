using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviour
{
    PhotonView view;

    void Start() {
        view = transform.parent.GetComponent<PhotonView>();

        if (view.IsMine) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
