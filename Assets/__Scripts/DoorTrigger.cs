using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject leftDoor;
    [SerializeField]
    GameObject rightDoor;

    bool isOpened = false;

    private void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            isOpened = true;
            leftDoor.transform.position += new Vector3(0, -81, 100);
            rightDoor.transform.position += new Vector3(0, 81, 100);
        }
    }
}
