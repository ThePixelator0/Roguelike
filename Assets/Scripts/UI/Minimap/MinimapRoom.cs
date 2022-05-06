using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapRoom : MonoBehaviour
{
    public Vector2 relativePos;
    [SerializeField]
    private Image img;

    void Start() {
        transform.SetParent(GameObject.Find("MinimapController/Minimap").transform, false);
    }

    public void Activate() {
        // print("Activating...");
        img.enabled = true;
    }
}
