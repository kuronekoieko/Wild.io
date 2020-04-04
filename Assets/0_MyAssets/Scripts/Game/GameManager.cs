using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] PlayerController _playerController;
    [SerializeField] FeedManager _feedManager;

    void Start()
    {
        _cameraController.OnStart(_playerController.transform.position);
        _playerController.OnStart();
        _feedManager.OnStart();
    }

    void Update()
    {
        _cameraController.FollowTarget(_playerController.transform.position);
        _playerController.OnUpdate();
        _feedManager.OnUpdate();
    }
}
