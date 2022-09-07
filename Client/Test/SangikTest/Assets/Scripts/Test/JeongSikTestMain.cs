using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongSikTestMain : MonoBehaviour
{
    private Player player;
    private MapManager mapManager;

    void Start()
    {
        this.mapManager = GameObject.FindObjectOfType<MapManager>();
        this.player = GameObject.FindObjectOfType<Player>();
        this.player.onDecideTargetTile = (startPos, targetPos) =>
        {
            this.mapManager.PathFinding(startPos, targetPos);
            this.player.Move(this.mapManager.PathList);
        };

        mapManager.Init();

    }

}
