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
    [SerializeField] TextMesh eatenCountTextPrefab;
    TextMesh eatenCountText;
    public int size;
    public CharactorState charactorState { set; get; }
    Collider[] colliders;
    public int eatenCount { get { return size + 2; } }

    public virtual void OnStart()
    {
        colliders = GetComponents<Collider>();
        eatenCountText = Instantiate(eatenCountTextPrefab, transform.position, Quaternion.identity, transform);
        eatenCountText.gameObject.SetActive(false);
        eatenCountText.transform.localScale = eatenCountText.transform.localScale * eatenCount * 0.7f;
    }

    public virtual void OnUpdate()
    {
        eatenCountText.transform.LookAt(Camera.main.transform.position);
    }

    public void Killed()
    {
        float duration = 1.0f;
        hideObject.SetActive(false);
        charactorState = CharactorState.DeadAnim;
        if (killedPS) killedPS.Play();

        eatenCountText.text = "+" + eatenCount;
        eatenCountText.gameObject.SetActive(true);
        eatenCountText.transform.DOMoveY(10, duration).SetRelative();


        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        DOVirtual.DelayedCall(duration, () =>
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
        eatenCountText.transform.position = transform.position;
        eatenCountText.gameObject.SetActive(false);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }


}
