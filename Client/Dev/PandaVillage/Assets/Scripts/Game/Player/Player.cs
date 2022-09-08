using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private MoveMent2D moveMent2D;
    private bool isUseTool;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>> onDecideTargetTile;

    public UnityAction<Vector3Int> onRequestDirtTile;
    public UnityAction<Vector3Int> onChangeDirtTile;

    public UnityAction<Vector3Int> onRequestHoeDirtTile;
    public UnityAction<Vector3Int> onChangeHoeDirtTile;

    public UnityAction<Vector3Int> onRequestObjectTile;
    public UnityAction<Vector3Int> onChangeObjectTile;

    private Vector3Int dir;
    private Vector3Int pos;

    private bool isUseHoe;
    private bool isUseSeed;
    private bool isUseWateringCan;

    private void Start()
    {
        this.moveMent2D = GetComponent<MoveMent2D>();
    }
    private void Update()
    {
        // 마우스 클릭시 좌표를 인게임 좌표로 변환하여 mousePos 변수에 저장
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z값은 사용을 안하므로 x, y 값만 저장후 Move
        int targetPosX = (int)Math.Truncate(mousePos.x);
        int targetPosY = (int)Math.Truncate(mousePos.y);

        int currentPosX = (int)Math.Truncate(this.transform.position.x);
        int currentPosY = (int)Math.Round(this.transform.position.y);
        Vector2Int curPos = new Vector2Int(currentPosX, currentPosY);
        Vector2Int targetPos = new Vector2Int(targetPosX, targetPosY);

        // 마우스 왼쪽버튼 클릭시
        if (Input.GetMouseButtonDown(1))
        {
            this.moveMent2D.pathList.Clear();
            this.onDecideTargetTile(curPos, targetPos, this.moveMent2D.pathList);

            #region 나중에 써야할 코드
            //if (!this.isUseTool)
            //{
            //    this.onDecideTargetTile(curPos, targetPos);
            //}
            //else
            //{
            //    this.pos = new Vector3Int(currentPosX, currentPosY, 0);
            //    Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);

            //    this.dir = tpos - this.pos;

            //    if (Mathf.Abs(dir.x) <= 1 && Mathf.Abs(dir.y) <= 1)
            //    {
            //        this.onRequestDirtTile(this.dir + this.pos);
            //    }
            //}
            #endregion
        }

        // 오른쪽 마우스 클릭 시 타일 변경됨
        if (Input.GetMouseButtonDown(0))
        {
            this.pos = new Vector3Int(currentPosX, currentPosY, 0);
            Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);

            this.dir = tpos - this.pos;

            var tilePos = this.dir + this.pos;

            if (Mathf.Abs(dir.x) <= 1 && Mathf.Abs(dir.y) <= 1)
            {
                // 밭
                if (this.isUseHoe)
                {
                    this.onRequestDirtTile(tilePos);
                }

                // 씨앗
                else if (this.isUseSeed)
                {
                    this.onRequestHoeDirtTile(tilePos);
                }

                // 물뿌리기
                else if (this.isUseWateringCan)
                {
                    this.onRequestObjectTile(tilePos);
                }
            }
        }
    }

    public void Move()
    {
        this.moveMent2D.Move();
    }

    public void UseItem()
    {
        // 도구를 들고 있을 때 플레이어가 움직이지 않도록 위치 고정
        var PosX = this.transform.position.x;
        var PosY = this.transform.position.y;
        this.transform.position = new Vector3(PosX, PosY, 0);
    }

    public int SeleteItem(int selete)
    {
        if (selete == 0)
        {
            this.isUseHoe = true;
            this.isUseSeed = false;
            this.isUseWateringCan = false;
        }

        else if(selete == 1)
        {
            this.isUseHoe = false;
            this.isUseSeed = true;
            this.isUseWateringCan = false;
        }

        else if (selete == 2)
        {
            this.isUseHoe = false;
            this.isUseSeed = false;
            this.isUseWateringCan = true;
        }

        return selete;
    }

    public void RequestDirtTile()
    {
        this.onChangeDirtTile(this.dir + this.pos);
    }

    // 씨앗 타일 요청
    public void RequestHoeDirtTile()
    {
        this.onChangeHoeDirtTile(this.dir + this.pos);
    }

    // 물 타일 요청
    public void RequestObjectTile()
    {
        this.onChangeObjectTile(this.dir + this.pos);
    }
}

// 작성자 : 박정식
// 마지막 수정 : 2022-08-14 
// 플레이어 관련 스크립트입니다.
