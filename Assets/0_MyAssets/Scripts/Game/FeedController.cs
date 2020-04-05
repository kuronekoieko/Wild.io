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
        if (base.charactorState == CharactorState.Dead)
        {
            transform.position = GameManager.i.feedManager.GetRandomPos();
            base.charactorState = CharactorState.Alive;
            base.animator.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
    }
}
