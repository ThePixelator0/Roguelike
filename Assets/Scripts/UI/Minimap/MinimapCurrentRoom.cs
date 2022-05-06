// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MinimapCurrentRoom : MonoBehaviour
// {
//     private GameObject player;
//     private Vector2 playerRelativePos;
//     [SerializeField]
//     private Vector2 centerPos = new Vector2(-144, -144);
//     [SerializeField]
//     private MinimapController controller;

//     Vector3 lastPos;


//     // Start is called before the first frame update
//     void Start()
//     {
//         player = GameObject.FindWithTag("Player");
//         controller = transform.parent.GetComponent<MinimapController>();
//     }

//     void FixedUpdate() {
//         playerRelativePos = player.transform.position / 14;
//         playerRelativePos = new Vector2(Mathf.Round(playerRelativePos.x), Mathf.Round(playerRelativePos.y));
//         transform.position = playerRelativePos + centerPos;



//         if (transform.position == lastPos) {
//             print("Butt");
//         } else {
//             print("Here");
//             lastPos = transform.position;
//             controller.CheckPos(lastPos);
//         }
//     }


// }

