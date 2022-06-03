using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateEnemy : MonoBehaviour
{


    public void OnPhotonInstantiate (PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        transform.name = (string)info.photonView.InstantiationData[0];
    }
}
