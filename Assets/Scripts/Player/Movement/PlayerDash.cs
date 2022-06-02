using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDash : MonoBehaviour
{
    public PlayerMovement movement;
    public CooldownContoller cooldown;
    [Space]
    public float dashSpeed = 1500f;         // How fast dash is compared to regular speed
    public float dashCooldown = 0f;         // How long until next dash available
    public float timeBetweenDashes = 2f;    // How long between start of each dash
    
    bool dashButton;

    PhotonView view;

    void Start() {
        view = transform.parent.GetComponent<PhotonView>();
    }

    void Update() {
        if (view.IsMine) dashButton = (Input.GetAxis("Dash") == 0 || !movement.canDash) ? false : true;
    }

    void FixedUpdate() {
        if (dashButton && dashCooldown <= 0) {
            Dash();
        }

        if (dashCooldown > 0) {
            dashCooldown -= Time.fixedDeltaTime;
            if (dashCooldown < 0) dashCooldown = 0;
        }
    }

    void Dash() {
        dashCooldown = timeBetweenDashes;
        Vector2 dashDir = movement.GetInputs();
        
        movement.rb.AddForce(dashDir * dashSpeed * PlayerStats.speedMod);

        cooldown.SetCooldown(0, dashCooldown);
    }
}
