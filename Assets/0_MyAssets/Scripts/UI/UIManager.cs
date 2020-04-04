using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面UIの一括管理
/// GameDirectorと各画面を中継する役割
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform canvasesPatent;
    BaseCanvasManager[] canvases;
    void Start()
    {
        canvases = new BaseCanvasManager[canvasesPatent.childCount];
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i] = canvasesPatent.GetChild(i).GetComponent<BaseCanvasManager>();
            if (canvases[i] == null) { continue; }
            canvases[i].OnStart();
        }
    }

    void OnUpdate(ScreenState currentScreen)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i] == null) { continue; }
            canvases[i].OnUpdate(currentScreen);
        }

    }
}
