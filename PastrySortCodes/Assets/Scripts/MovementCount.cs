using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementCount : MonoBehaviour
{
    [SerializeField] int levelMoveCount;
    int moveCount = 0;
    ManageStar manageStar;
    [SerializeField] TextMeshProUGUI movementCountText;
    bool isDecreased;
    void Start()
    {
        manageStar = FindObjectOfType<ManageStar>();
        movementCountText.text = moveCount.ToString() + "/" + levelMoveCount.ToString();

    }


    public void LevelMovementCountManage()
    {
        moveCount++;
        movementCountText.text = moveCount.ToString() + "/" + levelMoveCount.ToString();

        if (moveCount > levelMoveCount & !isDecreased)
        {
            manageStar.DecreaseStar();
            isDecreased = true;
        }
    }
}
