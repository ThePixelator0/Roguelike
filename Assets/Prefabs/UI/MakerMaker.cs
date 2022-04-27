using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerMaker : MonoBehaviour
{
    public GameObject marker;

    public void CreateMarker(Vector3 pos) {
        Instantiate(marker, pos, Quaternion.identity);
    }
}
