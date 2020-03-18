using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject rightLeg;

    public void Move()
    {
        Vector3 pos = leftLeg.transform.position;
    }
}
