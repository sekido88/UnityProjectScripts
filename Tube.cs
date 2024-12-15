using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tube : MonoBehaviour,IPointerClickHandler
{
    public int heightOffSetTube = 80;
    public int withOffSetTube = 30;

    private bool _isCompletedTube = false;
    public bool IsCompletedTube
    {
        get
        {
            return _isCompletedTube;
        }
        set
        {
            _isCompletedTube = value;
        }
    }

    public GameObject prefabListBall;
    public GameObject prefabListPosBall;
   
    public List<GameObject> listPosBall;
    public List<GameObject> listBall;
    [HideInInspector] public Transform topPosition;
    public int indexCurrentBall;

    public float moveToTopAnotherTubeDuration = 0.2f;
    public float moveToDownDuration = 0.3f;

    public void SetSizeTube(ref GameObject tubeGameObject, int heightScale)
    {
        RectTransform rectPrefabBall = GameManager.instance.prefabBall.GetComponent<RectTransform>();
        RectTransform rectTransform = tubeGameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectPrefabBall.sizeDelta.x + withOffSetTube, rectPrefabBall.sizeDelta.y * heightScale + heightOffSetTube);

        RectTransform rectListBall = tubeGameObject.transform.GetChild(0).GetComponent<RectTransform>();
        rectListBall.sizeDelta = new Vector2(rectPrefabBall.sizeDelta.x, rectPrefabBall.sizeDelta.y * heightScale);
        RectTransform rectListPos = tubeGameObject.transform.GetChild(1).GetComponent<RectTransform>();
        rectListPos.sizeDelta = new Vector2(rectPrefabBall.sizeDelta.x, rectPrefabBall.sizeDelta.y * heightScale);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Tube currentTubeOnMouseDown = GameManager.instance.currentTubeOnMouseDown;
        CheckTubeIsCompleted();
        if (currentTubeOnMouseDown == null)
        {
            if (!IsCompletedTube)
            {
                MoveToOnTop();
                GameManager.instance.currentTubeOnMouseDown = this;
            }
        }
        if(currentTubeOnMouseDown != null) {
            
            if(currentTubeOnMouseDown == this)
            {
                if(!IsCompletedTube)
                {
                    MoveToBackTube();
                }
                GameManager.instance.currentTubeOnMouseDown = null;
            }
            else
            {
                if (IsValidToMoveTube())
                {
                    UpdateListBall();
                    MoveToDown();
                    GameManager.instance.currentTubeOnMouseDown = null;
                }

            }
        }
        CheckAllTubeIsCompleted();
   
    }
    private void UpdateListBall()
    {
        Tube currentTubeOnMouseDown = GameManager.instance.currentTubeOnMouseDown;
        if (currentTubeOnMouseDown.GetBallOnTop() != null)
        {
                listBall.Add(currentTubeOnMouseDown.GetBallOnTop());
                currentTubeOnMouseDown.listBall.RemoveAt(currentTubeOnMouseDown.GetIndexBallOnTop());
        }
   
    }
    private bool IsValidToMoveTube()
    {
     
        return (!IsCompletedTube && listBall.Count < listPosBall.Count);
    }
    private void CheckTubeIsCompleted()
    {
        if (IsCompletedTube) { 
            return; 
        }
        if (listBall.Count < listPosBall.Count)
        {
            IsCompletedTube = false;
            return;
        }
        for(int i = 0;i < listBall.Count - 1;i++)
        {
            Image image1 = listBall[i].GetComponent<Image>();
            Image image2 = listBall[i + 1].GetComponent<Image>();
            if(image1.sprite != image2.sprite)
            {
                IsCompletedTube = false;
                return;
            }

        }
        IsCompletedTube = true;

            
    }
    private int GetIndexBallOnTop()
    {
        for (int i = listBall.Count - 1; i >= 0; i--)
        {
            if (listBall[i] != null)
            {
                return i;
            }
        }
        return -1;
    }
    private GameObject GetBallOnTop()
    {
        for(int i = listBall.Count - 1; i >= 0; i--)
        {
            if (listBall[i] != null)
            {
                return listBall[i];
            }
        }
        return null;
    }
    private void MoveToOnTop()
    {
        topPosition = listPosBall[listPosBall.Count - 1].transform;
        GameObject ball = GetBallOnTop();
        if(ball != null)
        {
            ball.transform.DOMove(topPosition.position + Vector3.up, moveToDownDuration);
        }
    }
    private void MoveToBackTube()
    {
        int indexBall = GetIndexBallOnTop();
        GameObject ball = null;
        if (indexBall != -1)
        {
            ball = listBall[indexBall];
        }
        if (ball != null)
        {
            ball.transform.DOMove(listPosBall[indexBall].transform.position, moveToDownDuration);
        }
    }
    private void MoveToDown()
    {
        int indexBall = GetIndexBallOnTop();
        listBall[indexBall].transform.DOMove(topPosition.position + Vector3.up, moveToTopAnotherTubeDuration).OnComplete( () =>
        {
            listBall[indexBall].transform.DOMove(listPosBall[indexBall].transform.position, moveToDownDuration);
        });
     
    }
    private void CheckAllTubeIsCompleted()
    {
        int countTubeNull = 0;
        int countTubeIsCompleted = 0;
        
        foreach (Tube tube in GameManager.instance.listTube) {

            tube.CheckTubeIsCompleted();
            if(tube.listBall.Count == 0)
            {
                countTubeNull++;
            }
            if(tube.IsCompletedTube)
            {
                countTubeIsCompleted++;
            }
        }

  
        if(countTubeNull + countTubeIsCompleted != GameManager.instance.listTube.Count) {
            GameManager.instance.AllTubeIsCompleted = false;
            return;
        }

        GameManager.instance.AllTubeIsCompleted = true;
        GameManager.instance.SetLevelCompleted();
    }
}
