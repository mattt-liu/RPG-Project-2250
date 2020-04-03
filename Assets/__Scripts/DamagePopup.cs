using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    //move upwards over time and fade away and destroy
    private TextMeshPro textMesh; //reference to our text
    private Color textColor; //change the alpha -> fade out
    private Transform playerTransform; //look towards the player

    private float disappearTimer = 0.5f;
    private float fadeOutSpeed = 5f;
    private float moveYSpeed = 1f;

    public void SetUp(int amount)
    {
        textMesh = GetComponent<TextMeshPro>();
        //playerTransform = GameObject.FindGameObjectsWithTag("Player").transform;
        playerTransform = Camera.main.transform;
        textColor = textMesh.color;
        textMesh.SetText(amount.ToString());
    }

        private void LateUpdate()
        {
            transform.LookAt(2 * transform.position - playerTransform.position);
            transform.position += new Vector3(0f, moveYSpeed * Time.deltaTime, 0f); //move upwards

            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f)
            {
                textColor.a -= fadeOutSpeed * Time.deltaTime;   //reduce the alpha in the text so it fades
                textMesh.color = textColor;
                if (textColor.a <= 0f)  //if alpha colour is less than zero destroy the object
                {
                    Destroy(gameObject);
                }
            }

        }

    
}
