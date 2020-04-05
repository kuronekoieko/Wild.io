﻿using System.Collections;
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
    [SerializeField] Text timerText;
    [SerializeField] Text eatenCountText;
    [SerializeField] RectTransform playerInfo;

    public readonly ScreenState thisScreen = ScreenState.Game;

    public override void OnStart()
    {

        base.SetScreenAction(thisScreen: thisScreen);
        this.ObserveEveryValueChanged(timer => Variables.timer)
            .Subscribe(_ => { SetTimeCountText(); })
            .AddTo(this.gameObject);

        this.ObserveEveryValueChanged(count => Variables.playerProperties[0].eatenCount)
            .Subscribe(count => eatenCountText.text = "★ " + count)
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate(ScreenState currentScreen)
    {
        if (currentScreen != thisScreen) { return; }
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, GameManager.i.playerController.transform.position);
        pos.y += 1000;
        playerInfo.anchoredPosition = pos;
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    void SetTimeCountText()
    {
        int sec = Mathf.CeilToInt(Variables.timer);
        float mSec = (Variables.timer - (sec - 1)) * 60f;
        if (Variables.timer == Values.TIME_LIMIT) { mSec = 0; }
        timerText.text = sec + "." + mSec.ToString("00");
    }

}
