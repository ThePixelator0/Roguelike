using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    private float cooldown;

    [SerializeField]
    private Image img;
    [SerializeField]
    private Text txt;

    public void Enable(bool enable = true) {
        img.enabled = enable;
        txt.enabled = enable;
    }

    public void SetCooldown(float num) {
        cooldown = num;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
        if (cooldown < 0) cooldown = 0;

        string floor = Mathf.Ceil(cooldown).ToString();
        if (floor == "0") {
            txt.text = null;
        } else if (txt.text != floor) {
            txt.text = floor;
        }
        
        if (txt.text == "") {
            img.color = Color.green;
        } else {
            img.color = Color.red;
        }
    }
}
