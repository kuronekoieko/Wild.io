using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedController : BaseCharactorController
{
    Rigidbody rb;
    public override void OnStart()
    {
        base.size = -1;
        base.OnStart();
        rb = GetComponent<Rigidbody>();
    }


    public override void OnUpdate()
    {
        if (base.charactorState == CharactorState.Dead)
        {
            rb.isKinematic = false;
            base.OnAlive();
        }
    }
}
