using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RowTube
{
    [SerializeField] private List<String> rows = new List<String> ();
    public int lengthRow()
    {
        return rows.Count;
    }

    public int[] getTubeRow(int index)
    {
        return Array.ConvertAll(rows[index].Split(','),int.Parse);
    }
}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{

    [SerializeField] private List<RowTube> level = new List<RowTube>();


    private List<GameObject> spaceSpawnTubes = new List<GameObject>();

      
     public void CreateLevel(ref List<Tube> tubes)
     {
        GameObject spaceSpawnTube = null;
        for (int i = 0; i < level.Count; i++)         
        {
            spaceSpawnTube = Instantiate(GameManager.instance.prefabSpaceSpawnTube, GameManager.instance.listTubeGameObject.transform);

            for (int j = 0; j < level[i].lengthRow(); j++)
            {

                GameObject tubeGameObject = Instantiate(GameManager.instance.prefabTube, spaceSpawnTube.transform);
                Tube tube = tubeGameObject.GetComponent<Tube>();

                tube.SetSizeTube(ref tubeGameObject, level[i].getTubeRow(j).Length);
                tubes.Add(tube);
                CreateBalls( i, j,ref tube);
            }

            spaceSpawnTubes.Add(spaceSpawnTube);
        }
     }
     private void CreateBalls( int i, int j,ref Tube tube)
    {
        int[] ballsId = level[i].getTubeRow(j);
        for (int k = 0; k < ballsId.Length; k++)
        {
            GameObject posBall = Instantiate(GameManager.instance.prefabPosBall, tube.prefabListPosBall.transform);
         
            tube.listPosBall.Add(posBall);
            if (ballsId[k] != -1)
            {
                GameObject ball = Instantiate(GameManager.instance.prefabBall, tube.prefabListBall.transform);
                ball.GetComponent<Image>().sprite = GameManager.instance.listSpriteBalls[ballsId[k]];
                tube.listBall.Add(ball);
            }  
        }
        tube.topPosition = tube.listPosBall[tube.listPosBall.Count - 1].transform;

    }
     public void DestroySpaceSpawnTube()
     {
        for(int i = 0; i < spaceSpawnTubes.Count; i++)
        {
            Destroy(spaceSpawnTubes[i]);
        }
        spaceSpawnTubes.Clear();
     }

}
