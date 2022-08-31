using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;     // Ÿ�ϸ� ���
using System;

public class TestTile : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField]
    private TileBase[] tileBases;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.ChangeTile();
        }
    }

    public void ChangeTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.tilemap = GetComponent<Tilemap>();
        // ���� �������� �� ���������� ����
        Vector3Int location = this.tilemap.WorldToCell(mousePos);
        // �ش� �� �����ǿ� Ÿ�� �׸���
        this.tilemap.SetTile(location, this.tileBases[0]);
    }

    // Ÿ�Ͽ� Ŀ�� �ø��� �� ����Ǵ� �޼���(�׽�Ʈ��)
    // ��� X
    private void onMouseOver()
    {
        try
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

            if (this.tilemap = hit.transform.GetComponent<Tilemap>())
            {
                // Ÿ�ϸ��� ��� Ÿ�� ���� ��ħ: ��� Ÿ�Ͽ� ���� ������ �˻� �� ��� ������Ʈ
                this.tilemap.RefreshAllTiles();

                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);

                // Ÿ�� ��ǥ �޾ƿͼ� �� �ٲٱ�
                this.tilemap.SetTileFlags(v3Int, TileFlags.None);
                this.tilemap.SetColor(v3Int, (Color.red));
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.LogFormat("click point :{0}", hit.point);
            }
        }
        catch (NullReferenceException)
        {

        } 
    }
}

// �ۼ���: ������
// ���� ����: 2022-08-14
// ȣ�̸� ������� �� ����� Ÿ�� �׸��� ��ũ��Ʈ