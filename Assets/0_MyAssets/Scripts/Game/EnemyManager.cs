using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyPlayerController enemyPlayerPrefab;
    EnemyPlayerController[] enemyPlayerControllers;

    public void OnStart()
    {
        enemyPlayerPrefab.OnStart();
        enemyPlayerControllers = new EnemyPlayerController[Variables.playerCount - 1];
        for (int i = 0; i < enemyPlayerControllers.Length; i++)
        {
            enemyPlayerControllers[i] = Instantiate(enemyPlayerPrefab, GameManager.i.GetRandomPlayerPos(), Quaternion.identity, transform);
            enemyPlayerControllers[i].OnStart();
            enemyPlayerControllers[i].SetParam(i + 1);
        }
    }


    public void OnUpdate()
    {
        for (int i = 0; i < enemyPlayerControllers.Length; i++)
        {
            enemyPlayerControllers[i].OnUpdate();
        }

    }
}
