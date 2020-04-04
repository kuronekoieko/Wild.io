using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ResultCanvasManager : BaseCanvasManager
{
    [SerializeField] Button nextButton;
    [SerializeField] Text eatenCountText;
    public readonly ScreenState thisScreen = ScreenState.Result;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: thisScreen);
        nextButton.onClick.AddListener(OnClickNextButton);
        gameObject.SetActive(false);
    }

    public override void OnUpdate(ScreenState currentScreen)
    {
        if (currentScreen != thisScreen) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
        eatenCountText.text = "★" + Variables.eatenCounts[0];
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
    }

    void OnClickNextButton()
    {
        Variables.screenState = ScreenState.Start;
        base.ReLoadScene();
    }
}
