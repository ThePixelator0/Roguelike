using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineOfSight {
    public class LOS
    {
        Vector2 pos1;
        Vector2 pos2;

        public bool PositionLOS(Vector2 pos1, Vector2 pos2, string goodTag, string ignoreTag) {
            // pos1 = current pos, pos2 = pos to check LOS on, goodTag = tag that ur looking for, ignoreTag - tag to ignore
            Vector2 posDir = (pos2 - pos1).normalized;
            float distance = Vector2.Distance(pos1, pos2);
            bool boolReturn = false;

            List<string> badTags = new List<string>(){"Player", "Boss", "Enemy", "Environment", "Shield"};
            badTags.Remove(goodTag); 
            badTags.Remove(ignoreTag); 
            if (ignoreTag == "Player") {
                badTags.Remove("Shield");
            }

            RaycastHit2D[] hits = Physics2D.RaycastAll(pos1, posDir, distance);

            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.tag == goodTag) {
                    // Raycast hit has the correct tag
                    boolReturn = true;
                }
                else if (badTags.Contains(hit.collider.tag) ) {
                    // Raycast hit something with a bad tag before the correct tag
                    return false;
                }
            }

            // If all collisions did not meet criteria
            return boolReturn;
        }
    }
}