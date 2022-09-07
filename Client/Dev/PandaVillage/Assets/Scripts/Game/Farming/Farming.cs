using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Farming : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField]
    private TileBase tileBases;
    public UnityAction onClick;

    public void Init()
    {
        this.CreateTile();
    }

    public void CreateTile()
    {
        //StartCoroutine(this.CreateTileRoutine());
    }

    private IEnumerator CreateTileRoutine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 월드 포지션을 셀 포지션으로 변경
            this.tilemap = GetComponent<Tilemap>();
            Vector3Int location = this.tilemap.WorldToCell(mousePos);
            // 해당 셀 포지션에 타일 그리기
            this.tilemap.SetTile(location, this.tileBases);
            this.onClick();
        }
        yield return null;
    }
}
