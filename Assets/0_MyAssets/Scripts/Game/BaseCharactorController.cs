using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseCharactorController : MonoBehaviour
{
    public enum CharactorState
    {
        Alive,
        Dead,
        DeadAnim,
    }
    [SerializeField] ParticleSystem killedPS;
    [SerializeField] protected GameObject hideObject;
    public int size;
    public CharactorState charactorState { set; get; }
    Collider[] colliders;

    public void Killed()
    {
        hideObject.SetActive(false);
        charactorState = CharactorState.DeadAnim;
        if (killedPS) killedPS.Play();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
            charactorState = CharactorState.Dead;
        });
    }

    protected void OnAlive()
    {
        transform.position = GameManager.i.feedManager.GetRandomPos();
        charactorState = CharactorState.Alive;
        hideObject.SetActive(true);
        gameObject.SetActive(true);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    public virtual void OnStart()
    {
        colliders = GetComponents<Collider>();
    }

    public virtual void OnUpdate()
    {

    }
}
