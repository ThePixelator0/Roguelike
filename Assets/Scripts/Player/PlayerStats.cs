using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    // This script stores variables for the player that other scripts may want to access.
    public static bool setup { get; set; }  // Have these vars been setup yet?

    public static float stealthMod { get; set; }
    public static float damageMod { get; set; }
    public static float speedMod { get; set; }
}
