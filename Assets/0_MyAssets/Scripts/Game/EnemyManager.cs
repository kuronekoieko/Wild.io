using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyPlayerController enemyPlayerPrefab;

    public void OnStart()
    {
        enemyPlayerPrefab.OnStart();
    }


    public void OnUpdate()
    {
        enemyPlayerPrefab.OnUpdate();
    }
}
