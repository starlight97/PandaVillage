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
        this.player.onDecideTargetTile = (startPos, targetPos, pathList) =>
        {
            this.mapManager.PathFinding(startPos, targetPos, pathList);
            this.player.Move();
        };
        //tilemap.SetTile()
        Vector3Int pos = new Vector3Int(2, 2, 0);


        this.testBtn.onClick.AddListener(() => 
        {            
            //mapManager.DirtMapSetTile(pos);
        });

    }

}
