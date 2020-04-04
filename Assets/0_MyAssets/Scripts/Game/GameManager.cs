using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] PlayerController _playerController;

    void Start()
    {
        _cameraController.OnStart(_playerController.transform.position);
        _playerController.OnStart();
    }

    void Update()
    {
        _cameraController.FollowTarget(_playerController.transform.position);
        _playerController.OnUpdate();
    }
}
