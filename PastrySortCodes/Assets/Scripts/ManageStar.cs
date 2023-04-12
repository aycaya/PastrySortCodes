using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStar : MonoBehaviour
{
    public int levelStars = 3;

    public void DecreaseStar()
    {
        int level = PlayerPrefs.GetInt("ActiveLevel", 3);

        int star = PlayerPrefs.GetInt("LevelStars" + level, 3);

        star--;
        PlayerPrefs.SetInt("LevelStars" + level, star);
    }

}
