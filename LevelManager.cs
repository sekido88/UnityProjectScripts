using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabSelectLevel;

    public int CountLevels;
    public bool isMouseDownSelectLevelElement = false;

    public GameObject selectLevel;

    public static LevelManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        selectLevel = GameObject.Find("SelectLevel");
    }
    void Start()
    {
        CreateSelectLevelElements();
    }
    public void CreateSelectLevelElements()
    {
        CountLevels = GameManager.instance.listLevelDatas.Count;
        Debug.Log(CountLevels);
        if (selectLevel != null)
        {
            for (int i = 0; i < CountLevels; i++)
            {
                GameObject SelectLevelTmp = Instantiate(prefabSelectLevel, selectLevel.transform.GetChild(0).transform);
                SelectLevelTmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                SelectLevelTmp.GetComponent<SelectLevelElement>().SetGameObjectSelectLevelElement(ref SelectLevelTmp);
            }
        }
    }
}
