using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FarmTestMain : MonoBehaviour
{    
    private MapManager mapManager;
    private SangIkTimeManager timeManager;
    private RanchManager ranchManager;


    //ui부분
    public Button DayButton;
    public Button SceneChangeButton;
    public Button OpenDoorButton;
    public Button AddAnimalButton;
    public Button AddHayButton;
    public GameObject AnimalUI;




    private Vector2 MouseTest()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2((int)mousePos.x, (int)mousePos.y);
    }
    private void Update()
    {
        //클릭했으면
        if (Input.GetMouseButtonDown(0))
        {
            //쓰다듬기
            ranchManager.ShowSiloUI(MouseTest());
            
            foreach (var coop in ranchManager.coopArr)
            {
                coop.AnimalPatted(MouseTest());
            }

            //동물 ui보기
            ranchManager.ShowAnimalUI();
        }
    }




    void Start()
    {       

        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.timeManager = GameObject.FindObjectOfType<SangIkTimeManager>();
        this.ranchManager = GameObject.FindObjectOfType<RanchManager>();

        var animalButton = AnimalUI.GetComponentInChildren<Button>();



        this.ranchManager.onDecideTargetTile = (startPos, targetPos, pathList, animal) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            animal.Move();
        };

        animalButton.onClick.AddListener(() => {
            AnimalUI.SetActive(false);
        });


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
            ranchManager.DoorOpen();
            ranchManager.AnimalsInit();
        });

        //건초추가버튼
        this.AddHayButton.onClick.AddListener(() =>
        {
            ranchManager.AddHay();
        });

        //6시가 되면 동물들이 집으로 돌아감
        this.timeManager.timeToGoHome = () => {
            ranchManager.AnimalsGoHome();
        };
        //다음날 버튼 - 성장과 생산
        this.DayButton.onClick.AddListener(() =>
        {
            NextDay();
        });

        Test();
        timeManager.Init();
        ranchManager.Init();

    }
    private IEnumerator LoadYourAsyncScene()
    {

        SceneManager.LoadSceneAsync(4);
        yield return null;
    }



   


    //다음날이 될때
    public void NextDay()
    {
        this.timeManager.hour = 0;
        this.timeManager.minute = 0;
        ranchManager.NextDay();
    }


    private void Test()
    {
        if (AnimalManager.instance.coopOpened)
        {
            foreach (var data in AnimalManager.instance.AnimalDic.Values)
            {
                Vector3 DoorPos = ranchManager.coopArr[0].transform.GetChild(1).position;
                var go = Instantiate(data);
                var scr = go.GetComponent<Animal>();
                go.transform.position = new Vector3(Random.Range(scr.mapBottomLeft.x, scr.mapTopRight.x + 1), Random.Range(scr.mapBottomLeft.y, scr.mapTopRight.y + 1));
            }
            ranchManager.coopArr[0].FindAnimals();
            ranchManager.AnimalsInit();
        }
    }

}
