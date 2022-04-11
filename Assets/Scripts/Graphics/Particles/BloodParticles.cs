using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    [SerializeField]
    private float deathTimer;
    
    void Start() {
        transform.parent = GameObject.Find("Parents/Particles").transform;
    }


    void Update()
    {
        if (deathTimer > 0){
            deathTimer -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
        
    }
    
}
