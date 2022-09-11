using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum eItemType
    {
        None = -1,
        Hoe, 
        WateringCan, 
        Seed
    }


    private Movement2D moveMent2D;
    private Farming farming;
    private eItemType isUseTool = eItemType.None;
    public UnityAction<Vector2Int, Vector2Int, List<Vector3>> onDecideTargetTile;

    public UnityAction<Vector3Int, Farming.eFarmType> onGetFarmTile;
    public UnityAction<Vector3Int, eItemType> onChangeFarmTile;

    private Vector3Int dir;
    private Vector3Int pos;
    private Rigidbody2D rbody;


    private void Start()
    {
        this.moveMent2D = GetComponent<Movement2D>();
        this.farming = GetComponent<Farming>();
        this.rbody = GetComponent<Rigidbody2D>();
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

        // 마우스 오른쪽버튼 클릭시
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

        // 왼쪽 마우스 클릭 시 타일 변경됨
        if (Input.GetMouseButtonDown(0))
        {
            this.pos = new Vector3Int(currentPosX, currentPosY, 0);
            Vector3Int tpos = new Vector3Int((int)mousePos.x, (int)mousePos.y, 0);

            this.dir = tpos - this.pos;

            var tilePos = this.dir + this.pos;

            if (Mathf.Abs(dir.x) <= 1 && Mathf.Abs(dir.y) <= 1)
            {
                //if(isUseTool != eItemType.None)
                //    GetFarmTile(tilePos, isUseTool);
                Attack(dir);
            }
        }
    }

    public void Move()
    {
        this.moveMent2D.Move();
    }


    public void SelectItem(eItemType itemType)
    {
        switch (itemType)
        {
            case eItemType.Hoe:
                this.isUseTool = itemType;
                break;
            case eItemType.WateringCan:
                this.isUseTool = itemType;
                break;
            case eItemType.Seed:
                this.isUseTool = itemType;
                break;
            default:
                this.isUseTool = eItemType.None;
                break;
        }
    }

    public void GetFarmTile(Vector3Int pos, eItemType state)
    {
        Farming.eFarmType type = farming.GetFarmTile(state);
        if (type != Farming.eFarmType.None)
            this.onGetFarmTile(pos, type);
    }

    public void ChangeFarmTile(Vector3Int pos)
    {
        this.onChangeFarmTile(pos, this.isUseTool);

    //    Farming.eFarmType type = farming.ChangeFarmTile(state);
    //    if(type != Farming.eFarmType.None)
    //        this.onChangeFarmTile(pos, state);    
    }

    public void Attack(Vector3 dir)
    {
        Vector3 startPos = this.transform.position;
        startPos.x += 0.5f;
        startPos.y += 0.5f;



        Debug.DrawRay(startPos, dir * 0.7f, new Color(0,1,0), 4f);
        //int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));  // Everything에서 Player 레이어만 제외하고 충돌 체크함
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("WallObject"));    // Player 와 MyTeammate 레이어만 충돌체크함
        //int layerMask = (1 << LayerMask.NameToLayer("Object"));    // Player 와 MyTeammate 레이어만 충돌체크함

        RaycastHit2D rayHit = Physics2D.Raycast(startPos, dir, 1, layerMask);

        if(rayHit.collider != null)
        {
            Destroy(rayHit.collider.gameObject);
        }
    }
}

// 작성자 : 박정식
// 마지막 수정 : 2022-08-14 
// 플레이어 관련 스크립트입니다.
