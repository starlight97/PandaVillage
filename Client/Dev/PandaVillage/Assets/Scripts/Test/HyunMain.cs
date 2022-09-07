using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class HyunMain : MonoBehaviour
{
    private MapManager mapManager;
    private Player player;

    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.player.onDecideTargetTile = (startPos, targetPos) =>
        {
            this.mapManager.PathFinding(startPos, targetPos);
            this.player.Move(this.mapManager.PathList);
        };
        Vector3Int pos = new Vector3Int(2, 2, 0);
        //Debug.Log(tilemap.GetTile(pos));
        //tilemap.SetTile()

        mapManager.Init();

        this.player.onRequestDirtTile = (pos) =>
        {
            if (mapManager.GetDirtTile(pos))
            {
                this.player.RequestDirtTile();
            }
        };

        this.player.onChangeDirtTile = (pos) => {
            this.mapManager.DirtMapSetTile(pos);
        };
    }
}
