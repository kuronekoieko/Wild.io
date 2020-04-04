using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BasePlayerController : BaseCharactorController
{
    public int eatenCount;
    public override void OnStart()
    {
        base.size = 0;
        this.ObserveEveryValueChanged(count => this.eatenCount)
            .Subscribe(count => CheckSizeUp())
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
        eatenCount++;
        colCharactor.Killed();
    }

    void CheckSizeUp()
    {
        int eatenCountToNextSize = PlayerSizeSettingSO.i.datas[base.size].eatenCountToNextSize;

        if (eatenCount < eatenCountToNextSize) { return; }

        size++;
        transform.localScale *= 2;
    }
}
