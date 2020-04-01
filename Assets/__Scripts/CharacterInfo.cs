 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public string CharacterType;

    [HideInInspector]
    public string Name;

    public int health;
    public int strength;
    public int speed;
}
