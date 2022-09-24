using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    /*
    오솔길 : Alley
    산맥 : MountainRange
    농장 : Farm
    버스정류장 : BusStop
    펠리컨마을 : PelicanVillage
    잉걸불수액숲 : CindersapForest
     */
    public enum eSceneType
    {
        App, LogoScene, LoadingScene, Title, Alley, MountainRange, Farm, BusStop, PelicanVillage, CindersapForest
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
                                this.LoadScene<FarmMain>(eSceneType.Farm);
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
                            });
                        });
                        main.AddListener("onArrivalBusStopPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<BusStopMain>(eSceneType.BusStop);
                            });
                        });
                        main.AddListener("onArrivalCindersapForestPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<CindersapForestMain>(eSceneType.CindersapForest);
                            });
                        });
                        main.Init();
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
                            });
                        });
                        main.AddListener("onArrivalMountainRangePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<MountainRangeMain>(eSceneType.MountainRange);
                            });
                        });
                        main.Init();
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
                            });
                        });
                        main.AddListener("onArrivalPelicanVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PelicanVillageMain>(eSceneType.PelicanVillage);
                            });
                        });
                        main.Init();
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
                            });
                        });
                        main.AddListener("onArrivalPelicanVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PelicanVillageMain>(eSceneType.PelicanVillage);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.PelicanVillage:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalMountainRangePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<MountainRangeMain>(eSceneType.MountainRange);
                            });
                        });
                        main.AddListener("onArrivalBusStopPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<BusStopMain>(eSceneType.BusStop);
                            });
                        });
                        main.AddListener("onArrivalCindersapForestPortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<CindersapForestMain>(eSceneType.CindersapForest);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.CindersapForest:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onArrivalMountainRangePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<FarmMain>(eSceneType.Farm);
                            });
                        });
                        main.AddListener("onArrivalPelicanVillagePortal", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<PelicanVillageMain>(eSceneType.PelicanVillage);
                            });
                        });
                        main.Init();
                        break;
                    }
            }
        };
    }
}
