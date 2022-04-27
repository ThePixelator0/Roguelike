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
        print(GameObject.Find("Player").GetComponent<Items>().silverskull);
        if (GameObject.Find("Player").GetComponent<Items>().silverskull == true) {
            GameObject.Find("Player").SendMessage("applyHealing", Random.Range(minHealth, maxHealth) );
        }
    }
}
