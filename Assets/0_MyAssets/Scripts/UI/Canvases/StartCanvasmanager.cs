using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartCanvasmanager : BaseCanvasManager
{

    [SerializeField] Button startButton;
    public readonly ScreenState thisScreen = ScreenState.Start;


    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: thisScreen);
        startButton.onClick.AddListener(OnClickStartButton);
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

    void OnClickStartButton()
    {
        Variables.screenState = ScreenState.Game;
    }
}
