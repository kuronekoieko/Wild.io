using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Button clearButton;
    [SerializeField] Button failButton;
    [SerializeField] Button loginButton;
    [SerializeField] Button homeButton;
    [SerializeField] Text stageNumText;

    public readonly ScreenState thisScreen = ScreenState.GAME;

    public override void OnStart()
    {
        clearButton.onClick.AddListener(() => { Variables.screenState = ScreenState.CLEAR; });
        failButton.onClick.AddListener(() => { Variables.screenState = ScreenState.FAILED; });
        loginButton.onClick.AddListener(() => { Variables.screenState = ScreenState.LOGIN; });
        homeButton.onClick.AddListener(() => { Variables.screenState = ScreenState.HOME; });

        base.SetScreenAction(thisScreen: thisScreen);

        this.ObserveEveryValueChanged(currentStageIndex => Variables.currentStageIndex)
            .Subscribe(currentStageIndex => { ShowStageNumText(); })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate(ScreenState currentScreen)
    {
        if (currentScreen != thisScreen) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    void ShowStageNumText()
    {
        stageNumText.text = "Stage " + (Variables.currentStageIndex + 1).ToString("000");
    }
}
