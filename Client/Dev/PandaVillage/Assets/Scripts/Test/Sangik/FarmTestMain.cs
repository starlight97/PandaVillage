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
    public Button DayButton;
    public Button SceneChangeButton;
    public Button OpenDoorButton;
    public Button AddAnimalButton;   

    void Start()
    {        
        coop.Init();

        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.coop = GameObject.FindObjectOfType<Coop>();

        //mapManager.Init();

        this.AddAnimalButton.onClick.AddListener(() => {
            AnimalManager.instance.AddAnimal();
        });
        

        this.SceneChangeButton.onClick.AddListener(() => {

            StartCoroutine(LoadYourAsyncScene());

            SceneManager.LoadSceneAsync(4);

        });

        this.OpenDoorButton.onClick.AddListener(() =>
        {           
            if (coop.SetDoor()) //문이 열렸을경우에만 모든 동물들이 나온다.
            {
                foreach (var data in AnimalManager.instance.AnimalDic.Values)
                {
                    var go = Instantiate(data);
                    go.transform.position = coop.transform.GetChild(1).position;

                    go.onDecideTargetTile = (startPos, targetPos, pathList) =>
                    {
                        this.mapManager.PathFinding(startPos, targetPos, pathList);
                        go.Move();
                    };
                }
                this.animal = GameObject.FindObjectsOfType<Animal>();
                AnimalsInit();
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
