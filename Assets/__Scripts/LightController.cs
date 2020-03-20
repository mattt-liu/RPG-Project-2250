using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Light myLight;

    public static float defaultBrightness = 3f;
    public bool on = false;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            myLight.intensity = defaultBrightness;
        }
        else
        {
            myLight.intensity = 0f;
        }
    }
}
