using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapHallway : MonoBehaviour
{
    [SerializeField]
    private Image img;

    void Start() {
        transform.SetParent(GameObject.Find("MinimapController/Hallways").transform, false);
    }

    public void Activate() {
        img.enabled = true;
    }
}
