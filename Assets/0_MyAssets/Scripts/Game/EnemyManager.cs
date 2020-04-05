using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] PlayerController playerPrefab;
    [SerializeField] Transform playerPossitionsParent;
    public PlayerController[] PlayerControllers { get; set; }
    Transform[] playerPoints;

    void Awake()
    {
        Variables.playerCount = playerPossitionsParent.childCount;
        Variables.playerProperties = new PlayerProperty[Variables.playerCount];
        for (int i = 0; i < Variables.playerProperties.Length; i++)
        {
            Variables.playerProperties[i] = new PlayerProperty();
            Variables.playerProperties[i].name = "Player " + i;
        }

        playerPoints = new Transform[Variables.playerCount];
        for (int i = 0; i < playerPoints.Length; i++)
        {
            playerPoints[i] = playerPossitionsParent.GetChild(i);
        }
    }

    public void OnStart()
    {
        PlayerControllers = new PlayerController[Variables.playerCount];
        for (int i = 0; i < PlayerControllers.Length; i++)
        {
            PlayerControllers[i] = Instantiate(
                playerPrefab,
                GetRandomPlayerPos(),
                Quaternion.identity,
                transform);



            PlayerControllers[i].SetParam(playerIndex: i);
            PlayerControllers[i].OnStart();
        }
    }

    public void Stop()
    {
        for (int i = 0; i < PlayerControllers.Length; i++)
        {
            PlayerControllers[i].Stop();
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < PlayerControllers.Length; i++)
        {
            PlayerControllers[i].OnUpdate();
        }
    }

    public Vector3 GetRandomPlayerPos()
    {
        int randomInt = Random.Range(0, playerPoints.Length);
        Vector3 pos = playerPoints[randomInt].position;
        playerPoints[randomInt].gameObject.SetActive(false);
        playerPoints = playerPoints.Where(p => p.gameObject.activeSelf).ToArray();

        return pos;
    }
}
