using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
public class BasePlayerController : BaseCharactorController
{
    protected int playerIndex;
    protected Rigidbody rb;
    float walkSpeed = 700f;
    protected Vector3 walkVec;
    int maxSize;

    public override void OnStart()
    {
        base.size = 0;
        this.ObserveEveryValueChanged(count => Variables.playerProperties[playerIndex].eatenCount)
            .Subscribe(count => CheckSizeUp(count))
            .AddTo(this.gameObject);
        rb = GetComponent<Rigidbody>();
        transform.localScale = Vector3.one;
        maxSize = Variables.playerSizes.Last().size;
    }

    public override void OnUpdate()
    {

    }

    protected void SetVelocityFromWalkVec()
    {
        float degree = Vector2ToDegree(new Vector2(walkVec.z, walkVec.x));
        transform.eulerAngles = new Vector3(0, degree, 0);
        Vector3 vel = walkVec.normalized * walkSpeed * Time.deltaTime;
        //落下しなくなるため、上に飛ばないようにする
        if (rb.velocity.y < 0) vel.y = rb.velocity.y;
        rb.velocity = vel;
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        var colCharactor = col.gameObject.GetComponent<BaseCharactorController>();
        if (colCharactor == null) { return; }
        //おなじだと両方消えるので
        if (colCharactor.size >= base.size) { return; }

        Variables.playerProperties[playerIndex].eatenCount++;
        colCharactor.Killed();
    }

    void CheckSizeUp(int eatenCount)
    {
        if (base.size > maxSize) { return; }
        int eatenCountToNextSize = Variables.playerSizes[base.size].eatenCountToNextSize;
        if (eatenCount < eatenCountToNextSize) { return; }

        size++;
        transform.localScale += Vector3.one;
        walkSpeed += 100;
    }

    public static float Vector2ToDegree(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }
}
