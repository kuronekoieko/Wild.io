using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharactorController : MonoBehaviour
{
    public int size { set; get; }

    public void Killed()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnStart()
    {

    }

    public virtual void OnUpdate()
    {

    }
}
