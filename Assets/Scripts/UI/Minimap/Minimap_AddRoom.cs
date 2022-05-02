using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_AddRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MinimapController").GetComponent<MinimapController>().CreateMinimapRoom(transform.position / 14);
    }
}
