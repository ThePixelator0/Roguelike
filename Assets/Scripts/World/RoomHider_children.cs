using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHider_children : MonoBehaviour
{
    [SerializeField]
    private string dir;
    [SerializeField]
    private SpriteRenderer rend;

    private RoomHider hider;
    private RoomTemplate templates;

    void Start() {
        hider = transform.parent.GetComponent<RoomHider>();
        templates = GameObject.Find("Room Templates").GetComponent<RoomTemplate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hider.playerRoom == "Entry Room") {
            rend.enabled = false;
        } 
    
        else {
            switch (dir) {
                case "l":
                    if (templates.leftRooms_Names.Contains(hider.playerRoom)) {
                        rend.enabled = false;
                    } else {
                        rend.enabled = true;
                    }
                    break;
                case "r":
                    if (templates.rightRooms_Names.Contains(hider.playerRoom)) {
                        rend.enabled = false;
                    } else {
                        rend.enabled = true;
                    }
                    break;
                case "t":
                    if (templates.topRooms_Names.Contains(hider.playerRoom)) {
                        rend.enabled = false;
                    } else {
                        rend.enabled = true;
                    }
                    break;
                case "b":
                    if (templates.bottomRooms_Names.Contains(hider.playerRoom)) {
                        rend.enabled = false;
                    } else {
                        rend.enabled = true;
                    }
                    break;

                case "tr":
                    if ((templates.topRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ||
                        (templates.rightRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ) {

                        rend.enabled = true;
                    } else {
                        rend.enabled = false;
                    }
                    break;
                case "tl":
                    if ((templates.topRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ||
                        (templates.leftRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ) {

                        rend.enabled = true;
                    } else {
                        rend.enabled = false;
                    }
                    break;
                case "br":
                    if ((templates.bottomRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ||
                        (templates.rightRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ) {

                        rend.enabled = true;
                    } else {
                        rend.enabled = false;
                    }
                    break;
                case "bl":
                    if ((templates.bottomRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ||
                        (templates.leftRooms_Names.Contains( hider.RoomNameAtPos(transform.position / 14) )) ) {

                        rend.enabled = true;
                    } else {
                        rend.enabled = false;
                    }
                    break;



                default:
                    print("dir has an invalid direction");
                    break;
            }
        }
    }
}
