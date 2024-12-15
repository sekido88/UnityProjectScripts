using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Tube> listTube;

    public List<Sprite> listSpriteBalls;
    public List<LevelData> listLevelDatas;

    public GameObject listTubeGameObject;
    public GameObject levelCompleted;

    public GameObject prefabTube;
    public GameObject prefabBall;
    public GameObject prefabPosBall;
    public GameObject prefabSpaceSpawnTube;

    public Tube currentTubeOnMouseDown = null;
    public static GameManager instance;
    public int currentLevel = 0;

    private bool _allTubeIsComleted;
    public bool AllTubeIsCompleted
    {
        set
        {
            _allTubeIsComleted = value;
        }
        get
        {
            return _allTubeIsComleted;
        }
    }    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (LevelManager.instance.isMouseDownSelectLevelElement)
        {
            listLevelDatas[currentLevel].CreateLevel(ref listTube);
            LevelManager.instance.isMouseDownSelectLevelElement = false;
            LevelManager.instance.selectLevel.SetActive(false);
        }
        levelCompleted.SetActive(false);
        AudioManager.instance.PlayBackgroundMusic(AudioManager.instance.backgroundMusic);
    }
    void Update()
    {
        if (LevelManager.instance.isMouseDownSelectLevelElement)
        {
            listLevelDatas[currentLevel].CreateLevel(ref listTube);
            LevelManager.instance.isMouseDownSelectLevelElement = false;
            LevelManager.instance.selectLevel.SetActive(false);
        }
    }

    public void NextLevel()
    {
        listLevelDatas[currentLevel].DestroySpaceSpawnTube();
        listTube.Clear();

        currentLevel++;
        listLevelDatas[currentLevel].CreateLevel(ref listTube);
        AllTubeIsCompleted = false;
        levelCompleted.SetActive(AllTubeIsCompleted);
      
    }
    public void SetLevelCompleted()
    {
        levelCompleted.SetActive(AllTubeIsCompleted);
    }
}
