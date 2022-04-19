using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
    public void Die_Boss() {
        GameObject.Find("Exit").SendMessage("ShowExit");
        Destroy(gameObject);
    }
}
