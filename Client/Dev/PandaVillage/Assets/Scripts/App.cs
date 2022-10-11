using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    /*
    오솔길 : Alley
    산맥 : MountainRange
    농장 : Farm
    버스정류장 : BusStop
    판다마을 : PandaVillage
    잉걸불수액숲 : CindersapForest
     */
    public enum eSceneType
    {
        App, LogoScene, LoadingScene, Title, Alley, MountainRange, Farm, BusStop, PandaVillage, CindersapForest, House
    }
    public enum eMapType
    {
        Alley, MountainRange, Farm, BusStop, PandaVillage, CindersapForest, SecretForest
    }

    public static App instance;

    private UIApp uiApp;

    private void Awake()
    {
        App.instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.uiApp = GameObject.FindObjectOfType<UIApp>();
        this.uiApp.Init();

        this.LoadScene<LogoMain>(eSceneType.LogoScene);
    }

    SceneParams param = new SceneParams();

    public void LoadScene<T>(eSceneType sceneType) where T : SceneMain
    {
        var idx = (int)sceneType;
        SceneManager.LoadSceneAsync(idx).completed += (obj) =>
        {
            var main = GameObject.FindObjectOfType<T>();

            main.onDestroy.AddListener(() =>
            {
                uiApp.FadeOut();
            });

            switch (sceneType)
            {
                case eSceneType.LogoScene:
                    {
                        this.uiApp.FadeOutImmediately();
                        var logoMain = main as LogoMain;
                        logoMain.AddListener("onShowLogoComplete", (param) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<LoadingMain>(eSceneType.LoadingScene);
                            });
                        });

                        this.uiApp.FadeIn(2f, () =>
                        {
                            logoMain.Init();
                        });
                        break;
                    }
                case eSceneType.LoadingScene:
                    {
                        this.uiApp.FadeIn(0.5f, () => 
                        {
                            main.AddListener("onLoadComplete", (data) =>
                            {
                                this.uiApp.FadeOut(0.5f, () =>
                                {
                                    this.LoadScene<TitleMain>(eSceneType.Title);
                                });
                            });
                            main.Init();
                        });

                        break;
                    }
                case eSceneType.Title:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClickNewGame", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                param.SpawnPos = new Vector3(39, 32, 0);
                                this.LoadScene<HouseMain>(eSceneType.House);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.Farm:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalAlleyPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<AlleyMain>(eSceneType.Alley);
                                param.SpawnPos = new Vector3(14, 0.7f, 0);
                            });
                        });
                        main.AddListener("onArrivalBusStopPortal", (data) =>
                        {
                            this.uiApp.FadeIn();

                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<BusStopMain>(eSceneType.BusStop);
                                param.SpawnPos = new Vector3(1, 6, 0);
                            });
                        });
                        main.AddListener("onArrivalCindersapForestPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<CindersapForestMain>(eSceneType.CindersapForest);
                                param.SpawnPos = new Vector3(68, 116.5f, 0);
                            });
                        });
                        main.AddListener("onArrivalHousePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<HouseMain>(eSceneType.House);
                                param.SpawnPos = new Vector3(33, 30.5f, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
                case eSceneType.Alley:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalFarmPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<FarmMain>(eSceneType.Farm);
                                param.SpawnPos = new Vector3(40.5f, 62.25f, 0);
                            });
                        });
                        main.AddListener("onArrivalMountainRangePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<MountainRangeMain>(eSceneType.MountainRange);
                                param.SpawnPos = new Vector3(1, 27, 0);
                            });
                        });

                        main.Init(param);
                        break;
                    }
                case eSceneType.MountainRange:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalAlleyPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<AlleyMain>(eSceneType.Alley);
                                param.SpawnPos = new Vector3(48.25f, 25, 0);
                            });
                        });
                        main.AddListener("onArrivalPandaVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PandaVillageMain>(eSceneType.PandaVillage);
                                param.SpawnPos = new Vector3(81f, 106.5f, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
                case eSceneType.BusStop:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalFarmPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<FarmMain>(eSceneType.Farm);
                                param.SpawnPos = new Vector3(78.5f, 47, 0);
                            });
                        });
                        main.AddListener("onArrivalPandaVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PandaVillageMain>(eSceneType.PandaVillage);
                                param.SpawnPos = new Vector3(1, 54.75f, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
                case eSceneType.PandaVillage:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalMountainRangePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<MountainRangeMain>(eSceneType.MountainRange);
                                param.SpawnPos = new Vector3(15.5f, 0.7f, 0);
                            });
                        });
                        main.AddListener("onArrivalBusStopPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<BusStopMain>(eSceneType.BusStop);
                                param.SpawnPos = new Vector3(33, 6, 0);
                            });
                        });
                        main.AddListener("onArrivalCindersapForestPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<CindersapForestMain>(eSceneType.CindersapForest);
                                param.SpawnPos = new Vector3(117.5f, 94, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
                case eSceneType.CindersapForest:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalFarmPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<FarmMain>(eSceneType.Farm);
                                param.SpawnPos = new Vector3(40.5f, 0.5f, 0);
                            });
                        });
                        main.AddListener("onArrivalPandaVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PandaVillageMain>(eSceneType.PandaVillage);
                                param.SpawnPos = new Vector3(1, 18, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
                case eSceneType.House:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalFarmPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<FarmMain>(eSceneType.Farm);
                                param.SpawnPos = new Vector3(64f, 49f, 0);
                            });
                        });
                        main.Init(param);
                        break;
                    }
            }
        };
    }
}
