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
    [SerializeField] protected Animator animator;
    public int size;
    public CharactorState charactorState { set; get; }

    public void Killed()
    {
        if (animator) animator.gameObject.SetActive(false);
        charactorState = CharactorState.DeadAnim;
        if (killedPS) killedPS.Play();
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
            charactorState = CharactorState.Dead;
        });
    }

    public virtual void OnStart()
    {

    }

    public virtual void OnUpdate()
    {

    }
}
