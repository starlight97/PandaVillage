using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongSikTestMain : MonoBehaviour
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

    }

}
