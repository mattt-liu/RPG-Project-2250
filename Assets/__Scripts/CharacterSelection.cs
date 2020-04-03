using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];

        //fill array with character models
        for(int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;
        
        //toggle off all models
        foreach (GameObject go in characterList)
            go.SetActive(false);

        //toggle on the FIRST index OR THE SELECTED CHARACTER IF ONE WAS ALREADY CHOSEN
        if (characterList[index])       //note index is zero by default if character isnt already chosen, if one is chosen it will be set to index
            characterList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        //toggle off the current model
        characterList[index].SetActive(false);
        index--;
        if (index < 0)
            index = characterList.Length - 1;

        //toggle on the new model
        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        //toggle off the current model
        characterList[index].SetActive(false);
        index++;
        if (index == characterList.Length)
            index = 0;

        //toggle on the new model
        characterList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Debug.Log(PlayerPrefs.GetInt("CharacterSelected"));
    }
}

