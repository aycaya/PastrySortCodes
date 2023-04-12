using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelectLevel : MonoBehaviour
{
    int lastLevel = 0;
    GameObject selectLevelParent;
    Button[] levelButtons;
    GameObject[] levelsObjects;
    int[] levelsStarsCount;
    GameObject[] levelsStarsObjects;
    private void Awake()
    {
        lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        selectLevelParent = GameObject.Find("SelectLevel");

        levelsObjects = new GameObject[selectLevelParent.transform.childCount];
        levelButtons = new Button[selectLevelParent.transform.childCount];
        levelsStarsCount = new int[lastLevel];
        levelsStarsObjects = new GameObject[lastLevel];

        for (int i = 0; i < selectLevelParent.transform.childCount; i++)
        {
            levelsObjects[i] = selectLevelParent.transform.GetChild(i).gameObject;
        }
        for (int j = 0; j < selectLevelParent.transform.childCount; j++)
        {
            levelButtons[j] = levelsObjects[j].GetComponentInChildren<Button>();

        }
        for (int j = 0; j < lastLevel; j++)
        {
            levelsStarsObjects[j] = levelsObjects[j].transform.Find("Stars").gameObject;

        }
        OpenLockedLevels(lastLevel);
        LevelStarSystem(lastLevel - 1);

    }

    public void SelectLevelButton(int level)
    {
        PlayerPrefs.SetInt("ActiveLevel", level);
        SceneManager.LoadScene(2);
    }
    void OpenLockedLevels(int final)
    {
        for (int i = 0; i < final; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
    void LevelStarSystem(int final)
    {
        for (int i = 0; i < final; i++)
        {
            levelsStarsCount[i] = PlayerPrefs.GetInt("LevelStars" + (i + 1), 3);
        }
        for (int i = 0; i < final; i++)
        {
            for (int j = 0; j < levelsStarsCount[i]; j++)
            {
                levelsStarsObjects[i].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }
}
