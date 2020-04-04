using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BasePlayerController : BaseCharactorController
{
    protected int playerIndex;
    protected Rigidbody rb;
    float walkSpeed = 500f;
    protected Vector3 walkVec;
    public override void OnStart()
    {
        base.size = 0;
        this.ObserveEveryValueChanged(count => Variables.eatenCounts[playerIndex])
            .Subscribe(count => CheckSizeUp(count))
            .AddTo(this.gameObject);
        rb = GetComponent<Rigidbody>();
    }

    public override void OnUpdate()
    {

    }

    protected void SetVelocityFromWalkVec()
    {
        float degree = Vector2ToDegree(new Vector2(walkVec.z, walkVec.x));
        transform.eulerAngles = new Vector3(0, degree, 0);
        rb.velocity = walkVec.normalized * walkSpeed * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter(Collision col)
    {

        var colCharactor = col.gameObject.GetComponent<BaseCharactorController>();
        if (colCharactor == null) { return; }
        //おなじだと両方消えるので
        if (colCharactor.size > base.size) { return; }

        Variables.eatenCounts[playerIndex]++;
        colCharactor.Killed();
    }

    void CheckSizeUp(int eatenCount)
    {
        int eatenCountToNextSize = PlayerSizeSettingSO.i.datas[base.size].eatenCountToNextSize;

        if (eatenCount < eatenCountToNextSize) { return; }

        size++;
        transform.localScale *= 2;
    }

    public static float Vector2ToDegree(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }
}
