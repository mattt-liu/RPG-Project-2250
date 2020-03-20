using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Level Lights")]
    public Light[] lights;

    [Header("Player")]
    public PlayerController player;

    private int _currentLevel;

    void Start()
    {
        _currentLevel = 1;
        player.currentLevel = _currentLevel;

        foreach (Light l in lights)
        {
            l.intensity = 0;
        }
        lights[0].intensity = LightController.defaultBrightness;
    }

    void Update()
    {
    }
    private void UpdateLight()
    {
        // change lighting
        lights[_currentLevel - 1].intensity = LightController.defaultBrightness;
    }
}
