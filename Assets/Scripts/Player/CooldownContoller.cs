using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownContoller : MonoBehaviour
{
    [SerializeField]
    private List<Cooldown> cooldowns;

    public void EnableCooldown(int cooldownNum) {
        cooldowns[cooldownNum].Enable();
    }

    public void SetCooldown(Vector2 cooldownInfo) {
        // cooldownInfo.x = what cooldown, y = length in seconds
        cooldowns[(int)cooldownInfo.x].SetCooldown(cooldownInfo.y);
    }
}
