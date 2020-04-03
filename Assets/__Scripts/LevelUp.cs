using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
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
    private bool iscalled = false;
    private int n = 0;




    void Update()
    {
        if (iscalled==false)
        {
            levelup();
        }


    }





    void levelup()
    {

        for (n=1; n<level; n++ )
        {
            LeftShoulder.transform.localScale += new Vector3(.15f, .20f, .15f);
            RightShoulder.transform.localScale += new Vector3(.15f, .20f, .15f);
            LeftUppersArm.transform.localScale += new Vector3(.15f, -0.15f, .15f);
            RightUpperArm.transform.localScale += new Vector3(.15f, -0.15f, .15f);
            //LeftLowerArm.transform.localScale += new Vector3(00f, 0f, .0015f);
            //RightLowerArm.transform.localScale += new Vector3(.0015f, 0f, .0015f);
            UpperLegRight.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            LowerLegRight.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            UpperLegLeft.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            LowerLegLeft.transform.localScale += new Vector3(.15f, 0.15f, .15f);
            Body.transform.localScale += new Vector3(0f, .25f, 0f);

            iscalled = true;

        }
        

            
        


    }
}
    
