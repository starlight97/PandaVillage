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


    void Start()
    {        
        coop.Init();

        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.animal = GameObject.FindObjectsOfType<Animal>();


        mapManager.Init();

        AnimalsInit();
        

        this.SceneChangeButton.onClick.AddListener(() => {

            StartCoroutine(LoadYourAsyncScene());

            SceneManager.LoadSceneAsync(4);

        });

        this.OpenDoorButton.onClick.AddListener(() => {
            coop.SetDoor();
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
            ani.onDecideTargetTile = (startPos, targetPos) =>
            {
                Debug.Log(startPos);
                Debug.Log(targetPos);
                this.mapManager.PathFinding(startPos, targetPos);
                ani.MovePlayer(this.mapManager.PathList);
            };

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
