using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    // This script stores values for a GameObject's stats. It does not modify them, just allows different scripts to reference and use it.
    public float speed = 2;                 // Movement Speed Multiplier
    [Space]
    public float knockbackResistance = 1;   // Resistance to Knockback Multiplier
    public float knockbackMod = 1;          // Knockback Dealt Multiplier
    [Space]
    public float damageMod = 1;             // Damage Dealt Multiplier
    public float weaknessMod = 1;           // Damage Taken Multiplier
    [Space]
    public float vision = 1;                 // Distance the GameObject can see
}
