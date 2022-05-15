using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    // This script stores variables for the player that other scripts may want to access.
    public static bool setup { get; set; }  // Have these vars been setup yet?

    public static float stealthMod { get; set; }    // How far enemies can see you
    public static float damageMod { get; set; }     // Damage dealth multiplier
    public static float weaknessMod { get; set; }   // Damage Taken multiplier
    public static float speedMod { get; set; }      // movement speed multiplier

    public static bool canTakeDamage { get; set; }  // Can take damage?
}
