using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create PlayerSizeSettingSO", fileName = "PlayerSizeSettingSO")]
public class PlayerSizeSettingSO : ScriptableObject
{
    public PlayerSize[] datas;

    private static PlayerSizeSettingSO _i;
    public static PlayerSizeSettingSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(PlayerSizeSettingSO);
            //初アクセス時にロードする
            if (_i != null) { return _i; }

            _i = Resources.Load<PlayerSizeSettingSO>(PATH);

            //ロード出来なかった場合はエラーログを表示
            if (_i == null)
            {
                Debug.LogError(PATH + " not found");
            }
            return _i;
        }
    }
}

[System.Serializable]
public class PlayerSize
{
    public int eatenCountToNextSize;
}