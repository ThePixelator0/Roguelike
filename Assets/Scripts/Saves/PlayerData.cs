using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Health
    public float health;
    public float maxHealth;

    // PlayerStats
    public float stealthMod;
    public float damageMod;
    public float weaknessMod;
    public float speedMod;

    public bool canTakeDamage;

    public int currentClass; 

    public List<int> items;


    public PlayerData (Health hp) {
        health = hp.health;
        maxHealth = hp.maxHealth;

        // stealthMod = PlayerStats.stealthMod;
        // damageMod = PlayerStats.damageMod;
        // weaknessMod = PlayerStats.weaknessMod;
        // speedMod = PlayerStats.speedMod;
        // canTakeDamage = PlayerStats.canTakeDamage;
        currentClass = PlayerStats.currentClass;

        items = PlayerItems.items;
    }
}
