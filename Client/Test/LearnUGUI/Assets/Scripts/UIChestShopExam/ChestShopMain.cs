using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UIChestShopExample
{
    public class ChestShopMain : MonoBehaviour
    {
        public Dictionary<int, ChestData> dicChestDatas;
        public UIShop uiShop;
        public static ChestShopMain instance;

        private void Awake()
        {
            ChestShopMain.instance = this;
        }

        void Start()
        {
            this.LoadData();

            uiShop.Init();
        }

        private void LoadData()
        {
            var asset = Resources.Load<TextAsset>("Datas/chest_data");
            this.dicChestDatas = JsonConvert.DeserializeObject<ChestData[]>(asset.text).ToDictionary(x => x.id);
        }
    }
}
