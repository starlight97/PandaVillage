using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FarmTestMain : MonoBehaviour
{    
    private MapManager mapManager;
    private SangIkTimeManager timeManager;

    public Coop coop;
    public Silo silo;
    public Button DayButton;
    public Button SceneChangeButton;
    public Button OpenDoorButton;
    public Button AddAnimalButton;
    public Button AddHayButton;



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
            coop.FindAnimals();
            AnimalsInit();            
        }
    }

    
    void Start()
    {       

        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.timeManager = GameObject.FindObjectOfType<SangIkTimeManager>();
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
                AnimalManager.instance.coopOpened = true;
            }


            coop.FindAnimals();
            AnimalsInit();
        });

        //건초추가버튼
        this.AddHayButton.onClick.AddListener(() =>
        {
            this.silo.hay++;
        });

        this.timeManager.timeToGoHome = () => {
            this.coop.AnimalsGoHome();
        };
        //다음날 버튼 - 성장과 생산
        this.DayButton.onClick.AddListener(() =>
        {

            this.timeManager.hour = 0;
            this.timeManager.minute = 0;

            foreach (var animal in coop.animalList)
            {
                //동물들이 배부른경우 성장과 생산을 할수 있음
                if (animal.isFull)
                {
                    animal.yummyDay++;

                    if (animal.yummyDay > 6)
                        animal.Produce();

                    if (animal.yummyDay == 6)
                    {
                        animal.GrowUp();
                    }
                }


                // 사일로의 건초가 있으면 먹이주기
                if (silo.hay > 0)
                {
                    silo.hay--;
                    animal.isFull = true;
                }
                else
                {
                    animal.isFull = false;
                }
            }
        });







        Test();
        coop.Init();
        silo.Init();
        timeManager.Init();

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

        foreach (var animal in coop.animalList)
        {   
            animal.Init();

            //동물들 움직이기
            this.coop.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
            {
                this.mapManager.PathFinding(startPos, targetPos, pathList);
                animal.Move();
            };           

            //동물들이 집으로 가게하기
            animal.goHome = (startPos, pathList) =>
            {
                Vector2Int targetPos = new Vector2Int((int)DoorPos.x, (int)DoorPos.y - 1);
                this.mapManager.PathFinding(startPos, targetPos, pathList);
                animal.Move();

            };
           
        }
    }
}
