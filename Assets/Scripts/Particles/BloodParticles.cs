using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public float deathTimer;

    // Update is called once per frame
    void Update()
    {

        if (deathTimer > 0){
            deathTimer -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
        
    }
    
}
