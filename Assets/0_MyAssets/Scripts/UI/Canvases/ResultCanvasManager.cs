using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ResultCanvasManager : BaseCanvasManager
{
    [SerializeField] Button nextButton;
    [SerializeField] PlayerResultController playerResultPrefab;
    public readonly ScreenState thisScreen = ScreenState.Result;
    PlayerResultController[] playerResults;
    PlayerProperty[] playerProperties;
    float posY = 730f;
    readonly int rankCount = 6;

    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: thisScreen);
        nextButton.onClick.AddListener(OnClickNextButton);
        gameObject.SetActive(false);
        playerResults = new PlayerResultController[rankCount];
        for (int i = 0; i < playerResults.Length; i++)
        {
            playerResults[i] = Instantiate(playerResultPrefab, Vector3.zero, Quaternion.identity, transform);
            playerResults[i].OnStart(posY);
            posY -= 100;
        }

    }

    public override void OnUpdate()
    {
      

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
        PlayerProperty[] ranking = Variables.playerProperties
            .OrderByDescending(p => p.eatenCount)
            .ToArray();
        for (int i = 0; i < playerResults.Length; i++)
        {
            playerResults[i].ShowParam(
                rank: i + 1,
                name: ranking[i].name,
                eatenCount: ranking[i].eatenCount
                );
        }
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
