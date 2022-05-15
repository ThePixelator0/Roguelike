using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownContoller : MonoBehaviour
{
    [SerializeField]
    private List<Cooldown> cooldowns;

    public void EnableCooldown(int cooldownNum, bool enable = true) {
        if (enable) cooldowns[cooldownNum].Enable();
        else cooldowns[cooldownNum].Enable(false);
    }

    public void SetCooldown(int cooldownNum, float length) {
        // cooldownNum = what cooldown, length = length in seconds
        cooldowns[cooldownNum].SetCooldown(length);
    }
}
