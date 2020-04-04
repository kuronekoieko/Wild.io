using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    Vector3 distanceToPlayer;
    float nFocalLength;

    Dictionary<int, float> apertureDic = new Dictionary<int, float>()
    {
        {5,30.0f},
        {20,35.0f},
        {30,40.0f},
        {50,40.0f},
    };

    public void OnStart(Vector3 playerPos)
    {
        distanceToPlayer = transform.position - playerPos;

        nFocalLength = focalLength(Camera.main.fieldOfView, 20);
        //transform.position = playerPos + distanceToPlayer.normalized * nFocalLength;
    }


    public void FollowTarget(Vector3 playerPos)
    {

        /*  if (apertureDic.TryGetValue(Variables.humanCount, out float aperture))
                {
                    float f = focalLength(Camera.main.fieldOfView, aperture);
                    DOTween.To(() => nFocalLength, (x) => nFocalLength = x, f, 0.5f);
                }*/


        // transform.position = playerPos + distanceToPlayer.normalized * nFocalLength;
        transform.position = playerPos + distanceToPlayer;
    }


    /*! 
     @brief 焦点距離(FocalLength)を求める
     @param[in]		fov			視野角(FieldOfView)
     @param[in]		aperture	画面幅いっぱいに表示したいオブジェクトの幅
     @return        焦点距離(FocalLength)
    */
    float focalLength(float fov, float aperture)
    {
        // FieldOfViewを2で割り、三角関数用にラジアンに変換しておく
        float nHalfTheFOV = fov / 2.0f * Mathf.Deg2Rad;

        // FocalLengthを求める
        float nFocalLength = (0.5f / (Mathf.Tan(nHalfTheFOV) / aperture));

        // Unityちゃんは画面高さ(Vertical)なFOVなので画面アスペクト比(縦/横)を掛けとく
        nFocalLength *= ((float)Screen.height / (float)Screen.width);

        return nFocalLength;
    }
}
