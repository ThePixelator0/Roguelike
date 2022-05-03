using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    [SerializeField]
    private Vector2 lowerBounds = new Vector2(-4.5f, -4.5f);
    [SerializeField]
    private Vector2 upperBounds = new Vector2(5.5f, 5.5f);

    [Space]
    [SerializeField]
    private bool coverEnterance = true;


    // Start is called before the first frame update
    void Start()
    {
        int xpos = (int)Random.Range(lowerBounds.x, upperBounds.x);
        int ypos = (int)Random.Range(lowerBounds.y, upperBounds.y);

        if (!coverEnterance) {
            while (xpos <= 1 && xpos >= -1) {
                xpos = (int)Random.Range(lowerBounds.x, upperBounds.x);
            }
        }

        Vector2 pos = new Vector2(xpos + transform.position.x, ypos + transform.position.y);
        
        transform.position = pos;
    }
}
