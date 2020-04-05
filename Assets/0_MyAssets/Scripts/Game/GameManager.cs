using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] FeedManager _feedManager;
    [SerializeField] PlayerManager _playerManager;

    public static GameManager i;
    public FeedManager feedManager { get { return _feedManager; } }
    public PlayerManager playerManager { get { return _playerManager; } }


    void Awake()
    {
        i = this;
    }
    void Start()
    {

        _feedManager.OnStart();
        _playerManager.OnStart();
        _cameraController.OnStart(_playerManager.playerControllers[0]);
        Variables.timer = Values.TIME_LIMIT;
    }



    void Update()
    {
        switch (Variables.screenState)
        {
            case ScreenState.Start:
                break;
            case ScreenState.Game:
                _cameraController.FollowTarget(_playerManager.playerControllers[0].transform.position);
                _feedManager.OnUpdate();
                _playerManager.OnUpdate();
                Variables.timer -= Time.deltaTime;
                if (Variables.timer < 0)
                {
                    Variables.screenState = ScreenState.Result;
                }
                break;
            case ScreenState.Result:
                _playerManager.Stop();
                break;
            default:
                break;
        }
    }
}
