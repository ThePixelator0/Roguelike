using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    // Movement Vars
    public bool canDash = true;

    [Space]
    public Rigidbody2D rb;
    public Animator animator;
    public PhotonView view;
    [Space]
    public float speed = 5000;                 // Speed of normal movement
    public float maxSpeed = 4;
    
    public bool canMove = true;
    Vector2 inputs;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        // Sync Data
        if (stream.IsWriting) {
            // We are writing
            stream.SendNext(transform.position);
        }
        else {
            // We are reading
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    
    void Awake() {
        view = transform.parent.GetComponent<PhotonView>();
        if (view == null) print("PlayerMovement PhotonView is null!");
    }

    void Update() {
        inputs = GetInputs();
    }

    void FixedUpdate() {
        if (view.IsMine) {
            if (canMove) {
                if (rb.velocity.magnitude <= maxSpeed * PlayerStats.speedMod) {
                    rb.AddForce(inputs * speed * Time.fixedDeltaTime * PlayerStats.speedMod);
                }
                
                animator.SetFloat("Speed_X", rb.velocity.x);
                animator.SetFloat("Speed_Y", rb.velocity.y);
            }
        }
    }

    
    public Vector2 GetInputs() {
        // Get the inputs for vertical and horizontal movement
        Vector2 inputs;

        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");

        inputs = inputs.normalized;

        return inputs;
    }
}
