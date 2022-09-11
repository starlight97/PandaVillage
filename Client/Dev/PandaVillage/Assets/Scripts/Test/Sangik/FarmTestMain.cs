using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FarmTestMain : MonoBehaviour
{
    private Animal[] animal;
    private MapManager mapManager;
    public Coop coop;
    public Silo silo;
    public Button DayButton;
    public Button SceneChangeButton;
    public Button OpenDoorButton;
    public Button AddAnimalButton;   

    private void Test()
    {
        if (AnimalManager.instance.coopOpened) 
        {
            foreach (var data in AnimalManager.instance.AnimalDic.Values)
            {
                Vector3 DoorPos = coop.transform.GetChild(1).position;
                var go = Instantiate(data);
                var scr = go.GetComponent<Animal>();
                go.transform.position = new Vector3(Random.Range(scr.mapBottomLeft.x, scr.mapTopRight.x + 1), Random.Range(scr.mapBottomLeft.y, scr.mapTopRight.y + 1));
            }
            this.animal = GameObject.FindObjectsOfType<Animal>();
            AnimalsInit();            
        }
    }



    void Start()
    {
        Test();
        coop.Init();
        silo.Init();

        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.coop = GameObject.FindObjectOfType<Coop>();


        //coop의 문의 포지션을 가져옴
        Vector3 DoorPos = coop.transform.GetChild(1).position;


        //동물추가버튼
        this.AddAnimalButton.onClick.AddListener(() => {
            AnimalManager.instance.AddAnimal();
        });
        
        //씬변환버튼
        this.SceneChangeButton.onClick.AddListener(() => {

            StartCoroutine(LoadYourAsyncScene());

            SceneManager.LoadSceneAsync(4);

        });


        //문열기버튼
        this.OpenDoorButton.onClick.AddListener(() =>
        {           
            
            if (coop.SetDoor()&& !AnimalManager.instance.coopOpened) //문이 열렸을경우에만 모든 동물들이 나온다.
            {
                foreach (var data in AnimalManager.instance.AnimalDic.Values)
                {
                    var go = Instantiate(data);
                    go.transform.position = DoorPos;                   
                }
                this.animal = GameObject.FindObjectsOfType<Animal>();
                AnimalsInit();
                AnimalManager.instance.coopOpened = true;
            }
            
        });




    }
    private IEnumerator LoadYourAsyncScene()
    {

        SceneManager.LoadSceneAsync(4);
        yield return null;
    }



    public void AnimalsInit()
    {
        //coop의 문의 포지션을 가져옴
        var DoorPos = coop.transform.GetChild(1).position;

        //animal들 초기화
        foreach (var ani in animal)
        {   
            ani.Init();

            //동물들이 무작위로 움직이게하기
            //ani.onDecideTargetTile = (startPos, targetPos, pathList) =>
            //{
            //    this.mapManager.PathFinding(startPos, targetPos, pathList);
            //    ani.Move();
            //};

            //동물들이 집으로 가게하기
            ani.onGoHome = (startPos, pathList) =>
            {
                Vector2Int targetPos = new Vector2Int((int)DoorPos.x, (int)DoorPos.y - 1);
                this.mapManager.PathFinding(startPos, targetPos, pathList);
                ani.Move();

            };
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
