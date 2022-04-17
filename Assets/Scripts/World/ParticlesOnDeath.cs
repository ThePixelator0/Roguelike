using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;

    public void DeathParticles(Vector3 offset = new Vector3() ) {
        Instantiate(particles, transform.position + offset, Quaternion.identity);
    }
}
