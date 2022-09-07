using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private GameObject modelGo;
    private PlayerMove playerMove;
    public bool isUseTool = true;
    public bool isUseSeed = true;
    public UnityAction<Vector2Int, Vector2Int> onDecideTargetTile;
    public UnityAction<Vector3Int> onClickDirtTile;
    
    private void Start()
    {
        this.playerMove = GetComponent<PlayerMove>();
        this.modelGo = this.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        // 마우스 왼쪽버튼 클릭시
        if (Input.GetMouseButtonDown(0))
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

            if (!this.isUseTool)
            {
                this.onDecideTargetTile(curPos, targetPos);
            }
            else
            {
                Vector2 dir = targetPos - curPos;
                Vector2 startRayPos = new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.45f);

                Debug.DrawRay(startRayPos, dir * 2f, Color.red, 3f);
                RaycastHit2D hit = Physics2D.Raycast(startRayPos, dir, 2f, LayerMask.GetMask("Object"));

                GameObject scanObject = null;

                if (hit.collider != null && hit.collider.CompareTag("Dirt"))
                {
                    scanObject = hit.collider.gameObject;
                    Debug.Log("scanObject" + scanObject);
                    // 도구를 들고 있을 때 플레이어가 움직이지 않도록 위치 고정
                    var PosX = this.transform.position.x;
                    var PosY = this.transform.position.y;
                    this.transform.position = new Vector3(PosX, PosY, 0);

                    // 맵 매니저에게 타일을 바꿔달라는 액션
                    Vector3Int dirtTilePos = new Vector3Int((int)hit.point.x, (int)hit.point.y, 0);
                    //Debug.Log(dirtTilePos);
                    this.onClickDirtTile(dirtTilePos);
                }

            }
        }
    }

    public void Move(List<Vector3> pathList)
    {
        this.playerMove.Move(pathList);
    }
}

// 작성자 : 박정식
// 마지막 수정 : 2022-08-14 
// 플레이어 관련 스크립트입니다.
