using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedController : BaseCharactorController
{

    public override void OnStart()
    {
        base.size = -1;
    }


    public override void OnUpdate()
    {
        if (base.isDead)
        {
            transform.position = GameManager.i.feedManager.GetRandomPos();
            base.isDead = false;
            gameObject.SetActive(true);
        }
    }
}
