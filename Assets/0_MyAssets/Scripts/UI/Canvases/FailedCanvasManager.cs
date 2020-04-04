using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FailedCanvasManager : BaseCanvasManager
{
    [SerializeField] Button restartButton;
    [SerializeField] Button homeButton;
    [SerializeField] Text coinCountText;
    [SerializeField] CoinCountView coinCountView;
    public readonly ScreenState thisScreen = ScreenState.FAILED;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: thisScreen);
        restartButton.onClick.AddListener(OnClickRestartButton);
        homeButton.onClick.AddListener(OnClickHomeButton);
        gameObject.SetActive(false);
        coinCountView.OnStart();
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
        gameObject.SetActive(false);
    }

    void OnClickRestartButton()
    {
        Variables.screenState = ScreenState.INITIALIZE;
    }

    void OnClickHomeButton()
    {
        Variables.screenState = ScreenState.HOME;
    }
}
