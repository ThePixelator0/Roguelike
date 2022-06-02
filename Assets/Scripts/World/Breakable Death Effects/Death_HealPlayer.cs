using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_HealPlayer : MonoBehaviour
{
    [SerializeField]
    private float minHealth;
    [SerializeField]
    private float maxHealth;
    

    public void DeathEffect() {
        // This needs to get redone since multiplayer happened
        if (GameObject.FindWithTag("Player").GetComponent<Items>().silverskull == true) {
            GameObject.FindWithTag("Player").SendMessage("applyHealing", Random.Range(minHealth, maxHealth) );
        }
    }
}
