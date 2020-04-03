using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [Header("Player Object")]
    public GameObject playerObject;
    private PlayerController player;

    [Header("Level")]
    public int level;
    public GameObject LeftShoulder;
    public GameObject RightShoulder;
    public GameObject Body;
    public GameObject LeftUppersArm;
    public GameObject RightUpperArm;
    public GameObject LeftLowerArm;
    public GameObject RightLowerArm;
    public GameObject UpperLegRight;
    public GameObject LowerLegRight;
    public GameObject UpperLegLeft;
    public GameObject LowerLegLeft;
    public GameObject Head;
    private int n = 0;

    private void Start()
    {
        player = playerObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (player.BodyLevelUp)
        {
            levelup();
            player.BodyLevelUp = false;
        }
    }

    void levelup()
    {
        
        for (n=-3; n<level; n++ )
        {
            LeftShoulder.transform.localScale += new Vector3(.2f, .2f, .2f);
            RightShoulder.transform.localScale += new Vector3(.2f, .2f, .2f);
            LeftUppersArm.transform.localScale += new Vector3(.15f, -0.15f, .15f);
            RightUpperArm.transform.localScale += new Vector3(.15f, -0.15f, .15f);
            LeftLowerArm.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
            RightLowerArm.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
            UpperLegRight.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            LowerLegRight.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            UpperLegLeft.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            LowerLegLeft.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            Body.transform.localScale += new Vector3(0.2f, 0f, 0f);
            Head.transform.localScale += new Vector3(-0.15f, 0f, -0.1f);


        }






    }
}
    
