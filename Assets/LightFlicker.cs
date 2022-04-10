using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private float flickTime;
    [SerializeField]
    private Light2D Flickeringlight;
    [SerializeField]
    private float flickMultiplier;
    public float flick;

    void Start() {
        flickMultiplier = Flickeringlight.intensity;
    }

    void Update() {
        flickTime += Time.deltaTime * Random.Range(0.5f, 1.5f);
        if (flickTime >= 6.28f) {
            flickTime -= 6.28f;
        }
        

        flick = Mathf.Cos(flickTime * 6.28f);

        Flickeringlight.intensity = Mathf.Pow(flick * flickMultiplier, 2) + flickMultiplier;

    }
}
