using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using System.Linq;

public class TilemapTest : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public List<TileObject> tileObjects;
    public List<TileData> tileDatas;
    private Dictionary<TileBase, TileObject> dicTileObejctData = new Dictionary<TileBase, TileObject>();

    //private Dictionary<int, TileData> dicTileData = new Dictionary<int, TileData>();

    private void Awake()
    {
        //DataManager.instance.LoadData<TileData>("./tile_data.json");
    }

    public void Init()
    {
        foreach (var tileObject in this.tileObjects)
        {
            foreach (var tile in tileObject.tiles)
            {
                this.dicTileObejctData.Add(tile, tileObject);
            }
        }

        Debug.Log(this.tileDatas);   

        foreach (var tileData in this.tileDatas)
        {

        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetTileObject();
        }
    }

    //private void LoadDatas()
    //{
    //    var asset = Resources.Load<TextAsset>("Datas/tile_data");
    //    this.dicTileData = JsonConvert.DeserializeObject<TileData[]>(asset.text).ToDictionary(x => x.id) ;
    //}

    public TileObject GetTileObject()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int gridPosition = this.tilemap.WorldToCell(mousePosition);

        TileBase clikedTile = this.tilemap.GetTile(gridPosition);

        var tileObject = this.dicTileObejctData[clikedTile];
        Debug.Log(tileObject);

        return tileObject;
    }
}
