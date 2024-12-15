using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlayGame : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public void OnMouseEnter()
    {
    }
    public void OnClickButtonPlayGame()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().sprite = UIManager.instance.spriteButtonPlayHover;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().sprite = UIManager.instance.spriteButtonPlay;
    }
}
