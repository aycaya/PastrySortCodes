using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLevelLoader : MonoBehaviour
{
    public int currentLevel = -1;
    GameObject LevelsGameObject;
    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("ActiveLevel", 0);
        LevelsGameObject = GameObject.Find("Levels");
        OpenLevel(currentLevel);
    }


    void OpenLevel(int level)
    {
        LevelsGameObject.transform.GetChild(level).gameObject.SetActive(true);
    }
}
