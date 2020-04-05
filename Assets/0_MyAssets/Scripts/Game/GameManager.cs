using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] FeedManager _feedManager;
    [SerializeField] EnemyManager _enemyManager;

    public static GameManager i;
    public FeedManager feedManager { get { return _feedManager; } }
    public EnemyManager enemyManager { get { return _enemyManager; } }


    void Awake()
    {
        i = this;
    }
    void Start()
    {

        _feedManager.OnStart();
        _enemyManager.OnStart();
        _cameraController.OnStart(_enemyManager.PlayerControllers[0]);
        Variables.timer = Values.TIME_LIMIT;
    }



    void Update()
    {
        switch (Variables.screenState)
        {
            case ScreenState.Start:
                break;
            case ScreenState.Game:
                _cameraController.FollowTarget(_enemyManager.PlayerControllers[0].transform.position);
                _feedManager.OnUpdate();
                _enemyManager.OnUpdate();
                Variables.timer -= Time.deltaTime;
                if (Variables.timer < 0)
                {
                    Variables.screenState = ScreenState.Result;
                }
                break;
            case ScreenState.Result:
                _enemyManager.Stop();
                break;
            default:
                break;
        }
    }
}
