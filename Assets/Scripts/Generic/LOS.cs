using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineOfSight {
    public class LOS
    {
        public bool PositionLOS(Vector2 pos1, Vector2 pos2, string goodTag) {
            Vector2 posDir = (pos2 - pos1).normalized;
            RaycastHit2D hit = Physics2D.Raycast(pos1, posDir);


            if (hit.collider == null) {
                // Raycast hit nothing
                return false;
            }
            else if (hit.collider.tag == goodTag) {
                // Raycast hit something with the right tag
                return true;
            }
            else {
                // Raycast hit something with the wrong tag
                return false;
            }
        }
    }
}