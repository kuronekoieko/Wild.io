using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    void Awake()
    {
        // 開発している画面を元に縦横比取得 (縦画面) iPhoneXS MAXサイズ
        float developAspect = 1242.0f / 2688.0f;
        // 横画面で開発している場合は以下の用に切り替えます
        // float developAspect = 1334.0f / 750.0f;

        // 実機のサイズを取得して、縦横比取得
        float deviceAspect = (float)Screen.width / (float)Screen.height;

        // 実機と開発画面との対比
        float scale = deviceAspect / developAspect;

        Camera mainCamera = Camera.main;

        // カメラに設定していたorthographicSizeを実機との対比でスケール
        float deviceSize = mainCamera.orthographicSize;
        // scaleの逆数
        float deviceScale = 1.0f / scale;
        // orthographicSizeを計算し直す
        mainCamera.orthographicSize = deviceSize * deviceScale;

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}
