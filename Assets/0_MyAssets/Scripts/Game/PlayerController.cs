using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayerController
{
    [SerializeField] Animator animator;
    Vector3 mouseDownPos;


    public override void OnStart()
    {
        base.OnStart();
        base.playerIndex = 0;
    }

    public override void OnUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            SetWalkVec();
        }



        base.SetVelocityFromWalkVec();
    }

    public void Stop()
    {
        base.rb.velocity = Vector3.zero;
    }

    void SetWalkVec()
    {

        Vector2 mouseVec = Input.mousePosition - mouseDownPos;

        //タップで止まる対策
        if (mouseVec.sqrMagnitude < 1.0f) { return; }
        walkVec.x = mouseVec.x;
        walkVec.z = mouseVec.y;

    }



}
