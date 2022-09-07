using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CoopTestMain : MonoBehaviour
{
    private Animal animal;
    private MapManager mapManager;
    public Button DayButton;
    public Button SceneChangeButton;


    void Start()
    {
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.animal = GameObject.FindObjectOfType<Animal>();
        this.animal.onDecideTargetTile = (startPos, targetPos) =>
        {
            Debug.Log(startPos);
            Debug.Log(targetPos);
            this.mapManager.PathFinding(startPos, targetPos);            
            this.animal.MovePlayer(this.mapManager.PathList);
        };

        mapManager.Init();
        animal.Init();

        this.SceneChangeButton.onClick.AddListener(()=> {
            SceneManager.LoadScene(5);
        });

        this.DayButton.onClick.AddListener(() =>
        {
            this.animal.age++;

            if (this.animal.age >6)
                this.animal.Produce();

            if (this.animal.age == 6)
            {
                this.animal.GrowUp();
            }            
        });
    }
}
