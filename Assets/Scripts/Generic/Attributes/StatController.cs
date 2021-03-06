using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StatController : MonoBehaviourPunCallbacks
{
    // This script stores values for a GameObject's stats. It does not modify them, just allows different scripts to reference and use it.
    public float speedMod = 1;              // Movement Speed Multiplier
    [Space]
    public float knockbackResistance = 1;   // Resistance to Knockback Multiplier
    public float knockbackMod = 1;          // Knockback Dealt Multiplier
    [Space]
    public float damageMod = 1;             // Damage Dealt Multiplier
    public float weaknessMod = 1;           // Damage Taken Multiplier
    public bool canTakeDamage = true;       // Can take damage?
    [Space]
    public float vision = 1;                 // Distance the GameObject can see
    [HideInInspector] public string objectTag;                 // Distance the GameObject can see

    void Start() {
        objectTag = gameObject.tag;
    }
}
