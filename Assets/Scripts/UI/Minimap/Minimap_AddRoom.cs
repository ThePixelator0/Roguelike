using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_AddRoom : MonoBehaviour
{
                                // Top, Bottom, Left, Right
    public bool[] directions = {false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MinimapController").GetComponent<MinimapController>().CreateMinimapRoom(transform.position / 14, directions);
    }
}
