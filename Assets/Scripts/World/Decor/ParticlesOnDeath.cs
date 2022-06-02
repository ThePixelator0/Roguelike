using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticlesOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;

    public void DeathParticles(Vector3 offset = new Vector3() ) {
        PhotonNetwork.Instantiate(particles.name, transform.position + offset, Quaternion.identity);
    }
}
