using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PastryMovement : MonoBehaviour
{
    Rigidbody rb;
    GameObject target;
    GameObject targetColor;
    Camera m_Camera;
    GameObject object1;
    CollectableType object1Color;
    Transform temp;
    int canJumpCount;
    GameObject tempHitHolder;
    int hitholderLastChild;
    int sameColorCanJUmp;
    bool isNull = true;
    MovementCount movementCount;
    [SerializeField] int shelfCapacity;
    public int completeShelfCount = 0;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        object1Color = CollectableType.Empty;
        movementCount = FindObjectOfType<MovementCount>();
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame & isNull)
        {

            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (object1 == null)
                {

                    if (hit.transform.tag == "Shelf")
                    {

                        int whichChild;
                        whichChild = GetLastChild(hit.transform.gameObject);
                        if (whichChild == -1) { return; }
                        tempHitHolder = hit.collider.gameObject.transform.gameObject;
                        hitholderLastChild = whichChild;
                        object1 = hit.collider.gameObject.transform.GetChild(whichChild).GetChild(0).gameObject;
                        object1Color = object1.GetComponent<Collectables>().collactableType;
                        object1.transform.DOLocalMoveY(5f, 1f);

                    }
                }
                else if (object1 != null)
                {

                    if (hit.transform.tag == "Shelf" & hit.collider.gameObject != tempHitHolder)
                    {
                        isNull = false;

                        int whichChild;
                        whichChild = CheckEmptySpaces(hit.transform.gameObject);
                        if (whichChild == -1)
                        {

                            temp = hit.collider.gameObject.transform.GetChild(hit.transform.childCount - 1).GetChild(0);
                        }
                        else if (whichChild != 0)
                        {
                            target = hit.collider.gameObject.transform.GetChild(whichChild).gameObject;
                            temp = hit.collider.gameObject.transform.GetChild(whichChild - 1).GetChild(0);

                        }
                        else
                        {
                            target = hit.collider.gameObject.transform.GetChild(whichChild).gameObject;
                            temp = null;
                        }
                        if (whichChild == 0)
                        {

                            canJumpCount = hitholderLastChild + 1;

                            sameColorCanJUmp = CheckProductsCanJump(tempHitHolder, canJumpCount, hitholderLastChild);
                            if (sameColorCanJUmp > 1)
                            {
                                var param = 0;
                                if (hitholderLastChild < sameColorCanJUmp)
                                {
                                    param = 0;
                                    movementCount.LevelMovementCountManage();

                                    for (int i = hitholderLastChild, j = 0; i >= param; i--, j++)
                                    {
                                        MovementDoJumpMultiple(tempHitHolder, hit.collider.gameObject.transform.GetChild(j).gameObject, i);
                                    }
                                    IsItFinish(hit.collider.gameObject);

                                }
                                else
                                {
                                    param = 1;
                                    movementCount.LevelMovementCountManage();

                                    for (int i = hitholderLastChild, j = 0; i >= param; i--, j++)
                                    {

                                        MovementDoJumpMultiple(tempHitHolder, hit.collider.gameObject.transform.GetChild(j).gameObject, i);
                                    }
                                    IsItFinish(hit.collider.gameObject);

                                }




                            }
                            else
                            {

                                movementCount.LevelMovementCountManage();

                                MovementDoJump(target);
                                IsItFinish(hit.collider.gameObject);

                            }
                        }
                        else if ((temp.GetComponent<Collectables>().collactableType == object1Color & whichChild != -1))
                        {
                            canJumpCount = hit.transform.childCount - whichChild;
                            sameColorCanJUmp = CheckProductsCanJump(tempHitHolder, canJumpCount, hitholderLastChild);


                            if (sameColorCanJUmp > 1)
                            {
                                movementCount.LevelMovementCountManage();

                                for (int i = hitholderLastChild, j = whichChild + 1; i > hitholderLastChild - sameColorCanJUmp; i--, j--)
                                {
                                    MovementDoJumpMultiple(tempHitHolder, hit.collider.gameObject.transform.GetChild(j).gameObject, i);
                                }
                                IsItFinish(hit.collider.gameObject);


                            }
                            else if (sameColorCanJUmp == 1)
                            {

                                movementCount.LevelMovementCountManage();

                                MovementDoJump(target);
                                IsItFinish(hit.collider.gameObject);

                            }

                        }
                        else
                        {
                            object1.transform.DOLocalMoveY(0f, 1f);

                            object1 = temp.gameObject;
                            object1Color = object1.GetComponent<Collectables>().collactableType;
                            tempHitHolder = hit.collider.gameObject.transform.gameObject;
                            hitholderLastChild = GetLastChild(hit.transform.gameObject);
                            target = null;
                            targetColor = null;
                            object1.transform.DOLocalMoveY(5f, 1f);
                            isNull = true;


                        }

                    }


                }


            }
        }
    }
    int CheckProductsCanJump(GameObject parent, int howMany, int lastChild)
    {
        int howManyJump = 0;
        int param = 0;
        if (lastChild == 0)
        {
            return 1;
        }
        else
        {
            if (lastChild <= howMany)
            {
                param = 0;
            }
            else
            {
                param = lastChild - howMany;
            }
            for (int i = lastChild; i >= param; i--)
            {
                if (parent.transform.GetChild(i).GetComponentInChildren<Collectables>().collactableType != object1Color)
                {
                    return howManyJump;
                }
                else
                {
                    howManyJump++;
                }

            }
        }

        return howManyJump;


    }

    void MovementDoJumpMultiple(GameObject parent, GameObject lastTarget, int param)
    {
        var objectLast = parent.transform.GetChild(param).GetChild(0).transform;
        objectLast.parent = lastTarget.transform;

        objectLast.transform.DOJump(lastTarget.transform.position, 3, 1, 1f).OnComplete(() => MakeNull());
    }
    void MovementDoJump(GameObject parentTobe)
    {
        object1.transform.parent = parentTobe.transform;

        object1.transform.DOJump(target.transform.position, 3, 1, 1f).OnComplete(() => MakeNull());

    }
    void MakeNull()
    {
        object1 = null;
        target = null;
        temp = null;
        tempHitHolder = null;
        targetColor = null;
        object1Color = CollectableType.Empty;
        isNull = true;

    }
    int GetLastChild(GameObject shelfs)
    {
        for (int i = shelfs.transform.childCount - 1; i >= 0; i--)
        {
            if (shelfs.transform.GetChild(i).childCount > 0)
            {
                return i;
            }
        }
        return -1;
    }
    void IsItFinish(GameObject shelfs)
    {
        int howManyJump = 0;
        if (CheckEmptySpaces(shelfs) == -1)
        {
            for (int i = 0; i < shelfs.transform.childCount; i++)
            {
                if (shelfs.transform.GetChild(i).GetComponentInChildren<Collectables>().collactableType == object1Color)
                {
                    howManyJump++;
                }
            }
        }
        else
        {
            return;
        }
        if (howManyJump == shelfCapacity)
        {
            shelfs.gameObject.tag = "Untagged";
            completeShelfCount++;
        }
        else
        {
            return;
        }
    }
    int CheckEmptySpaces(GameObject shelfs)
    {
        for (int i = 0; i < shelfs.transform.childCount; i++)
        {
            if (shelfs.transform.GetChild(i).childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

}
