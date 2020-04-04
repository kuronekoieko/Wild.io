using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] PlayerController _playerController;
    [SerializeField] FeedManager _feedManager;

    void Awake()
    {
        Variables.eatenCounts = new int[1];
    }

    void Start()
    {
        _cameraController.OnStart(_playerController.transform.position);
        _playerController.OnStart();
        _feedManager.OnStart();
        Variables.timer = Values.TIME_LIMIT;
    }

    void Update()
    {
        switch (Variables.screenState)
        {
            case ScreenState.Start:
                break;
            case ScreenState.Game:
                _cameraController.FollowTarget(_playerController.transform.position);
                _playerController.OnUpdate();
                _feedManager.OnUpdate();
                Variables.timer -= Time.deltaTime;
                if (Variables.timer < 0)
                {
                    Variables.screenState = ScreenState.Result;
                }
                break;
            case ScreenState.Result:
                _playerController.Stop();
                break;
            default:
                break;
        }
    }
}
