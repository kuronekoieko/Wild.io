using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerController : BasePlayerController
{
    public override void OnStart()
    {
        base.walkVec = Vector3.forward;
        base.OnStart();
        base.playerIndex = 1;
    }


    public override void OnUpdate()
    {
        base.SetVelocityFromWalkVec();
    }

    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        Vector3 normal = col.contacts[0].normal;
        normal.y = 0;
        Vector3 reflectVec = Vector3.Reflect(base.walkVec, normal);
        base.walkVec = reflectVec;
    }
}

