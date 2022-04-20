using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineOfSight {
    public class LOS
    {
        public bool PositionLOS(Vector2 pos1, Vector2 pos2, string goodTag) {
            Vector2 posDir = (pos2 - pos1).normalized;
            float distance = Vector2.Distance(pos1, pos2);

            List<string> badTags = new List<string>("Player", "Boss", "Enemy", "Environment");
            badTags.Remove(goodTag); 

            List<RaycastHit2D> hits = Physics2D.RaycastAll(pos1, posDir);

            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.collider.tag == goodTag) {
                    // Raycast hit has the correct tag
                    return true;
                }
                else if (badTags.Contains(hit.collider.tag) ) {
                    // Raycast hit something with a bad tag before the correct tag
                    print("Raycast hit " + hit.collider.tag + " before " + goodTag);
                    return false;
                }
            }

            // If all collisions did not meet criteria
            return false;
        }
    }
}