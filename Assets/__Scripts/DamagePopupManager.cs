using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    #region Singleton

    public static DamagePopupManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("super bug ");
            Destroy(gameObject);
        }
    } 

    #endregion

    [SerializeField]
    private GameObject damagePopupPrefab;

    public void DisplayDamagePopup(int amount, Transform popupParent)
    {
        GameObject GO = Instantiate(damagePopupPrefab, popupParent.transform.position, Quaternion.identity, popupParent);
        GO.GetComponent<DamagePopup>(). SetUp(amount);
    }

}

