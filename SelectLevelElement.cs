using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectLevelElement : MonoBehaviour,IPointerClickHandler
{
    private GameObject selectLevelElement;
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.instance.isMouseDownSelectLevelElement = true;
        GameManager.instance.currentLevel = int.Parse(selectLevelElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1;
    }
    public void SetGameObjectSelectLevelElement(ref GameObject selectLevelElementRef)
    {
        selectLevelElement = selectLevelElementRef;
    }

}
