using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewShelf : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    [SerializeField] GameObject firstShelfToMove;
    [SerializeField] GameObject secondShelfToMove;
    [SerializeField] GameObject shelfToOpen;
    ManageStar manageStar;
    void Start()
    {
        manageStar = FindObjectOfType<ManageStar>();
    }

    public void OpenShelfButton()
    {
        OpenShelf();
        MoveOtherShelfs();
        manageStar.DecreaseStar();
    }

    void OpenShelf()
    {
        shelfToOpen.SetActive(true);
        shelfToOpen.tag = "Shelf";
    }
    void MoveOtherShelfs()
    {
        firstShelfToMove.transform.position = pos1.position;
        secondShelfToMove.transform.position = pos2.position;
    }
}
