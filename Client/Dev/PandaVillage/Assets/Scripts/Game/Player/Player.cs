using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    private PlayerMove playerMove;
    public UnityAction<Vector2Int, Vector2Int> onDecideTargetTile;

    private void Start()
    {
        this.playerMove = GetComponent<PlayerMove>();
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
            this.onDecideTargetTile(curPos, targetPos);
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
