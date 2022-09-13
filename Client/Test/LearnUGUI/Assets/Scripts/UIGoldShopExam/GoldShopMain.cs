using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UIGoldShopExample
{
    public class GoldShopMain : MonoBehaviour
    {
        public Dictionary<int, GoldshopData> dicGoldShopDatas;
        public UIGoldShop uiGoldShop;
        public static GoldShopMain instance;

        private void Awake()
        {
            GoldShopMain.instance = this;
        }

        void Start()
        {
            this.LoadData();

            this.uiGoldShop.Init();
        }

        private void LoadData()
        {
            var asset = Resources.Load<TextAsset>("Datas/goldshop_data");
            this.dicGoldShopDatas = JsonConvert.DeserializeObject<GoldshopData[]>(asset.text).ToDictionary(x => x.id);
        }

        [MenuItem("slg/mission_info/delete")]
        public static void DeleteGameInfo()
        {
            var path = string.Format("{0}/mission_info.json", Application.persistentDataPath);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("mission_info.json deleted");
            }
            else
            {
                Debug.Log("mission_info.json not found.");
            }

            // https://answers.unity.com/questions/43422/how-to-implement-show-in-explorer.html
            Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
        }

        [MenuItem("slg/mission_info/show in explorer")]
        public static void ShowInExplorer()
        {
            Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
        }
    }
}