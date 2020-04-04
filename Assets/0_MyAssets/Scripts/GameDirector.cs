using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体を総括するスクリプト
/// シーン一つでゲームを完結させる前提ですが、
/// 複数シーンでステージを切り替えたい場合はこのシーンを破棄せず、
/// シーンの追加読み込みで切り替えてください
/// </summary>
public class GameDirector : MonoBehaviour
{
    public UIManager uIManager { get; private set; }
    public GameManager gameManager { get; private set; }
    public static GameDirector i;

    /// <summary>
    /// インスタンスの生成はAwakeで
    /// awakeは各スクリプトに独立して書いてOK
    /// </summary>
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        uIManager = GetComponent<UIManager>();
        gameManager = GetComponent<GameManager>();
        i = this;
    }

    /// <summary>
    /// パラメーターなどの初期化はStartで
    /// Startはここにしか書かない
    /// </summary>
    void Start()
    {
        gameManager.OnStart();
        uIManager.OnStart();
        //SoundManager.i.OnStart(); //効果音を実装するときに使ってください
        //SaveDataManager.i.OnStart(); /セーブデータを実装するときに使ってください
        Variables.screenState = ScreenState.INITIALIZE;
    }

    /// <summary>
    /// Updateもここにしか書かない
    /// 画面の切り替えはVariables.screenStateを切り替えればOK
    /// 詳しくはBaseCanvasManagerを見てください
    /// </summary>
    void Update()
    {
        switch (Variables.screenState)
        {
            case ScreenState.INITIALIZE:
                gameManager.OnInitialize();
                Variables.screenState = ScreenState.GAME;
                break;
            case ScreenState.GAME:
                gameManager.OnUpdate();
                break;
            default:
                break;
        }
    }
}
