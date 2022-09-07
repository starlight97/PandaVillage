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

        foreach (var ani in animal)
        {
            ani.onDecideTargetTile = (startPos, targetPos) =>
            {
                Debug.Log(startPos);
                Debug.Log(targetPos);
                this.mapManager.PathFinding(startPos, targetPos);
                ani.MovePlayer(this.mapManager.PathList);                      
            };

            ani.Init();

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

        this.SceneChangeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(4);
        });

        this.OpenDoorButton.onClick.AddListener(() => {
            coop.SetDoor();
        });
    }
}
