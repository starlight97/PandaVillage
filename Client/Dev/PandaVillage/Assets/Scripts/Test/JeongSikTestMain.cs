using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class JeongSikTestMain : MonoBehaviour
{


    private MapManager mapManager;
    private Player player;

    public Button testBtn;


    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.player.onDecideTargetTile = (startPos, targetPos) =>
        {
            this.mapManager.PathFinding(startPos, targetPos);
            this.player.Move(this.mapManager.PathList);
        };
        //tilemap.SetTile()
        Vector3Int pos = new Vector3Int(2, 2, 0);

        mapManager.Init();

        this.testBtn.onClick.AddListener(() => 
        {            
            mapManager.ObjectMapSetTile(pos);
        });

    }

}
