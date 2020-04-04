using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BasePlayerController : BaseCharactorController
{
    public int playerIndex;
    public override void OnStart()
    {
        base.size = 0;
        this.ObserveEveryValueChanged(count => Variables.eatenCounts[playerIndex])
            .Subscribe(count => CheckSizeUp(count))
            .AddTo(this.gameObject);
    }

    public override void OnUpdate()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        var colCharactor = col.gameObject.GetComponent<BaseCharactorController>();
        if (colCharactor == null) { return; }
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
}
