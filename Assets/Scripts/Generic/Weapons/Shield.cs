using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private TilemapRenderer rend;
    [SerializeField]
    private BoxCollider2D selfCol;

    public bool shieldEnabled = false;

    void Start() {
        rend.enabled = false;
        selfCol.enabled = false;
    }

    void Update() {
        if (shieldEnabled) {
            transform.position = transform.parent.position;

            if ( (Input.GetAxis("Secondary") != 0) ) {
                FaceMouse();
                rend.enabled = true;
                selfCol.enabled = true;
            } else {
                rend.enabled = false;
                selfCol.enabled = false;
                shieldEnabled = false;
            }
        }
    }

    void FaceMouse(float offset = 0f) {
        // Face towards Mouse

        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f; // Distance between the camera and the object

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 + offset));
    }

    public void EnableShield() {
        shieldEnabled = true;
    }
}
