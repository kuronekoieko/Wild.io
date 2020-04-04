using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] PlayerController _playerController;
    [SerializeField] FeedManager _feedManager;
    [SerializeField] EnemyManager _enemyManager;
    [SerializeField] Transform playerPossitionsParent;
    Transform[] playerPoints;
    public static GameManager i;

    void Awake()
    {
        Variables.playerCount = playerPossitionsParent.childCount;
        Variables.eatenCounts = new int[Variables.playerCount];
        playerPoints = new Transform[Variables.playerCount];
        for (int i = 0; i < playerPoints.Length; i++)
        {
            playerPoints[i] = playerPossitionsParent.GetChild(i);
        }
        i = this;
    }

    void Start()
    {
        _playerController.OnStart();
        _playerController.transform.position = GetRandomPlayerPos();
        _cameraController.OnStart(_playerController.transform.position);
        _feedManager.OnStart();
        _enemyManager.OnStart();
        Variables.timer = Values.TIME_LIMIT;
    }

    public Vector3 GetRandomPlayerPos()
    {
        int randomInt = Random.Range(0, playerPoints.Length);
        Vector3 pos = playerPoints[randomInt].position;
        playerPoints[randomInt].gameObject.SetActive(false);
        playerPoints = playerPoints.Where(p => p.gameObject.activeSelf).ToArray();

        return pos;
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
                _enemyManager.OnUpdate();
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
