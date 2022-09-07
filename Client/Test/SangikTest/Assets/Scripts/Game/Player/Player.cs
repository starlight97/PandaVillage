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
        // ���콺 ���ʹ�ư Ŭ����
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���� ��ǥ�� �ΰ��� ��ǥ�� ��ȯ�Ͽ� mousePos ������ ����
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // z���� ����� ���ϹǷ� x, y ���� ������ Move
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

// �ۼ��� : ������
// ������ ���� : 2022-08-14 
// �÷��̾� ���� ��ũ��Ʈ�Դϴ�.
