using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CoopTestMain : MonoBehaviour
{
    private Animal[] animal;
    private MapManager mapManager;
    public Button DayButton;
    public Button SceneChangeButton;


    void Start()
    {
     

        this.mapManager = GameObject.FindObjectOfType<MapManager>();        

        //mapManager.Init();

        foreach (var data in AnimalManager.instance.AnimalDic.Values)
        {
            var go = Instantiate(data);
            var scr = go.GetComponent<Animal>();
            go.transform.position = new Vector3(Random.Range(scr.mapBottomLeft.x, scr.mapTopRight.x+1), Random.Range(scr.mapBottomLeft.y, scr.mapTopRight.y +1));
        }

        this.animal = GameObject.FindObjectsOfType<Animal>();
        AnimalsInit();



        this.SceneChangeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(5);
        });
       
    }


    public void AnimalsInit()
    {
        //animal들 초기화
        foreach (var ani in animal)
        {
            //Move
            //ani.onDecideTargetTile = (startPos, targetPos) =>
            //{
            //    Debug.Log(startPos);
            //    Debug.Log(targetPos);
            //    //this.mapManager.PathFinding(startPos, targetPos);
            //    //ani.MovePlayer(this.mapManager.PathList);
            //};

            //초기화
            ani.Init();


            //성장과 생산
            this.DayButton.onClick.AddListener(() =>
            {
                ani.age++;

                if (ani.age > 6)
                    ani.Produce();

                if (ani.age == 6)
                {
                    ani.GrowUp();
                }
            });

        }
    }
}
